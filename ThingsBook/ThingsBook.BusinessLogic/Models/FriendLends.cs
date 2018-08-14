using System.Collections.Generic;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic.Models
{
    public class FilteredLends
    {
        public IEnumerable<Lend> ActiveLends { get; set; }

        public IEnumerable<HistoricalLend> History { get; set; }
    }
}