using System.Threading.Tasks;
using YQB.Contracts.People.Repositories;

namespace YQB.Contracts._Common
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