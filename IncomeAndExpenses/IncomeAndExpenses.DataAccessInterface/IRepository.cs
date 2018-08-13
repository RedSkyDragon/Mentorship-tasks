using System.Collections.Generic;
using System.Linq;

namespace IncomeAndExpenses.DataAccessInterface
{
    /// <summary>
    /// Represents interface for work with entities
    /// </summary>
    /// <typeparam name="TId">Type of Id field</typeparam>
    /// <typeparam name="T">Entity type</typeparam>
    public interface IRepository<TId, T> 
        where T : EntityDM<TId>
    {
        /// <summary>
        /// Returns IQueryable interface for creating database queries
        /// </summary>
        /// <returns>IQueryable collection of entities</returns>
        IQueryable<T> All();

        /// <summary>
        /// Gets entity with requested Id
        /// </summary>
        /// <param name="id">Entity Id</param>
        /// <returns>Entity</returns>
        T Get(TId id);

        /// <summary>
        /// Creates entity in database
        /// </summary>
        /// <param name="item">Entity to create</param>
        void Create(T item);

        /// <summary>
        /// Updates requested entity in database
        /// </summary>
        /// <param name="item">Entity to update</param>
        void Update(T item);

        /// <summary>
        /// Deletes entity with requested Id from database
        /// </summary>
        /// <param name="id">Entity Id to delete</param>
        void Delete(TId id);
    }
}
