using System;

namespace ThingsBook.Data.Interface
{
    public class Lend : Entity
    {
        public Guid FriendId { get; set; }

        public DateTime LendDate { get; set; }

        public string Comment { get; set; }
    }
}
