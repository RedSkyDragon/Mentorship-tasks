using IncomeAndExpenses.Web.Utils;
using System.Web;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomExceptionFilter());
        }
    }
}
