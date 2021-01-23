using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Helper.Exceptions.Exceptions;
using Microsoft.Data.SqlClient;
using RepoDb;

namespace DDD.Infrastructure.DataAccess._Base
{
    public abstract class BaseRepoDbRepository
    {
        protected SqlTransaction Transaction { get; }
        protected SqlConnection Connection => Transaction?.Connection;
        protected BaseRepoDbRepository(SqlTransaction transaction) => Transaction = transaction;

        /// <summary>Popups the identifier.</summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected virtual T PopupId<T, TEntity>()
            where TEntity : class
        {
            try
            {
                return Connection.ExecuteScalar<T>
                (
                    commandText: $@"SELECT NEXT VALUE FOR {ClassMappedNameCache.Get<TEntity>().TrimEnd(']')}Sequence]",
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

        /// <summary>Inserts the specified entity.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected object Insert<T>(T entity)
            where T : class
        {
            try
            {
                var result =
                    Connection.Insert<T>
                        (entity,
                        transaction: Transaction);
                return result;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Inserts all.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected object InsertAll<T>(IEnumerable<T> entities)
            where T : class
        {
            try
            {
                var result =
                    Connection.InsertAll<T>
                    (entities,
                        transaction: Transaction);
                return result;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Bulks the insert.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected object BulkInsert<T>(IEnumerable<T> entities)
            where T : class
        {
            try
            {
                var result =
                    Connection.BulkInsert<T>
                    (entities,
                        transaction: Transaction);
                return result;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Updates the specified entity.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected object Update<T>(T entity)
            where T : class
        {
            try
            {
                var result =
                    Connection.Update<T>
                    (
                        entity,
                        transaction: Transaction
                    );

                return result;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Updates all.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected object UpdateAll<T>(IEnumerable<T> entities)
            where T : class
        {
            try
            {
                var result =
                    Connection.UpdateAll<T>
                    (
                        entities,
                        transaction: Transaction
                    );

                return result;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Updates the specified entity.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="whereOrPrimaryKey">The where or primary key.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected object Update<T>(object entity, object whereOrPrimaryKey)
        where T : class
        {
            try
            {
                var result = Connection.Update
                    (
                    ClassMappedNameCache.Get<T>(),
                        entity,
                        whereOrPrimaryKey,
                        transaction: Transaction
                    );

                return result;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Bulks the update.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected object BulkUpdate<T>(IEnumerable<T> entities)
            where T : class
        {
            try
            {
                var result =
                    Connection.BulkUpdate<T>
                    (
                        entities,
                        transaction: Transaction
                    );

                return result;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Deletes the specified entity.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected object Delete<T>(T entity)
            where T : class
        {
            try
            {
                var result =
                    Connection.Delete<T>
                    (
                        entity,
                        transaction: Transaction
                    );

                return result;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Deletes all.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected object DeleteAll<T>(IEnumerable<T> entities)
            where T : class
        {
            try
            {
                var result =
                    Connection.DeleteAll<T>
                    (
                        entities,
                        transaction: Transaction
                    );

                return result;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Bulks the delete.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected object BulkDelete<T>(IEnumerable<T> entities)
            where T : class
        {
            try
            {
                var result =
                    Connection.BulkDelete<T>
                    (
                        entities,
                        transaction: Transaction
                    );

                return result;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Existses the specified where.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected object Exists<T>(Expression<Func<T, bool>> where = null)
            where T : class
        {
            try
            {
                var entities = Connection.Exists<T>
                (
                    where,
                    transaction: Transaction
                );

                return entities;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Counts the specified where.</summary>
        /// <typeparam name="T">
        ///   <br />
        /// </typeparam>
        /// <param name="where">The where.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="DataAccessException">
        ///   <br />
        /// </exception>
        protected object Count<T>(Expression<Func<T, bool>> where = null)
            where T : class
        {
            try
            {
                var entities = Connection.Count<T>
                    (
                        where,
                        transaction: Transaction
                    );

                return entities;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Determines the maximum of the parameters.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">The expression.</param>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected object Max<T>(Expression<Func<T, object>> expression,
            Expression<Func<T, bool>> where = null)
            where T : class
        {
            try
            {
                var entities = Connection.Max<T>
                    (
                        expression,
                        where,
                        transaction: Transaction
                    );

                return entities;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Queries the specified expression.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">The expression.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="top">The top.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected IEnumerable<T> Query<T>(Expression<Func<T, bool>> expression,
            IEnumerable<OrderField> orderBy = null,
            int top = 0)
            where T : class
        {
            try
            {
                var entities =
                    Connection.Query<T>
                    (
                        expression,
                        orderBy,
                        top,
                        transaction: Transaction
                    );

                return entities;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Queries the specified object.</summary>
        /// <typeparam name="T">
        ///   <br />
        /// </typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="top">The top.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="DataAccessException">
        ///   <br />
        /// </exception>
        protected IEnumerable<T> Query<T>(IEnumerable<QueryField> where,
            IEnumerable<OrderField> orderBy = null,
            int top = 0)
            where T : class
        {
            try
            {
                var entities =
                    Connection.Query<T>
                    (
                        where,
                        orderBy,
                        top,
                        transaction: Transaction
                    );

                return entities;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Queries the specified procedure name.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected IEnumerable<T> Query<T>(string procedureName, object param = null)
            where T : class
        {
            try
            {
                var entities =
                    Connection.ExecuteQuery<T>
                        (
                            commandText: procedureName,
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

        /// <summary>Executes the query.</summary>
        /// <typeparam name="T">
        ///   <br />
        /// </typeparam>
        /// <param name="text">The text.</param>
        /// <param name="param">The parameter.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="DataAccessException">
        ///   <br />
        /// </exception>
        protected IEnumerable<T> ExecuteQuery<T>(string text, object param = null)
            where T : class
        {
            try
            {
                var entities = Connection.ExecuteQuery<T>
                    (
                        commandText: text,
                        param: param,
                        commandType: CommandType.Text,
                        transaction: Transaction
                    );
                return entities;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Batches the query.</summary>
        /// <typeparam name="T">
        ///   <br />
        /// </typeparam>
        /// <param name="page">The page.</param>
        /// <param name="rowPerBatch">The row per batch.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="DataAccessException">
        ///   <br />
        /// </exception>
        protected IEnumerable<T> BatchQuery<T>(int page, int rowPerBatch,
            Expression<Func<T, bool>> expression,
            IEnumerable<OrderField> orderBy = null)
            where T : class
        {
            try
            {
                var entities =
                    Connection.BatchQuery<T>
                    (
                        page,
                        rowPerBatch,
                        orderBy,
                        expression,
                        transaction: Transaction
                    );

                return entities;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Batches the query.</summary>
        /// <typeparam name="T">
        ///   <br />
        /// </typeparam>
        /// <param name="page">The page.</param>
        /// <param name="rowPerBatch">The row per batch.</param>
        /// <param name="where">The where.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="DataAccessException">
        ///   <br />
        /// </exception>
        protected IEnumerable<T> BatchQuery<T>(int page, int rowPerBatch, IEnumerable<QueryField> where,
            IEnumerable<OrderField> orderBy = null)
            where T : class
        {
            try
            {
                var entities =
                    Connection.BatchQuery<T>
                    (
                        page,
                        rowPerBatch,
                        orderBy,
                        where,
                        transaction: Transaction
                    );

                return entities;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Queries all.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="orderBy">The order by.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected IEnumerable<T> QueryAll<T>(IEnumerable<OrderField> orderBy = null)
            where T : class
        {
            try
            {
                var entities =
                    Connection.QueryAll<T>
                    (
                        orderBy,
                        transaction: Transaction
                    );

                return entities;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Queries the multiple.</summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="param">The parameter.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected QueryMultipleExtractor QueryMultiple(string sql, object param = null,
           CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                return Connection.ExecuteQueryMultiple
                    (
                        commandText: sql,
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

        /// <summary>Queries the first or default.</summary>
        /// <typeparam name="T">
        ///   <br />
        /// </typeparam>
        /// <param name="whereOrPrimaryKey">The where or primary key.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="DataAccessException">
        ///   <br />
        /// </exception>
        protected T QueryFirstOrDefault<T>(object whereOrPrimaryKey,
            IEnumerable<OrderField> orderBy = null)
            where T : class
        {
            try
            {
                var entities =
                    Connection.Query<T>
                    (
                        whereOrPrimaryKey,
                        orderBy,
                        transaction: Transaction
                    );

                return entities.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Queries the first or default.</summary>
        /// <typeparam name="T">
        ///   <br />
        /// </typeparam>
        /// <param name="expression">The expression.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="top">The top.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="DataAccessException">
        ///   <br />
        /// </exception>
        protected T QueryFirstOrDefault<T>(Expression<Func<T, bool>> expression,
            IEnumerable<OrderField> orderBy = null)
            where T : class
        {
            try
            {
                var entities =
                    Connection.Query<T>
                    (
                        expression,
                        orderBy,
                        transaction: Transaction
                    );

                return entities.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Queries the first or default.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected T QueryFirstOrDefault<T>(string procedureName, object param = null)
            where T : class
        {
            try
            {
                T entity =
                    Connection.ExecuteQuery<T>
                        (
                            commandText: procedureName,
                            param: param,
                            commandType: CommandType.StoredProcedure,
                            transaction: Transaction
                        ).FirstOrDefault();
                return entity;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }

        }

        /// <summary>Queries the single or default.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected T QuerySingleOrDefault<T>(string procedureName, object param = null)
            where T : class
        {
            try
            {
                T entity =
                    Connection.ExecuteQuery<T>
                        (
                            commandText: procedureName,
                            param: param,
                            commandType: CommandType.StoredProcedure,
                            transaction: Transaction
                        ).SingleOrDefault();
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
                return Connection.ExecuteNonQuery
                    (
                    commandText: procedureName,
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
                            commandText: procedureName,
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

        /// <summary>Popups the identifier asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected virtual async Task<T> PopupIdAsync<T, TEntity>()
            where TEntity : class
        {
            try
            {
                return await Connection.ExecuteScalarAsync<T>
                (
                    commandText: $@"SELECT NEXT VALUE FOR {ClassMappedNameCache.Get<TEntity>().TrimEnd(']')}Sequence]",
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

        /// <summary>Inserts the asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected async Task<object> InsertAsync<T>(T entity)
            where T : class
        {
            try
            {
                var result = await
                    Connection.InsertAsync<T>
                        (entity,
                        transaction: Transaction);
                return result;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Inserts all asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected async Task<object> InsertAllAsync<T>(IEnumerable<T> entities)
            where T : class
        {
            try
            {
                var result = await
                    Connection.InsertAllAsync<T>
                    (entities,
                        transaction: Transaction);
                return result;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Bulks the insert asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected async Task<object> BulkInsertAsync<T>(IEnumerable<T> entities)
            where T : class
        {
            try
            {
                var result = await
                    Connection.BulkInsertAsync<T>
                        (entities,
                        transaction: Transaction);

                return result;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Updates the asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected async Task<object> UpdateAsync<T>(T entity)
            where T : class
        {
            try
            {
                var result = await
                    Connection.UpdateAsync<T>
                    (
                        entity,
                        transaction: Transaction
                    );

                return result;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Updates all asynchronous.</summary>
        /// <typeparam name="T">
        ///   <br />
        /// </typeparam>
        /// <param name="entities">The entities.</param>
        /// <param name="fields">The fields.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="DataAccessException">
        ///   <br />
        /// </exception>
        protected async Task<object> UpdateAllAsync<T>(IEnumerable<T> entities, IEnumerable<Field> fields = null)
            where T : class
        {
            try
            {
                var result = await
                    Connection.UpdateAllAsync
                    (
                        ClassMappedNameCache.Get<T>(),
                        entities,
                        fields: fields,
                        transaction: Transaction
                    );

                return result;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }
        /// <summary>Updates the asynchronous.</summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="whereOrPrimaryKey">The where or primary key.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected async Task<object> UpdateAsync<T>(object entity, object whereOrPrimaryKey)
        where T : class
        {
            try
            {
                var result = await
                    Connection.UpdateAsync
                    (
                        ClassMappedNameCache.Get<T>(),
                        entity,
                        whereOrPrimaryKey,
                        transaction: Transaction
                    );

                return result;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Updates the asynchronous.</summary>
        /// <typeparam name="T">
        ///   <br />
        /// </typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="where">The where.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="DataAccessException">
        ///   <br />
        /// </exception>
        protected async Task<object> UpdateAsync<T>(object entity, Expression<Func<T, bool>> where)
            where T : class
        {
            try
            {
                var result = await
                    Connection.UpdateAsync
                    (
                        ClassMappedNameCache.Get<T>(),
                        entity,
                        where,
                        transaction: Transaction
                    );

                return result;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Bulks the update asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected async Task<object> BulkUpdateAsync<T>(IEnumerable<T> entities)
            where T : class
        {
            try
            {
                var result = await
                    Connection.BulkUpdateAsync<T>
                    (entities,
                        transaction: Transaction);

                return result;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Deletes the asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected async Task<object> DeleteAsync<T>(T entity)
            where T : class
        {
            try
            {
                var result = await
                    Connection.DeleteAsync<T>
                    (
                        entity,
                        transaction: Transaction
                    );

                return result;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Deletes the asynchronous.</summary>
        /// <typeparam name="T">
        ///   <br />
        /// </typeparam>
        /// <param name="whereOrPrimaryKey">The where or primary key.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="DataAccessException">
        ///   <br />
        /// </exception>
        protected async Task<object> DeleteAsync<T>(object whereOrPrimaryKey)
            where T : class
        {
            try
            {
                var result = await
                    Connection.DeleteAsync<T>
                    (
                        whereOrPrimaryKey,
                        transaction: Transaction
                    );

                return result;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Deletes all asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected async Task<object> DeleteAllAsync<T>(IEnumerable<T> entities)
            where T : class
        {
            try
            {
                var result = await
                    Connection.DeleteAllAsync<T>
                    (
                        entities,
                        transaction: Transaction
                    );

                return result;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Deletes all asynchronous.</summary>
        /// <typeparam name="T">
        ///   <br />
        /// </typeparam>
        /// <param name="primaryKeys">The primaryKeys.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="DataAccessException">
        ///   <br />
        /// </exception>
        protected async Task<object> DeleteAllAsync<T>(IEnumerable<object> primaryKeys)
            where T : class
        {
            try
            {
                var result = await
                    Connection.DeleteAllAsync<T>
                    (
                        primaryKeys,
                        transaction: Transaction
                    );

                return result;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Bulks the delete asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected async Task<object> BulkDeleteAsync<T>(IEnumerable<T> entities)
            where T : class
        {
            try
            {
                var result = await
                    Connection.BulkDeleteAsync<T>
                    (entities,
                        transaction: Transaction);

                return result;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Existses the asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected async Task<bool> ExistsAsync<T>(Expression<Func<T, bool>> where = null)
            where T : class
        {
            try
            {
                var entities = await Connection.ExistsAsync<T>
                (
                    where,
                    transaction: Transaction
                );

                return entities;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Maximums the asynchronous.</summary>
        /// <typeparam name="T">
        ///   <br />
        /// </typeparam>
        /// <param name="where">The where.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="DataAccessException">
        ///   <br />
        /// </exception>
        protected async Task<object> CountAsync<T>(Expression<Func<T, bool>> where = null)
            where T : class
        {
            try
            {
                var entities = await
                    Connection.CountAsync<T>
                    (
                        where,
                        transaction: Transaction
                    );

                return entities;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Maximums the asynchronous.</summary>
        /// <typeparam name="T">
        ///   <br />
        /// </typeparam>
        /// <param name="expression">The expression.</param>
        /// <param name="where">The where.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="DataAccessException">
        ///   <br />
        /// </exception>
        protected async Task<object> MaxAsync<T>(Expression<Func<T, object>> expression,
            Expression<Func<T, bool>> where = null)
            where T : class
        {
            try
            {
                var entities = await
                    Connection.MaxAsync<T>
                    (
                        expression,
                        where,
                        transaction: Transaction
                    );

                return entities;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Queries the asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">The expression.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="top">The top.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected async Task<IEnumerable<T>> QueryAsync<T>(Expression<Func<T, bool>> expression,
            IEnumerable<OrderField> orderBy = null,
            int top = 0)
            where T : class
        {
            try
            {
                var entities = await
                    Connection.QueryAsync<T>
                    (
                        expression,
                        orderBy,
                        top,
                        transaction: Transaction
                    );

                return entities;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Queries the asynchronous.</summary>
        /// <typeparam name="T">
        ///   <br />
        /// </typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="top">The top.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="DataAccessException">
        ///   <br />
        /// </exception>
        protected async Task<IEnumerable<T>> QueryAsync<T>(QueryGroup obj,
            IEnumerable<OrderField> orderBy = null,
            int top = 0)
            where T : class
        {
            try
            {
                var entities = await
                    Connection.QueryAsync<T>
                    (
                        obj,
                        orderBy,
                        top,
                        transaction: Transaction
                    );

                return entities;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Queries the asynchronous.</summary>
        /// <typeparam name="T">
        ///   <br />
        /// </typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="top">The top.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="DataAccessException">
        ///   <br />
        /// </exception>
        protected async Task<IEnumerable<T>> QueryAsync<T>(IEnumerable<QueryField> obj,
            IEnumerable<OrderField> orderBy = null,
            int top = 0)
            where T : class
        {
            try
            {
                var entities = await
                    Connection.QueryAsync<T>
                    (
                        obj,
                        orderBy,
                        top,
                        transaction: Transaction
                    );

                return entities;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Queries the asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected async Task<IEnumerable<T>> QueryAsync<T>(string procedureName,
            object param = null)
        where T : class
        {
            try
            {
                var entities = await
                    Connection.ExecuteQueryAsync<T>
                        (
                            commandText: procedureName,
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

        /// <summary>Executes the query asynchronous.</summary>
        /// <typeparam name="T">
        ///   <br />
        /// </typeparam>
        /// <param name="text">The text.</param>
        /// <param name="param">The parameter.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="DataAccessException">
        ///   <br />
        /// </exception>
        protected async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string text, object param = null)
            where T : class
        {
            try
            {
                var entities = await
                    Connection.ExecuteQueryAsync<T>
                    (
                        commandText: text,
                        param: param,
                        commandType: CommandType.Text,
                        transaction: Transaction
                    );
                return entities;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Batches the query asynchronous.</summary>
        /// <typeparam name="T">
        ///   <br />
        /// </typeparam>
        /// <param name="page">The page.</param>
        /// <param name="rowPerBatch">The row per batch.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="DataAccessException">
        ///   <br />
        /// </exception>
        protected async Task<IEnumerable<T>> BatchQueryAsync<T>(int page, int rowPerBatch,
            Expression<Func<T, bool>> expression,
            IEnumerable<OrderField> orderBy = null)
            where T : class
        {
            try
            {
                var entities = await
                    Connection.BatchQueryAsync<T>
                    (
                        page,
                        rowPerBatch,
                        orderBy,
                        expression,
                        transaction: Transaction
                    );

                return entities;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Batches the query asynchronous.</summary>
        /// <typeparam name="T">
        ///   <br />
        /// </typeparam>
        /// <param name="page">The page.</param>
        /// <param name="rowPerBatch">The row per batch.</param>
        /// <param name="where">The where.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="DataAccessException">
        ///   <br />
        /// </exception>
        protected async Task<IEnumerable<T>> BatchQueryAsync<T>(int page, int rowPerBatch, IEnumerable<QueryField> where,
            IEnumerable<OrderField> orderBy = null)
            where T : class
        {
            try
            {
                var entities = await
                    Connection.BatchQueryAsync<T>
                    (
                        page,
                        rowPerBatch,
                        orderBy,
                        where,
                        transaction: Transaction
                    );

                return entities;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Queries all asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="orderBy">The order by.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected async Task<IEnumerable<T>> QueryAllAsync<T>(IEnumerable<OrderField> orderBy = null)
            where T : class
        {
            try
            {
                var entities = await
                    Connection.QueryAllAsync<T>
                    (
                        orderBy,
                        transaction: Transaction
                    );

                return entities;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Queries the multiple asynchronous.</summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="param">The parameter.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected async Task<QueryMultipleExtractor> QueryMultipleAsync(string sql,
            object param = null, CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                return await Connection.ExecuteQueryMultipleAsync
                    (
                        commandText: sql,
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

        /// <summary>Queries the first or default asynchronous.</summary>
        /// <typeparam name="T">
        ///   <br />
        /// </typeparam>
        /// <param name="whereOrPrimaryKey">The where or primary key.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="DataAccessException">
        ///   <br />
        /// </exception>
        protected async Task<T> QueryFirstOrDefaultAsync<T>(object whereOrPrimaryKey,
            IEnumerable<OrderField> orderBy = null)
            where T : class
        {
            try
            {
                var entities = await
                    Connection.QueryAsync<T>
                    (
                        whereOrPrimaryKey,
                        orderBy,
                        transaction: Transaction
                    );

                return entities.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Queries the first or default asynchronous.</summary>
        /// <typeparam name="T">
        ///   <br />
        /// </typeparam>
        /// <param name="expression">The expression.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="DataAccessException">
        ///   <br />
        /// </exception>
        protected async Task<T> QueryFirstOrDefaultAsync<T>(Expression<Func<T, bool>> expression,
            IEnumerable<OrderField> orderBy = null)
            where T : class
        {
            try
            {
                var entities = await
                    Connection.QueryAsync<T>
                    (
                        expression,
                        orderBy,
                        transaction: Transaction
                    );

                return entities.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Queries the first or default asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected async Task<T> QueryFirstOrDefaultAsync<T>(string procedureName,
            object param = null)
            where T : class
        {
            try
            {
                var entity = await
                    Connection.ExecuteQueryAsync<T>
                        (
                            commandText: procedureName,
                            param: param,
                            commandType: CommandType.StoredProcedure,
                            transaction: Transaction
                        );
                return entity.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }

        }

        /// <summary>Queries the single or default asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected async Task<T> QuerySingleOrDefaultAsync<T>(string procedureName,
            object param = null)
            where T : class

        {
            try
            {
                var entity = await
                    Connection.ExecuteQueryAsync<T>
                        (
                            commandText: procedureName,
                            param: param,
                            commandType: CommandType.StoredProcedure,
                            transaction: Transaction
                        );
                return entity.SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        /// <summary>Executes the asynchronous.</summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected async Task<int> ExecuteAsync(string procedureName, object param = null)
        {
            try
            {
                var result = await
                    Connection.ExecuteNonQueryAsync
                        (
                            commandText: procedureName,
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

        /// <summary>Executes the scalar asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        /// <exception cref="DataAccessException"></exception>
        protected async Task<T> ExecuteScalarAsync<T>(string procedureName, object param = null)
        where T : struct
        {
            try
            {
                T result = await
                    Connection.ExecuteScalarAsync<T>
                        (
                            commandText: procedureName,
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
