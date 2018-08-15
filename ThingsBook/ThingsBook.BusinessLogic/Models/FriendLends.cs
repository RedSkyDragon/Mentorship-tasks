using System.Collections.Generic;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic.Models
{
    public class FilteredLends
    {
        public IEnumerable<LendBL> ActiveLends { get; set; }

        public IEnumerable<HistLendBL> History { get; set; }
    }
}