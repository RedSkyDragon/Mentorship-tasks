using System;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic.Models
{
    /// <summary>
    /// Business logic model for friend
    /// </summary>
    public class Friend
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Guid Id { get; set; } = SequentialGuidUtils.CreateGuid();

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
