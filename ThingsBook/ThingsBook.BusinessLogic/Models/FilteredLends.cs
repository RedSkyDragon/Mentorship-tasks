using System.Collections.Generic;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic.Models
{
    public class FilteredLends
    {
        public IEnumerable<ActiveLend> ActiveLends { get; set; }

        public IEnumerable<HistLend> History { get; set; }
    }
}