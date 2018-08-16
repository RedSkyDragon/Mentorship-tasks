using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ThingsBook.BusinessLogic;
using ThingsBook.Data.Interface;

namespace ThingsBook.WebAPI.Controllers
{
    public class FriendsController : BaseController
    {
        private IFriendsBL _friends;

        public FriendsController(IFriendsBL friends)
        {
            _friends = friends;
        }

        // GET: api/Friends
        [HttpGet]
        public async Task<IHttpActionResult> Get(Guid userId)
        {
            return Ok(await _friends.GetAll(userId));
        }

        // GET: api/Friends/5
        [HttpGet]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            return Ok(await _friends.GetOne(id));
        }

        // POST: api/Friends
        public async Task<IHttpActionResult> Post(Friend friend)
        {
        }

        // PUT: api/Friends/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Friends/5
        public async Task<IHttpActionResult> Delete(int id)
        {
        }
    }
}
