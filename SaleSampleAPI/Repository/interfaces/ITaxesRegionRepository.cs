using SaleSampleAPI.Models;

namespace SaleSampleAPI.Repository.interfaces
{
    public interface ITaxesRegionRepository
    {
        public TaxesRegion Get(int id);
    }
}
