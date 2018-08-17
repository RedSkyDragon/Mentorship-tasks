using System;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic.Models
{
    public class HistLend
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Thing Thing { get; set; }

        public Friend Friend { get; set; }

        public DateTime LendDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public string Comment { get; set; }
    }
}
