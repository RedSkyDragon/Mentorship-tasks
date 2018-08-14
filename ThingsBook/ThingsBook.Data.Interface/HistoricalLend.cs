using System;

namespace ThingsBook.Data.Interface
{
    public class HistoricalLend
    {
        public Guid Id { get; set; } = SequentialGuidUtils.CreateGuid();

        public Guid UserId { get; set; }

        public Guid ThingId { get; set; }

        public Guid FriendId { get; set; }

        public DateTime LendDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public string Comment { get; set; }
    }
}
