using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ThingsBook.WebAPI.Controllers
{
    public class FriendsController : ApiController
    {
        // GET: api/Friends
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Friends/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Friends
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Friends/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Friends/5
        public void Delete(int id)
        {
        }
    }
}
