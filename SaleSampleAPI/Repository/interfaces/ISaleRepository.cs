using SaleSampleAPI.Models;
using System.Data;

namespace SaleSampleAPI.Repository.interfaces
{
    public interface ISaleRepository
    {
        //Creates the SALE
        public int Add(int regionID, DataTable dtProducts);

        //public bool CreateSale(int regionId, List<KeyValuePair<int, int>> products);
        public Sale GetSale(int idSale);

        public List<SaleProducts> GetSaleProducts(int saleId);

        //or should the service use GetSaleProducts function and calculate the total?,
        //even if it's a waste of memoryy
        public float GetSalePrice(int saleId);


    }
}
