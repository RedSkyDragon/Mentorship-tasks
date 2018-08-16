using System;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic.Models
{
    public class ActiveLend
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Thing Thing { get; set; }

        public Friend Friend { get; set; }

        public DateTime LendDate { get; set; }

        public string Comment { get; set; }
    }
}
