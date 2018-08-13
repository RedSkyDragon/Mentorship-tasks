namespace IncomeAndExpenses.DataAccessInterface
{
    /// <summary>
    /// Represents base entity from database
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public abstract class EntityDM<TId>
    {
        /// <summary>
        /// Gets or sets Id field
        /// </summary>
        public TId Id { get; set; }
    }
}
