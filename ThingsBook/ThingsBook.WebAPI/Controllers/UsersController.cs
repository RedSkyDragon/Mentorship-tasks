using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ThingsBook.BusinessLogic;
using ThingsBook.Data.Interface;

namespace ThingsBook.WebAPI.Controllers
{
    [RoutePrefix("users")]
    public class UsersController : BaseController
    {
        private IUsersBL _users;

        public UsersController(IUsersBL users)
        {
            _users = users;
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<User>> Get()
        {
            return await _users.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<User> Get(Guid id)
        {
            return await _users.Get(id);
        }

        [HttpPost]
        [Route("")]
        public async Task Post(User user)
        {
            await _users.CreateOrUpdate(user);
        }

        [HttpPut]
        [Route("")]
        public async Task Put(User user)
        {
            await _users.CreateOrUpdate(user);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(Guid id)
        {
            await _users.Delete(id);
        }
    }
}
