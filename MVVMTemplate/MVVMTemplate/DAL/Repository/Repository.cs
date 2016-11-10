using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTemplate.DAL.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public IEnumerable<TEntity> GetAll()
        {
            try
            {
                return Context.Set<TEntity>().ToList();
            }
            catch (Exception)
            {
                //Exeption when alter database
                return Context.Set<TEntity>().ToList();
            }
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public void Create(TEntity t)
        {
            Context.Set<TEntity>().Add(t);
            Context.SaveChanges();
        }

        public void Delete(TEntity t)
        {
            var entry = Context.Entry(t);
            entry.State = EntityState.Deleted;
            Context.Set<TEntity>().Remove(t);
            Context.SaveChanges();
        }

        public void Update(TEntity t)
        {
            var entry = Context.Entry(t);
            Context.Set<TEntity>().Attach(t);
            entry.State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            foreach (var t in entities)
            {
                var entry = Context.Entry(t);
                Context.Set<TEntity>().Attach(t);
                entry.State = EntityState.Modified;
                Context.SaveChanges();
            }
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
            Context.SaveChanges();
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
            Context.SaveChanges();
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().SingleOrDefault(predicate);
        }

        public TEntity SingleOrDefault()
        {
            return Context.Set<TEntity>().SingleOrDefault();
        }

        public void AddOrUpdate(IEnumerable<TEntity> entities)
        {
            foreach (var t in entities)
            {
                Context.Set<TEntity>().AddOrUpdate(t);
                Context.SaveChanges();
            }
        }

        // Execute Store Procedure return a List<T>
        public IEnumerable<TEntity> Execute(string execQuery, params object[] parameters)
        {
            try
            {
                return Context.Database.SqlQuery<TEntity>(execQuery, parameters).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        // Execute Store Procedure return result of CRUD command
        public int ExecuteNonQuery(string execQuery, params object[] parameters)
        {
            try
            {
                return Context.Database.ExecuteSqlCommand(execQuery, parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Execute SqlCommand With TransactionalBehavior
        public int ExecuteNonQuery(TransactionalBehavior transactionalBehavior, string execQuery, params object[] parameters)
        {
            try
            {
                return Context.Database.ExecuteSqlCommand(transactionalBehavior, execQuery, parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Execute SqlCommand With TransactionalBehavior
        public int ExecuteSqlCommand(TransactionalBehavior transactionalBehavior, string execQuery, params object[] parameters)
        {
            try
            {
                string[] commands = execQuery.Split(new string[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);
                DbConnection conn = Context.Database.Connection; // Get Database connection
                ConnectionState initialState = conn.State;
                try
                {
                    if (initialState != ConnectionState.Open)
                        conn.Open();  // open connection if not already open

                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        // Iterate the string array and execute each one.
                        foreach (string thisCommand in commands)
                        {
                            cmd.CommandText = thisCommand;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                finally
                {
                    if (initialState != ConnectionState.Open)
                        conn.Close(); // only close connection if not initially open
                }
            }
            catch (Exception)
            {
                return 0;
            }
            return 1;
        }

        public bool BulkCopy(string tableName, DataTable dataTable)
        {
            try
            {
                using (SqlConnection conn = (SqlConnection)Context.Database.Connection)
                {
                    SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.UseInternalTransaction, null);
                    bulkCopy.DestinationTableName = tableName;
                    conn.Open();
                    bulkCopy.WriteToServer(dataTable);
                    dataTable.Rows.Clear();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }


    }
}
