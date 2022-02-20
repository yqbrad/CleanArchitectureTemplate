using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Framework.Domain.BaseModels;
using Framework.Domain.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;

namespace YQB.Infrastructure.DataAccess._Common
{
    public abstract class BaseDbContext : DbContext
    {
        private IDbContextTransaction _transaction;
        private const string Schema = "dbo";

        public BaseDbContext(DbContextOptions options) : base(options) { }

        protected BaseDbContext() { }

        public void BeginTransaction()
            => _transaction = Database.BeginTransaction();

        public async Task BeginTransactionAsync()
            => _transaction = await Database.BeginTransactionAsync();

        public void RollbackTransaction()
        {
            if (_transaction is null)
                throw new NullReferenceException("Please call `BeginTransaction()` method first.");

            _transaction.Rollback();
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction is null)
                throw new NullReferenceException("Please call `BeginTransaction()` method first.");

            await _transaction.RollbackAsync();
        }

        public void CommitTransaction()
        {
            if (_transaction is null)
                throw new NullReferenceException("Please call `BeginTransaction()` method first.");

            _transaction.Commit();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction is null)
                throw new NullReferenceException("Please call `BeginTransaction()` method first.");

            await _transaction.CommitAsync();
        }

        public T GetShadowPropertyValue<T>(object entity, string propertyName) where T : IConvertible
        {
            var value = Entry(entity).Property(propertyName).CurrentValue;
            return value != null
                ? (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture)
                : default;
        }

        public object GetShadowPropertyValue(object entity, string propertyName)
            => Entry(entity).Property(propertyName).CurrentValue;

        protected override void OnModelCreating(ModelBuilder builder)
            => base.OnModelCreating(builder);

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            ChangeTracker.AutoDetectChangesEnabled = false;
            var result = base.SaveChanges();
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();
            ChangeTracker.AutoDetectChangesEnabled = false;
            var result = base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        public IEnumerable<string> GetIncludePaths(Type clrEntityType)
        {
            var entityType = Model.FindEntityType(clrEntityType);
            var includedNavigations = new HashSet<INavigation>();
            var stack = new Stack<IEnumerator<INavigation>>();
            while (true)
            {
                var entityNavigations = new List<INavigation>();
                foreach (var navigation in entityType.GetNavigations())
                {
                    if (includedNavigations.Add(navigation))
                        entityNavigations.Add(navigation);
                }
                if (entityNavigations.Count == 0)
                {
                    if (stack.Count > 0)
                        yield return string.Join(".", stack.Reverse().Select(e => e.Current.Name));
                }
                else
                {
                    foreach (var navigation in entityNavigations)
                    {
                        var inverseNavigation = navigation.FindInverse();
                        if (inverseNavigation != null)
                            includedNavigations.Add(inverseNavigation);
                    }
                    stack.Push(entityNavigations.GetEnumerator());
                }
                while (stack.Count > 0 && !stack.Peek().MoveNext())
                    stack.Pop();
                if (stack.Count == 0)
                    break;
                entityType = stack.Peek().Current.GetTargetType();
            }
        }

        internal T GetNextSequenceValue<T, TEntity>()
            where T : IEquatable<T>
            where TEntity : BaseAggregateRoot<T>
        {
            try
            {
                var sqlType = typeof(T) == typeof(int) ?
                    System.Data.SqlDbType.Int : System.Data.SqlDbType.BigInt;

                var result = new SqlParameter("result", sqlType)
                {
                    Direction = System.Data.ParameterDirection.Output
                };

                var name = typeof(TEntity).Name.Replace("Dao", "_Sequence");
                Database.ExecuteSqlRaw($"SELECT @result = (NEXT VALUE FOR {Schema}.{name})", result);
                return (T)result.Value;
            }
            catch (Exception e)
            {
                throw new DataAccessException(e);
            }
        }

        internal async Task<T> GetNextSequenceValueAsync<T, TEntity>()
            where T : IEquatable<T>
            where TEntity : BaseAggregateRoot<T>
        {
            try
            {
                var sqlType = typeof(T) == typeof(int) ?
                    System.Data.SqlDbType.Int : System.Data.SqlDbType.BigInt;

                var result = new SqlParameter("result", sqlType)
                {
                    Direction = System.Data.ParameterDirection.Output
                };

                var name = typeof(TEntity).Name + "_Sequence";
                await Database.ExecuteSqlRawAsync($"SELECT @result = (NEXT VALUE FOR {Schema}.{name})", result);
                return (T)result.Value;
            }
            catch (Exception e)
            {
                throw new DataAccessException(e);
            }
        }
    }
}