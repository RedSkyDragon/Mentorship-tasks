using System;
using System.Collections.Generic;

namespace ThingsBook.Data.Interface
{
    public class Category
    {
        public Guid Id { get; set; } = SequentialGuidUtils.CreateGuid();

        public string Name { get; set; }

        public string About { get; set; }

        public IEnumerable<Thing> Things { get; set; }
    }
}