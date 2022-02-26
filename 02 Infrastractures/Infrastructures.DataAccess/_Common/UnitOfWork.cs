using System;
using System.Threading.Tasks;
using Framework.Domain.Exceptions;
using YQB.Contracts._Common;
using YQB.Contracts.People.Repositories;

namespace YQB.Infra.Data._Common
{
    public sealed class UnitOfWork<TDbContext> : IUnitOfWork
        where TDbContext : BaseDbContext
    {
        private readonly TDbContext _dbContext;
        private readonly IUnitOfWorkConfiguration _config;

        public IPersonRepository PersonRepository { get; }

        public UnitOfWork(TDbContext dbContext,
            IUnitOfWorkConfiguration config, 
            IPersonRepository personRepository)
        {
            _dbContext = dbContext;
            _config = config;

            PersonRepository = personRepository;
        }

        public void InitiateDatabase()
        {
            if (!_config.Seed.IsEnable)
                return;

            try
            {
                //
                Commit();
            }
            catch (Exception ex)
            {
                throw new InitializeDataBaseException(ex);
            }
        }
        public async Task InitiateDatabaseAsync()
        {
            if (!_config.Seed.IsEnable)
                return;

            try
            {
                //
                await CommitAsync();
            }
            catch (Exception ex)
            {
                throw new InitializeDataBaseException(ex);
            }
        }

        public void BeginTransaction() => _dbContext.BeginTransaction();
        public Task BeginTransactionAsync() => _dbContext.BeginTransactionAsync();

        public int Commit() => _dbContext.SaveChanges();
        public async Task<int> CommitAsync() => await _dbContext.SaveChangesAsync();

        public void Rollback() => _dbContext.RollbackTransaction();
        public Task RollbackAsync() => _dbContext.RollbackTransactionAsync();
    }
}