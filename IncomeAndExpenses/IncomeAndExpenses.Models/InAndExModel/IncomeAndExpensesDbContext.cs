using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncomeAndExpenses.Models
{
    public class IncomeAndExpensesDbContext : IdentityDbContext<User>
    {
        public IncomeAndExpensesDbContext()
            : base("IncomeAndExpensesDbContext", throwIfV1Schema: false) { }

        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<IncomeType> IncomeTypes { get; set; }
        public DbSet<Income> Incomes { get; set; }

        public static IncomeAndExpensesDbContext Create()
        {
            return new IncomeAndExpensesDbContext();
        }
    }
}
