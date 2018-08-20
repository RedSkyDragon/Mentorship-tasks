using System.Collections.Generic;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic.Models
{
    /// <summary>
    /// Business logic model for lists of active and historical lends
    /// </summary>
    public class FilteredLends
    {
        /// <summary>
        /// Gets or sets the active lends.
        /// </summary>
        public IEnumerable<ActiveLend> ActiveLends { get; set; }

        /// <summary>
        /// Gets or sets the historical lends.
        /// </summary>
        public IEnumerable<HistLend> History { get; set; }
    }
}