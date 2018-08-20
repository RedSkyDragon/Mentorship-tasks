using System;

namespace ThingsBook.Data.Interface
{
    /// <summary>
    /// Friend data model.
    /// </summary>
    /// <seealso cref="ThingsBook.Data.Interface.Entity" />
    public class Friend : Entity
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
        /// Gets or sets the contacts.
        /// </summary>
        public string Contacts { get; set; }
    }
}