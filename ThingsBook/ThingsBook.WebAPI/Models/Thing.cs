using System;

namespace ThingsBook.WebAPI.Models
{
    /// <summary>
    /// Thing model for data from client.
    /// </summary>
    public class Thing
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the about.
        /// </summary>
        public string About { get; set; }

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        public Guid CategoryId { get; set; }
    }
}