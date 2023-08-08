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
        public List<SaleProducts> GetSaleProducts(int saleId)
        {
            List<SaleProducts> products = this._saleRepository.GetSaleProducts(saleId);

            return products.ToList();
        }

        public List<Tuple<Product, int>> DetailSale(int saleId)
        {
            List<Tuple<Product, int>> productsAmount = new List<Tuple<Product, int>>();

            //Reads on Sale_Product to get the Products
            var productsSale = this.GetSaleProducts(saleId); //ProdId and Amount bought
            if (productsSale == null || productsSale.Count == 0)
            {
                //log error
                return productsAmount; //empty
            }

            //now lets get the products itself
            var productsId = from prod in productsSale
                             select prod.idProduct;
            List < Product > productsFromSale = this._productRepository.GetProducts(productsId.ToList());

            //At this point we have the products model and the amount, we need to merge them

            
            productsFromSale.ForEach(productsFromSale =>
            {
                productsAmount.Add(
                    Tuple.Create(
                        productsFromSale,
                        productsSale.Where(prod => prod.idProduct == productsFromSale.Id).First().amount)
                    );                
            });

            return productsAmount;            
        }


        public double TotalSale(int saleId)
        {
            //Reads on Sale_Product to get the Products,
            //----validations, sale must exist
            
            List<Product> productsFromSale = 
                this._productRepository.GetProducts(GetSaleProductsIds(saleId));
            if(productsFromSale == null || productsFromSale.Count == 0)
            {
                //log error
                return -1;
            }
            Sale sale = this._saleRepository.GetSale(saleId);
            if(sale == null)
            {
                return -1;
            }
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
