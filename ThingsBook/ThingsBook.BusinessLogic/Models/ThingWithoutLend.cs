using System;

namespace ThingsBook.BusinessLogic.Models
{
    public class ThingWithoutLend
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string About { get; set; }

        public Guid CategoryId { get; set; }
    }
}
