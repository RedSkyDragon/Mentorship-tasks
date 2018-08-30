using System.Collections.Generic;

namespace ThingsBook.MVCClient.Models
{
    /// <summary>
    /// View model for index page.
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Gets or sets the API user.
        /// </summary>
        /// <value>
        /// The API user.
        /// </value>
        public User ApiUser { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        public IEnumerable<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the things.
        /// </summary>
        /// <value>
        /// The things.
        /// </value>
        public IEnumerable<Thing> Things { get; set; }
    }
}
