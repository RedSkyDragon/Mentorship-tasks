using System;

namespace ThingsBook.Data.Interface
{
    /// <summary>
    /// Historical lend data model.
    /// </summary>
    /// <seealso cref="ThingsBook.Data.Interface.Entity" />
    public class HistoricalLend : Entity
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the thing identifier.
        /// </summary>
        public Guid ThingId { get; set; }

        /// <summary>
        /// Gets or sets the friend identifier.
        /// </summary>
        public Guid FriendId { get; set; }

        /// <summary>
        /// Gets or sets the lend date.
        /// </summary>
        public DateTime LendDate { get; set; }

        /// <summary>
        /// Gets or sets the return date.
        /// </summary>
        public DateTime ReturnDate { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        public string Comment { get; set; }
    }
}
