using DDD.Contracts._Base;
using Helper.Exceptions.Exceptions;
using RepoDb;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DDD.Infrastructure.DataAccess._Base
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        #region Variables
        private readonly IUnitOfWorkConfiguration _config;
        private SqlTransaction _transaction;
        private SqlConnection _connection;
        #endregion

        #region PrivateRepositories

        #endregion

        #region Properties

        #endregion

        public UnitOfWork(IUnitOfWorkConfiguration configuration)
        {
            SqlServerBootstrap.Initialize();
            _config = configuration;
            Task.Run(
                async () =>
                {
                    await BeginTransactionAsync();
                }).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        #region ►| IUnitOfWork |◄
        public void InitiateDatabase(InitialData data)
        {
            if (!_config.Seed.IsEnable)
                return;

            try
            {

            }
            catch (Exception ex)
            {
                throw new InitializeDataBaseException(ex);
            }
        }

        public async Task InitiateDatabaseAsync(InitialData data)
        {
            if (!_config.Seed.IsEnable)
                return;

            try
            {

                await CommitAsync();
            }
            catch (Exception ex)
            {
                throw new InitializeDataBaseException(ex);
            }
        }

        public void BeginTransaction()
        {
            /*~~~~~~~~~~~~~~~ SQL SERVER ~~~~~~~~~~~~~~~*/
            /* Dispose */
            _transaction?.Rollback();
            _transaction?.Dispose();
            _connection?.Close();
            _connection?.Dispose();
            /* Create */
            _connection = new SqlConnection(_config.SqlServerConnectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public async Task BeginTransactionAsync()
        {
            /*~~~~~~~~~~~~~~~ SQL SERVER ~~~~~~~~~~~~~~~*/
            /* Dispose */
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
            }

            if (_connection != null)
            {
                await _connection.CloseAsync();
                await _connection.DisposeAsync();
            }

            /* Create */
            _connection = new SqlConnection(_config.SqlServerConnectionString);
            await _connection.OpenAsync();
            var dbTransaction = await _connection.BeginTransactionAsync();
            _transaction = (SqlTransaction)dbTransaction;
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch (Exception)
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                Dispose();
            }
            BeginTransaction();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch (Exception)
            {
                await _transaction.RollbackAsync();
                throw;
            }
            finally
            {
                Dispose();
            }
            await BeginTransactionAsync();
        }

        public void Rollback()
        {
            try
            {
                _transaction.Rollback();
            }
            finally
            {
                Dispose();
            }
            BeginTransaction();
        }

        public async Task RollbackAsync()
        {
            try
            {
                await _transaction.RollbackAsync();
            }
            finally
            {
                Dispose();
            }
            await BeginTransactionAsync();
        }

        #endregion

        #region ►| IDisposable |◄
        ~UnitOfWork() => dispose(false);

        public object Clone() => (IUnitOfWork)Activator.CreateInstance(GetType());

        private void dispose(bool disposing)
        {
            if (disposing)
            {
                //Sql
                _transaction?.Dispose();
                _transaction = null;
                _connection?.Close();
                _connection?.Dispose();
                _connection = null;

                DisposeAllRepositories();
            }
        }

        private async Task disposeAsync(bool disposing)
        {
            if (disposing)
            {
                //Sql
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }

                if (_connection != null)
                {
                    await _connection.CloseAsync();
                    await _connection.DisposeAsync();
                    _connection = null;
                }

                DisposeAllRepositories();
            }
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task DisposeAsync()
        {
            // Dispose of unmanaged resources.
            await disposeAsync(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        private void DisposeAllRepositories()
        {

        }
        #endregion
    }
}