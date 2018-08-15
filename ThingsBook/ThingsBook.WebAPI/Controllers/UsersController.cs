using System;
using System.Threading.Tasks;
using System.Web.Http;
using ThingsBook.BusinessLogic;
using ThingsBook.Data.Interface;

namespace ThingsBook.WebAPI.Controllers
{
    public class UsersController : BaseController
    {
        private IUsersBL _users;

        public UsersController(IUsersBL users)
        {
            _users = users;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await _users.GetAll());
        }

        // GET: api/Users/5
        [HttpGet]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            return Ok(await _users.Get(id));
        }

        // POST: api/Users
        [HttpPost]
        public async Task<IHttpActionResult> Post(User user)
        {
            await _users.CreateOrUpdate(user);
            return Ok();
        }

        // PUT: api/Users/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(User user)
        {
            await _users.CreateOrUpdate(user);
            return Ok();
        }

        // DELETE: api/Users/5
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            await _users.Delete(id);
            return Ok();
        }
    }
}
