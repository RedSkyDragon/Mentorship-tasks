using System;
using System.Collections.Generic;

namespace ThingsBook.Data.Interface
{
    public class User
    {
        public Guid Id { get; set; } = SequentialGuidUtils.CreateGuid();

        public string Name { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<Friend> Friends { get; set; }
    }
}
