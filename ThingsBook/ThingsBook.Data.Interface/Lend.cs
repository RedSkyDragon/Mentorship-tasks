using System;

namespace ThingsBook.Data.Interface
{
    public class Lend
    {
        public Guid Id { get; set; } = GuidUtils.NewGuid();

        public string FriendId { get; set; }

        public DateTime LendDate { get; set; }

        public string Comment { get; set; }
    }
}