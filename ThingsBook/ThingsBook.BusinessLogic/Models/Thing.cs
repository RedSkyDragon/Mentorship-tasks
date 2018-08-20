using System;

namespace ThingsBook.BusinessLogic.Models
{
    /// <summary>
    /// Business logic model for thing. Lend is not included.
    /// </summary>
    public class Thing
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Guid Id { get; set; }

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
