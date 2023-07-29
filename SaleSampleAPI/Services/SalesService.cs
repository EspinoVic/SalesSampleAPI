using SaleSampleAPI.Models;
using SaleSampleAPI.Repository.interfaces;
using SaleSampleAPI.Services.interfaces;
using System.Collections.Generic;
using System.Data;

namespace SaleSampleAPI.Services
{
    public class SalesService : ISalesService
    {
        private ISaleRepository _saleRepository;
        private IProductRepository _productRepository;
        private ITaxesRegionRepository _taxRegionRepository;

        public SalesService(ISaleRepository saleRepository, IProductRepository productRepository
            , ITaxesRegionRepository taxRegionRepository)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _taxRegionRepository = taxRegionRepository;
        }

        public bool CreateSale(int regionId, Dictionary<int,int> products)
        {
            //call SP to create the sale
            DataTable dtProduct = new DataTable();
            dtProduct.Columns.Add("idProduct", typeof(int));
            dtProduct.Columns.Add("amount", typeof(int));

            //Add data
            foreach (var item in products)
                dtProduct.Rows.Add(item.Key, item.Value);

            //return
            return this._saleRepository.Add(regionId, dtProduct) == 0;

        }
        public List<int> GetSaleProductsIds(int saleId)
        {
            List<SaleProducts> products = this._saleRepository.GetSaleProducts(saleId);
            var asd = from prod in products
                      select prod.idProduct;

            return asd.ToList();
        }

        public List<Product> DetailSale(int saleId)
        {
            //Reads on Sale_Product to get the Products
            var products = this.GetSaleProductsIds(saleId);
 
            List<Product>  productsFromSale = this._productRepository.GetProducts(products);
            return productsFromSale;            
        }


        public double TotalSale(int saleId)
        {
            //Reads on Sale_Product to get the Products,
            //----validations, sale must exist
            
            List<Product> productsFromSale = 
                this._productRepository.GetProducts(GetSaleProductsIds(saleId));
            Sale sale = this._saleRepository.GetSale(saleId);
            //and SUM the products' prices and calculate taxes according to the region

            //better to do composite? to avoid calling a lot of repositories. (complex entity framework object)
            TaxesRegion taxesRegion
                = this._taxRegionRepository.Get(sale.taxesRegion);

            double saleTotal = 0;

            productsFromSale.ForEach(
                (product)=> { saleTotal += (product.Cost * taxesRegion.Percentage); }
            );

            return saleTotal;

        }
    }
}
