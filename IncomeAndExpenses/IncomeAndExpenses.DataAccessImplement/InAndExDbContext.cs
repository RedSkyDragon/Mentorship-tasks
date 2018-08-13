using IncomeAndExpenses.DataAccessInterface;
using System.Data.Entity;


namespace IncomeAndExpenses.DataAccessImplement
{
    public class InAndExDbContext: DbContext
    {
        public InAndExDbContext() : base("InAndExDbContext") { }

        /// <summary>
        /// Creates model and tunes properties in database
        /// </summary>
        /// <param name="modelBuilder">The model builder</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDM>().ToTable("Users");
            modelBuilder.Entity<UserDM>().Property(p => p.UserName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<UserDM>().HasMany(p => p.ExpenseTypes).WithRequired(p => p.User).WillCascadeOnDelete(true);
            modelBuilder.Entity<UserDM>().HasMany(p => p.IncomeTypes).WithRequired(p => p.User).WillCascadeOnDelete(true);

            modelBuilder.Entity<ExpenseTypeDM>().ToTable("ExpenseTypes");
            modelBuilder.Entity<ExpenseTypeDM>().Property(p=> p.UserId).IsRequired();
            modelBuilder.Entity<ExpenseTypeDM>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            modelBuilder.Entity<ExpenseTypeDM>().Property(p => p.Description).IsMaxLength();
            modelBuilder.Entity<ExpenseTypeDM>().HasMany(p => p.Expenses).WithRequired(p => p.ExpenseType).WillCascadeOnDelete(true);

            modelBuilder.Entity<IncomeTypeDM>().ToTable("IncomeTypes");
            modelBuilder.Entity<IncomeTypeDM>().Property(p => p.UserId).IsRequired();
            modelBuilder.Entity<IncomeTypeDM>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            modelBuilder.Entity<IncomeTypeDM>().Property(p => p.Description).IsMaxLength();
            modelBuilder.Entity<IncomeTypeDM>().HasMany(p => p.Incomes).WithRequired(p => p.IncomeType).WillCascadeOnDelete(true);

            modelBuilder.Entity<ExpenseDM>().ToTable("Expenses");
            modelBuilder.Entity<ExpenseDM>().Property(p=> p.Amount).IsRequired().HasPrecision(10,2);
            modelBuilder.Entity<ExpenseDM>().Property(p => p.Date).IsRequired();
            modelBuilder.Entity<ExpenseDM>().Property(p => p.Comment).IsMaxLength();

            modelBuilder.Entity<IncomeDM>().ToTable("Incomes");
            modelBuilder.Entity<IncomeDM>().Property(p => p.Amount).IsRequired().HasPrecision(10,2);
            modelBuilder.Entity<IncomeDM>().Property(p => p.Date).IsRequired();
            modelBuilder.Entity<IncomeDM>().Property(p => p.Comment).IsMaxLength();

            base.OnModelCreating(modelBuilder);
        }
    }
}