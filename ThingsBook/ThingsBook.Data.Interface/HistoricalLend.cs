using System;

namespace ThingsBook.Data.Interface
{
    public class HistoricalLend
    {
        public Guid Id { get; set; } = GuidUtils.NewGuid();

        public string UserId { get; set; }

        public string ThingId { get; set; }

        public string FriendId { get; set; }

        public DateTime LendDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public string Comment { get; set; }
    }
}
