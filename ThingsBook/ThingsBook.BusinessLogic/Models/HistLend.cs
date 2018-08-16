using System;

namespace ThingsBook.BusinessLogic.Models
{
    public class HistLend
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid ThingId { get; set; }

        public string ThingName { get; set; }

        public Guid FriendId { get; set; }

        public string FriendName { get; set; }

        public DateTime LendDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public string Comment { get; set; }
    }
}
