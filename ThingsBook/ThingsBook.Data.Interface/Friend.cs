using System;

namespace ThingsBook.Data.Interface
{
    public class Friend : Entity
    {
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string Contacts { get; set; }
    }
}