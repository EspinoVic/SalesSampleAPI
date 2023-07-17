using SaleSampleAPI.Data;
using SaleSampleAPI.Models;

namespace SaleSampleAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        //Injection
        private ProductContext _productContext;

        public ProductRepository(ProductContext productContext)
        {
            _productContext = productContext;
        }

        public int AddProduct(Product product)
        {
            this._productContext.Add(product);
            
            return this._productContext.SaveChanges();
        }

        public int UpdateProduct(Product product)
        {
            this._productContext.Update(product);

            return this._productContext.SaveChanges();
        }

        public Product GetProduct(int id)
        {
            Product product = this._productContext.Product.Where(product => product.Id == id).FirstOrDefault();
            return product;
        }

    }
}
