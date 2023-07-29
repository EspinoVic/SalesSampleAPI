using Microsoft.EntityFrameworkCore;
using SaleSampleAPI.Repository.interfaces;

namespace SaleSampleAPI.Repository
{
    public class EntityDatabaseTransaction : IDatabaseTransaction
    {

        Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction _dbContextTransaction;

        public EntityDatabaseTransaction(DbContext dbContext)
        {
            this._dbContextTransaction = dbContext.Database.BeginTransaction();
        }


        public void Commit()
        {
            this._dbContextTransaction.Commit();
        }

        public void Dispose()
        {
            this._dbContextTransaction.Dispose();
        }

        public void Rollback()
        {
            this._dbContextTransaction.Rollback();
        }
    }
}
