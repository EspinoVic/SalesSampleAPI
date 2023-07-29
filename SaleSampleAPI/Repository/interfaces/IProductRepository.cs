using SaleSampleAPI.Models;

namespace SaleSampleAPI.Repository.interfaces
{
    public interface IProductRepository
    {
        //IMplementing interfaces because it's very useful for Dependency Injection
        public int AddProduct(Product product);
        public int UpdateProduct(Product product);
        public Product GetProduct(int id);
        public List<Product> GetProducts(List<int> ids);

    }
}
