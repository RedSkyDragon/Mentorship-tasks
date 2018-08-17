using System;

namespace ThingsBook.Data.Interface
{
    /// <summary>
    /// Thing data model.
    /// </summary>
    /// <seealso cref="ThingsBook.Data.Interface.Entity" />
    public class Thing : Entity
    {
        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the additional information.
        /// </summary>
        public string About { get; set; }

        /// <summary>
        /// Gets or sets the lend information.
        /// </summary>
        public Lend Lend { get; set; }
    }
}