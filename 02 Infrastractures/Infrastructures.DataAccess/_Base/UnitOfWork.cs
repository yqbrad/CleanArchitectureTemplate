using DDD.Contracts._Base;
using System;
using System.Threading.Tasks;
using DDD.DomainModels._Exceptions;

namespace DDD.Infrastructure.DataAccess._Base
{
    public sealed class UnitOfWork<TDbContext> : IUnitOfWork
        where TDbContext : BaseDbContext
    {
        //public IAppRepository App { get; }

        private readonly TDbContext _dbContext;
        private readonly IUnitOfWorkConfiguration _config;
        public UnitOfWork(TDbContext dbContext, IUnitOfWorkConfiguration config)
        {
            _dbContext = dbContext;
            _config = config;

            //App = app;
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