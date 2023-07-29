using SaleSampleAPI.Data;
using SaleSampleAPI.Models;
using SaleSampleAPI.Repository.interfaces;

namespace SaleSampleAPI.Repository
{
    public class TaxesRegionRepository : ITaxesRegionRepository
    {
        private SalesContext _salesContext;

        public TaxesRegionRepository(SalesContext salesContext)
        {
            _salesContext = salesContext;
        }

        public TaxesRegion Get(int id)
        {
            return this._salesContext.TaxesRegigons.Where(tr => tr.RegionId == id).FirstOrDefault();
        }
    }
}
