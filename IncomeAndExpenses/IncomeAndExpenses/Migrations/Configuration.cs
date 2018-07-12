namespace IncomeAndExpenses.Migrations
{
    using IncomeAndExpenses.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<IncomeAndExpenses.Models.IncomeAndExpensesDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "IncomeAndExpenses.Models.IncomeAndExpensesDbContext";
        }

        protected override void Seed(IncomeAndExpenses.Models.IncomeAndExpensesDbContext context)
        {

        }
    }
}
