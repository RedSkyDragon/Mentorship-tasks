using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncomeAndExpenses.Web.Utils
{
    public class ViewUtils
    {
        public static List<int> ListForPagination()
        {
            var result = new List<int>();
            result.Add(5);
            result.Add(10);
            result.Add(15);
            result.Add(20);
            result.Add(30);
            return result;
        }
    }
}