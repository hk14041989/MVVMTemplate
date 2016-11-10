using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTemplate.DAL.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {

        /// <summary>
        /// Gets all objects from database
        /// </summary>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Find object by Expression.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// Create a new object to database.
        /// </summary>
        /// <param name="t">Specified a new object to create.</param>
        void Create(TEntity t);

        /// <summary>
        /// Delete the object from database.
        /// </summary>
        /// <param name="t">Specified a existing object to delete.</param>        
        void Delete(TEntity t);

        /// <summary>
        /// Update object changes and save to database.
        /// </summary>
        /// <param name="t">Specified the object to save.</param>
        void Update(TEntity t);

        /// <summary>
        /// Update objects changes and save to database.
        /// </summary>
        /// <param name="entities">Specified the object to save.</param>
        void AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        void UpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        void RemoveRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        TEntity SingleOrDefault();

        /// <summary>
        /// Inserted Or Updated records
        /// </summary>
        /// <param name="entities"></param>
        void AddOrUpdate(IEnumerable<TEntity> entities);

        IEnumerable<TEntity> Execute(string execQuery, params object[] parameters);

        int ExecuteNonQuery(string execQuery, params object[] parameters);
        int ExecuteNonQuery(TransactionalBehavior transactionalBehavior, string execQuery, params object[] parameters);
        int ExecuteSqlCommand(TransactionalBehavior transactionalBehavior, string execQuery, params object[] parameters);
        bool BulkCopy(string tableName, DataTable dataTable);
    }
}
