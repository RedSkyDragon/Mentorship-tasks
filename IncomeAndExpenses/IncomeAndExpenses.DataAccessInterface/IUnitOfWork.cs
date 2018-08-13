using System;

namespace IncomeAndExpenses.DataAccessInterface
{
    /// <summary>
    /// Represents interface for database connection
    /// </summary>
    public interface IUnitOfWork: IDisposable
    {
        /// <summary>
        /// Represents repository for entities
        /// </summary>
        /// <typeparam name="TId">Type of the entity Id field</typeparam>
        /// <typeparam name="T">Entity type</typeparam>
        /// <returns>IRepository interface</returns>
        IRepository<TId, T> Repository<TId, T>() 
            where T: EntityDM<TId>;

        /// <summary>
        /// Represents repository for entities with integer Id field
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <returns>IRepository interface</returns>
        IRepository<int, T> Repository<T>()
            where T : EntityDM<int>;

        /// <summary>
        /// Saves changes in database
        /// </summary>
        void Save();
    }
}
