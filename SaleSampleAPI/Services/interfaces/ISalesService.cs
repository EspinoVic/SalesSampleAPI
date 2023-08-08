using SaleSampleAPI.Models;

namespace SaleSampleAPI.Services.interfaces
{
    public interface ISalesService
    {
        public bool CreateSale(int regionId,  Dictionary<int, int> products);
        public List<Tuple<Product, int>> DetailSale(int saleId);
        public double TotalSale(int saleId);
    }
}
