using System;

namespace ThingsBook.Data.Interface
{
    public class Friend
    {
        public Guid Id { get; set; } = GuidUtils.NewGuid();

        public string Name { get; set; }

        public string Contacts { get; set; }
    }
}