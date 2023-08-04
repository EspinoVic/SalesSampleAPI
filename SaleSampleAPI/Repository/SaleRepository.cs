using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SaleSampleAPI.Data;
using SaleSampleAPI.Models;
using SaleSampleAPI.Repository.interfaces;
using System.Data;

namespace SaleSampleAPI.Repository
{
    public class SaleRepository : ISaleRepository
    {
        //Should I interface the salesContext? check when mocking project
        private SalesContext _salesContext;

        public SaleRepository(SalesContext salesContext)
        {
            _salesContext = salesContext;
        }

        public int Add(int regionID, DataTable dtProduct)
        {
            //Table-Valued Parameter
            SqlParameter regionParam = new SqlParameter
            {
                ParameterName = "regionId",
                SqlDbType = SqlDbType.Int,
                Value = regionID
            };

            var productList = new SqlParameter()
            {
                ParameterName = "productsIdList",
                Value = dtProduct,
                SqlDbType = SqlDbType.Structured,
                TypeName = "dbo.ListProductsToBuy"
            };

            return this._salesContext.Database.ExecuteSqlRaw("ssa_sp_create_sale",regionID, productList);
        }

        public Sale GetSale(int idSale)
        {
            return this._salesContext.Sales.Where(sale => sale.Id == idSale).FirstOrDefault();
        }

        public float GetSalePrice(int saleId)
        {
            throw new NotImplementedException();
        }

        public List<SaleProducts> GetSaleProducts(int saleId)
        {
            throw new NotImplementedException();
        }
    }
}
