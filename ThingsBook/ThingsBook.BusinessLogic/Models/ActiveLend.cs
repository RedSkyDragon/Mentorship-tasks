using System;

namespace ThingsBook.BusinessLogic.Models
{
    /// <summary>
    /// Active lend business logic model
    /// </summary>
    public class ActiveLend
    {
        /// <summary>
        /// Gets or sets the thing.
        /// </summary>
        public Thing Thing { get; set; }

        /// <summary>
        /// Gets or sets the friend.
        /// </summary>
        public Friend Friend { get; set; }

        /// <summary>
        /// Gets or sets the lend date.
        /// </summary>
        public DateTime LendDate { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        public string Comment { get; set; }
    }
}
