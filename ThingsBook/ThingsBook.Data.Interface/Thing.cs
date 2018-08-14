using System;

namespace ThingsBook.Data.Interface
{
    public class Thing
    {
        public Guid Id { get; set; } = SequentialGuidUtils.CreateGuid();

        public string Name { get; set; }

        public string About { get; set; }

        public Lend Lend { get; set; }
    }
}