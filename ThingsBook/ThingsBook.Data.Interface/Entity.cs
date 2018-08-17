using System;

namespace ThingsBook.Data.Interface
{
    /// <summary>
    /// Base class with identifier for data models.
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Guid Id { get; set; } = SequentialGuidUtils.CreateGuid();
    }
}
