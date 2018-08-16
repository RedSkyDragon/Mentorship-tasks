using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThingsBook.WebAPI.Models
{
    public class Thing
    {
        public string Name { get; set; }

        public string About { get; set; }

        public Guid CategoryId { get; set; }
    }
}