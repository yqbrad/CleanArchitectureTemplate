using Dapper;
using Helper.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DDD.Infrastructure.DataAccess._Base
{
    public abstract class BaseDapperRepository
    {
        protected IDbTransaction Transaction { get; }
        protected IDbConnection Connection => Transaction?.Connection;
        protected BaseDapperRepository(IDbTransaction transaction) => Transaction = transaction;

        protected virtual T PopupId<T>(string sequenceName)
        {
            try
            {
                return Connection.ExecuteScalar<T>
                       (
                           sql: $@"SELECT NEXT VALUE FOR {sequenceName}",
                           param: null,
                           commandType: CommandType.Text,
                           transaction: Transaction
                       );
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        protected IEnumerable<T> Query<T>(string procedureName, object param = null)
        {
            try
            {
                IEnumerable<T> entities =
                    Connection.Query<T>
                        (
                            sql: procedureName,
                            param: param,
                            commandType: CommandType.StoredProcedure,
                            transaction: Transaction
                        );
                return entities;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        protected SqlMapper.GridReader QueryMultiple(string sql, object param = null,
           CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                return Connection.QueryMultiple
                    (
                        sql: sql,
                        param: param,
                        commandType: commandType,
                        transaction: Transaction
                    );
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        protected T QueryFirstOrDefault<T>(string procedureName, object param = null)
        {
            try
            {
                T entity =
                    Connection.QueryFirstOrDefault<T>
                        (
                            sql: procedureName,
                            param: param,
                            commandType: CommandType.StoredProcedure,
                            transaction: Transaction
                        );
                return entity;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }

        }

        protected T QuerySingleOrDefault<T>(string procedureName, object param = null)
        {
            try
            {
                T entity =
                    Connection.QuerySingleOrDefault<T>
                        (
                            sql: procedureName,
                            param: param,
                            commandType: CommandType.StoredProcedure,
                            transaction: Transaction
                        );
                return entity;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }

        }

        /// <summary>
        /// Execute a stored procedure and return rows affected.
        /// </summary>
        /// <param name="procedureName">Stored procedure name</param>
        /// <param name="param">Stored procedure parameters</param>
        /// <returns>Integer as affected rows</returns>
        /// <remarks>Must call 'Commit()' at the end to commit changes to the database.</remarks>
        protected int Execute(string procedureName, object param = null)
        {
            try
            {
                return Connection.Execute
                    (
                    sql: procedureName,
                    param: param,
                    commandType: CommandType.StoredProcedure,
                    transaction: Transaction
                    );
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>
        /// Execute query and return first cell of first row
        /// </summary>
        /// <typeparam name="T">Type of result</typeparam>
        /// <param name="procedureName">Stored procedure name</param>
        /// <param name="param">Stored procedure parameters</param>
        /// <returns>First cell of first row</returns>
        protected T ExecuteScalar<T>(string procedureName, object param = null)
        where T : struct
        {
            try
            {
                T result =
                    Connection.ExecuteScalar<T>
                        (
                            sql: procedureName,
                            param: param,
                            commandType: CommandType.StoredProcedure,
                            transaction: Transaction
                        );
                return result;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        protected virtual async Task<T> PopupIdAsync<T>(string sequenceName)
        {
            try
            {
                return await Connection.ExecuteScalarAsync<T>
                       (
                           sql: $@"SELECT NEXT VALUE FOR {sequenceName}",
                           param: null,
                           commandType: CommandType.Text,
                           transaction: Transaction
                       );
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        protected async Task<IEnumerable<T>> QueryAsync<T>(string procedureName,
            object param = null)
        {
            try
            {
                var entities = await
                    Connection.QueryAsync<T>
                        (
                            sql: procedureName,
                            param: param,
                            commandType: CommandType.StoredProcedure,
                            transaction: Transaction
                        );
                return entities;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        protected async Task<SqlMapper.GridReader> QueryMultipleAsync(string sql,
            object param = null, CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                return await Connection.QueryMultipleAsync
                    (
                        sql: sql,
                        param: param,
                        commandType: commandType,
                        transaction: Transaction
                    );
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }

        }

        protected async Task<T> QueryFirstOrDefaultAsync<T>(string procedureName,
            object param = null)
        {
            try
            {
                T entity = await
                    Connection.QueryFirstOrDefaultAsync<T>
                        (
                            sql: procedureName,
                            param: param,
                            commandType: CommandType.StoredProcedure,
                            transaction: Transaction
                        );
                return entity;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }

        }

        protected async Task<T> QuerySingleOrDefaultAsync<T>(string procedureName,
            object param = null)
        {
            try
            {
                T entity = await
                    Connection.QuerySingleOrDefaultAsync<T>
                        (
                            sql: procedureName,
                            param: param,
                            commandType: CommandType.StoredProcedure,
                            transaction: Transaction
                        );
                return entity;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        protected async Task<int> ExecuteAsync(string procedureName, object param = null)
        {
            try
            {
                var result = await
                    Connection.ExecuteAsync
                        (
                            sql: procedureName,
                            param: param,
                            commandType: CommandType.StoredProcedure,
                            transaction: Transaction
                        );
                return result;

            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        protected async Task<T> ExecuteScalarAsync<T>(string procedureName, object param = null)
        where T : struct
        {
            try
            {
                T result = await
                    Connection.ExecuteScalarAsync<T>
                        (
                            sql: procedureName,
                            param: param,
                            commandType: CommandType.StoredProcedure,
                            transaction: Transaction
                        );
                return result;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }
    }
}