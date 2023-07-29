namespace SaleSampleAPI.Repository.interfaces
{
    public interface IDatabaseTransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
