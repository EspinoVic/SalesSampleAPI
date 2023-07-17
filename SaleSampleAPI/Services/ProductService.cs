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
            return this._productRepository.AddProduct(product) == 1;
        }

        public Product GetProductById(int id)
        {
            return this._productRepository.GetProduct(id);
        }

        public int UpdateInventoryProduct(int id, int newInventoryAmount)
        {
            Product productToUpdate = this.GetProductById(id);
            productToUpdate.InventoryAmount = newInventoryAmount;

            return this._productRepository.UpdateProduct(productToUpdate);
        }
    }
}
