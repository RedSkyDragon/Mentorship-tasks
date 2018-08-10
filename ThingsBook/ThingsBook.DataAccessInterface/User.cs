using System;
using System.Collections.Generic;

namespace ThingsBook.DataAccessInterface
{
    public class User
    {
        public Guid Id { get; set; } = GuidUtils.NewGuid();

        public string Name { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<Friend> Friends { get; set; }
    }
}
