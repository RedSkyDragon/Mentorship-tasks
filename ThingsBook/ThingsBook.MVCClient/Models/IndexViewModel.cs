using System.Collections.Generic;

namespace ThingsBook.MVCClient.Models
{
    public class IndexViewModel
    {
        public User ApiUser { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<Thing> Things { get; set; }
    }
}
