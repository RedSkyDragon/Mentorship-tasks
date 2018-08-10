using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ThingsBook.WebAPI.Controllers
{
    public class LendsHistoryController : ApiController
    {
        // GET: api/LendsHistory
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/LendsHistory/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/LendsHistory
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/LendsHistory/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/LendsHistory/5
        public void Delete(int id)
        {
        }
    }
}
