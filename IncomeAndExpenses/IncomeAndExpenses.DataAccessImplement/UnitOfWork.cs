using IncomeAndExpenses.DataAccessInterface;
using System.Data.Entity;

namespace IncomeAndExpenses.DataAccessImplement
{
    /// <summary>
    /// Represents class for database connection
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _db;

        /// <summary>
        /// Creates new UnitOfWork
        /// </summary>
        /// <param name="db">database context</param>
        public UnitOfWork(DbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Saves changes in database
        /// </summary>
        public void Save()
        {
            _db.SaveChanges();
        }

        /// <summary>
        /// Repository for entities
        /// </summary>
        /// <typeparam name="TId">Type of the entity Id field</typeparam>
        /// <typeparam name="T">Entity type</typeparam>
        /// <returns>IRepository interface</returns>
        public IRepository<TId, T> Repository<TId, T>() 
            where T : Entity<TId>
        {
            return new Repository<TId, T>(_db);
        }

        /// <summary>
        /// Repository for entities with integer Id field
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <returns>IRepository interface</returns>
        public IRepository<int, T> Repository<T>()
            where T : Entity<int>
        {
            return new Repository<int, T>(_db);
        }

        /// <summary>
        /// Disposes UnitOfWork
        /// </summary>
        public void Dispose() { }
    }
}