using IncomeAndExpenses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncomeAndExpenses.DataAccess
{
    public class ContextDAL
    {
        public static void AddStandartTypes(User user)
        {
            using (var db = new IncomeAndExpensesDbContext())
            {
                db.ExpenseTypes.Add(new ExpenseType { Name = "Food", Description = "Category of expenditure on food", IsStandart = true, UserId = user.Id, User = user });
                db.ExpenseTypes.Add(new ExpenseType { Name = "Other", Description = "Category of other expenditure", IsStandart = true, UserId = user.Id, User = user });
                db.IncomeTypes.Add(new IncomeType { Name = "Salary", Description = "Income from regular work", IsStandart = true, UserId = user.Id, User = user });
                db.IncomeTypes.Add(new IncomeType { Name = "Gift", Description = "Income from a gift", IsStandart = true, UserId = user.Id, User = user });
                db.SaveChanges();
            }
        }
    }
}