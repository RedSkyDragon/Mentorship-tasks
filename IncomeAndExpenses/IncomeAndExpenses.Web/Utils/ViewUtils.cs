using System.Collections.Generic;

namespace IncomeAndExpenses.Web.Utils
{
    /// <summary>
    /// class for view utils
    /// </summary>
    public class ViewUtils
    {
        /// <summary>
        /// Lists for pagination. Includes possible page sizes
        /// </summary>
        /// <returns>List with int sizes</returns>
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