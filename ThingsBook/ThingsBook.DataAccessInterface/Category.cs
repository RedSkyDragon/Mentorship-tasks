using System;
using System.Collections.Generic;

namespace ThingsBook.DataAccessInterface
{
    public class Category
    {
        public Guid Id { get; set; } = GuidUtils.NewGuid();

        public string Name { get; set; }

        public string About { get; set; }

        public IEnumerable<Thing> Things { get; set; }
    }
}