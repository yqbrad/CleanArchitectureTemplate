using System.Threading.Tasks;
using $safeprojectname$.People.Repositories;

namespace $safeprojectname$._Common
{
    public interface IUnitOfWork
    {
        void InitiateDatabase();
        Task InitiateDatabaseAsync();

        void BeginTransaction();
        Task BeginTransactionAsync();

        int Commit();
        Task<int> CommitAsync();

        void Rollback();
        Task RollbackAsync();

        IPersonRepository PersonRepository { get; }
    }
}