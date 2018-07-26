using IncomeAndExpenses.Web.Utils;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web
{
    public class FilterConfig
    {
        /// <summary>
        /// Register CustonExceptionFilter 
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomExceptionFilter());
        }
    }
}
