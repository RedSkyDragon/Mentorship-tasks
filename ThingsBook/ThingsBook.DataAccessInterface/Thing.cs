using System;

namespace ThingsBook.DataAccessInterface
{
    public class Thing
    {
        public Guid Id { get; set; } = GuidUtils.NewGuid();

        public string Name { get; set; }

        public string About { get; set; }

        public Lend Lend { get; set; }
    }
}