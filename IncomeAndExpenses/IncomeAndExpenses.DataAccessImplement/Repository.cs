using IncomeAndExpenses.DataAccessInterface;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace IncomeAndExpenses.DataAccessImplement
{
    /// <summary>
    /// Represents repository to work with entities
    /// </summary>
    /// <typeparam name="TId">Type of Id field</typeparam>
    /// <typeparam name="T">Entity type</typeparam>
    public class Repository<TId, T> : IRepository<TId, T> 
        where T: Entity<TId>
    {
        private DbContext _db;

        /// <summary>
        /// Creates new Repository
        /// </summary>
        /// <param name="db">database context</param>
        public Repository(DbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Creates entity in database
        /// </summary>
        /// <param name="item">Entity to create</param>
        public void Create(T item)
        {
            _db.Set<T>().Add(item);
        }

        /// <summary>
        /// Deletes entity with requested Id from database
        /// </summary>
        /// <param name="id">Entity Id to delete</param>
        public void Delete(TId id)
        {
            T item = _db.Set<T>().Find(id);
            if (item != null)
            {
                _db.Set<T>().Remove(item);
            }
        }

        /// <summary>
        /// Gets entity with requested Id
        /// </summary>
        /// <param name="id">Entity Id</param>
        /// <returns>Entity</returns>
        public T Get(TId id)
        {
            return _db.Set<T>().Find(id);
        }

        /// <summary>
        /// Returns IQueryable interface for creating database queries
        /// </summary>
        /// <returns>IQueryable collection of entities</returns>
        public IQueryable<T> All()
        {
            return _db.Set<T>();
        }

        /// <summary>
        /// Updates requested entity in database
        /// </summary>
        /// <param name="item">Entity to update</param>
        public void Update(T item)
        {
            var last_vers = _db.Set<T>().Find(item.Id);
            if (item != null)
            {
                _db.Entry(last_vers).CurrentValues.SetValues(item);
            }
        }
    }
}
