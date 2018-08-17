using System;

namespace ThingsBook.Data.Interface
{
    /// <summary>
    /// Category data model.
    /// </summary>
    /// <seealso cref="ThingsBook.Data.Interface.Entity" />
    public class Category : Entity
    {
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
    }
}