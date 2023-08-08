using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SaleSampleAPI.Data;
using SaleSampleAPI.Models;
using SaleSampleAPI.Repository.interfaces;

namespace SaleSampleAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        //Injected
        private SalesContext _salesContext;

        public ProductRepository(SalesContext productContext)
        {
            _salesContext = productContext;
        }

        public int AddProduct(Product product)
        {
            this._salesContext.Add(product);
            
            return this._salesContext.SaveChanges();
        }

        public int UpdateProduct(Product product)
        {
            this._salesContext.Update(product);
            //this._salesContext.Database.SqlQuery()
            return this._salesContext.SaveChanges();
        }

        public Product GetProduct(int id)
        {
            Product product = this._salesContext.Product.Where(product => product.Id == id).FirstOrDefault();
            return product;
        }

        public List<Product> GetProducts(List<int> ids) 
        {
            var prods = (
                        from products in this._salesContext.Product
                        select products
                        ).ToList()
                        ;


            var result =
                from products in prods
                join prodsRequired in ids
                    on products.Id equals prodsRequired
                select products;

            return result.ToList();
        }

   
    }
}
