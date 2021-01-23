using System;
using System.Threading.Tasks;

namespace $safeprojectname$._Base
{
    public interface IUnitOfWork : IDisposable, ICloneable
    {
        void InitiateDatabase(InitialData data);
        Task InitiateDatabaseAsync(InitialData data);

        void BeginTransaction();
        Task BeginTransactionAsync();

        void Commit();
        Task CommitAsync();

        void Rollback();
        Task RollbackAsync();
    }
}