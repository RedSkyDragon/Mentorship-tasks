using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ThingsBook.BusinessLogic;
using ThingsBook.Data.Interface;

namespace ThingsBook.WebAPI.Controllers
{
    [RoutePrefix("user")]
    public class UsersController : BaseController
    {
        private IUsersBL _users;

        public UsersController(IUsersBL users)
        {
            _users = users;
        }

        [HttpGet]
        [Route("~/users")]
        public Task<IEnumerable<User>> Get()
        {
            return _users.GetAll();
        }

        [HttpGet]
        [Route("{userId:guid}")]
        public Task<User> Get(Guid userId)
        {
            return _users.Get(userId);
        }

        [HttpPost]
        [Route("")]
        public async Task<User> Post([FromBody]Models.User user)
        {
            User userDM = new User { Name = user.Name };
            await _users.CreateOrUpdate(userDM);
            return await _users.Get(userDM.Id);
        }

        [HttpPut]
        [Route("{userId:guid}")]
        public async Task<User> Put([FromUri]Guid userId, [FromBody]Models.User user)
        {
            User userDM = new User { Id = userId, Name = user.Name };
            await _users.CreateOrUpdate(userDM);
            return await _users.Get(userId);
        }

        [HttpDelete]
        [Route("{userId:guid}")]
        public Task Delete([FromUri]Guid userId)
        {
            return _users.Delete(userId);
        }
    }
}
