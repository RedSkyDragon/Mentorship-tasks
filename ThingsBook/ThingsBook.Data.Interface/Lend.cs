using System;

namespace ThingsBook.Data.Interface
{
    /// <summary>
    /// Lend data model.
    /// </summary>
    public class Lend
    {
        /// <summary>
        /// Gets or sets the friend identifier.
        /// </summary>
        public Guid FriendId { get; set; }

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
