using System;

namespace ThingsBook.Data.Interface
{
    public class Thing : Entity
    {
        public Guid CategoryId { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string About { get; set; }

        public Lend Lend { get; set; }
    }
}