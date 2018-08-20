using System;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic.Models
{
    /// <summary>
    /// Business logic model for thing.
    /// </summary>
    public class ThingWithLend
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Guid Id { get; set; } = SequentialGuidUtils.CreateGuid();

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        public Guid CategoryId { get; set; }

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
