namespace IncomeAndExpenses.DataAccessInterface
{
    public abstract class Entity<TId>
    {
        public TId Id { get; set; }
    }
}
