using SaleSampleAPI.Models;

namespace SaleSampleAPI.Services.interfaces
{
    public interface IProductService
    {
        public bool AddNewProduct(Product product);

        public Product GetProductById(int id);

        public int UpdateInventoryProduct(int id, int newInventoryAmount);


    }
}
