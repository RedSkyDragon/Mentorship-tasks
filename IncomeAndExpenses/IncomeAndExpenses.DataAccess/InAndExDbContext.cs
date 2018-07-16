using IncomeAndExpenses.Models;
using System.Data.Entity;


namespace IncomeAndExpenses.Web.Models
{
    public class InAndExDbContext: DbContext
    {
        public InAndExDbContext() : base("InAndExDbContext") { }

        public DbSet<User> Users { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<IncomeType> IncomeTypes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Income> Incomes { get; set; }
    }
}