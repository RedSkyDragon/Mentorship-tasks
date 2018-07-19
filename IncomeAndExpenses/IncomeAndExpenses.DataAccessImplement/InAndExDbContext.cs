using IncomeAndExpenses.DataAccessInterface;
using System.Data.Entity;


namespace IncomeAndExpenses.DataAccessImplement
{
    public class InAndExDbContext: DbContext
    {
        public InAndExDbContext() : base("InAndExDbContext") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(p => p.UserName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<User>().HasMany(p => p.ExpenseTypes).WithRequired(p => p.User).WillCascadeOnDelete(true);
            modelBuilder.Entity<User>().HasMany(p => p.IncomeTypes).WithRequired(p => p.User).WillCascadeOnDelete(true);

            modelBuilder.Entity<ExpenseType>().Property(p=> p.UserId).IsRequired();
            modelBuilder.Entity<ExpenseType>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            modelBuilder.Entity<ExpenseType>().Property(p => p.Description).IsMaxLength();
            modelBuilder.Entity<ExpenseType>().HasMany(p => p.Expenses).WithRequired(p => p.ExpenseType).WillCascadeOnDelete(true);

            modelBuilder.Entity<IncomeType>().Property(p => p.UserId).IsRequired();
            modelBuilder.Entity<IncomeType>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            modelBuilder.Entity<IncomeType>().Property(p => p.Description).IsMaxLength();
            modelBuilder.Entity<IncomeType>().HasMany(p => p.Incomes).WithRequired(p => p.IncomeType).WillCascadeOnDelete(true);

            modelBuilder.Entity<Expense>().Property(p=> p.Amount).IsRequired().HasPrecision(10,2);
            modelBuilder.Entity<Expense>().Property(p => p.Date).IsRequired();
            modelBuilder.Entity<Expense>().Property(p => p.Comment).IsMaxLength();

            modelBuilder.Entity<Income>().Property(p => p.Amount).IsRequired().HasPrecision(10,2);
            modelBuilder.Entity<Income>().Property(p => p.Date).IsRequired();
            modelBuilder.Entity<Income>().Property(p => p.Comment).IsMaxLength();

            base.OnModelCreating(modelBuilder);
        }
    }
}