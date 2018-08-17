using System;
using System.Collections.Generic;

namespace ThingsBook.Data.Interface
{
    public class Category : Entity
    {
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string About { get; set; }
    }
}