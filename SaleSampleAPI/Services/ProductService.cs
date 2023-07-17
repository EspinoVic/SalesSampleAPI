using SaleSampleAPI.Models;
using SaleSampleAPI.Repository;

namespace SaleSampleAPI.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        
        //Injection
        public ProductService(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        public bool AddNewProduct(Product product)
        {
            if (product == null) return false; // throw new ArgumentNullException();
            return this._productRepository.AddProduct(product) == 1;
        }

        public Product GetProductById(int id)
        {
            Product product = this._productRepository.GetProduct(id);
            return product;
        }

        public int UpdateInventoryProduct(int id, int newInventoryAmount)
        {
            Product productToUpdate = this.GetProductById(id);
            if (productToUpdate == null) //product doesn't exist
                return 0; //throw exception or log error?
            productToUpdate.InventoryAmount = newInventoryAmount;

            return this._productRepository.UpdateProduct(productToUpdate);
        }
    }
}
