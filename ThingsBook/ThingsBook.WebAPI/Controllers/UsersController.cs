using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ThingsBook.BusinessLogic;
using ThingsBook.Data.Interface;

namespace ThingsBook.WebAPI.Controllers
{

    /// <summary>
    /// Controller for users management
    /// </summary>
    /// <seealso cref="ThingsBook.WebAPI.Controllers.BaseController" />
    [RoutePrefix("user")]
    public class UsersController : BaseController
    {
        private IUsersBL _users;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="users">The users business logic</param>
        public UsersController(IUsersBL users)
        {
            _users = users;
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>List of users</returns>
        [HttpGet]
        [Route("~/users")]
        public Task<IEnumerable<User>> Get()
        {
            return _users.GetAll();
        }

        /// <summary>
        /// Gets the specified user by identifier.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>User object</returns>
        [HttpGet]
        [Route("{userId:guid}")]
        public Task<User> Get(Guid userId)
        {
            return _users.Get(userId);
        }

        /// <summary>
        /// Creates new user.
        /// </summary>
        /// <param name="user">The user information</param>
        /// <returns>Created user</returns>
        [HttpPost]
        [Route("")]
        public Task<User> Post([FromBody]Models.User user)
        {
            User userDM = new User { Name = user.Name };
            return _users.CreateOrUpdate(userDM);
        }

        /// <summary>
        /// Updates the specified user by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="user">The user information.</param>
        /// <returns>Updated user</returns>
        [HttpPut]
        [Route("{userId:guid}")]
        public Task<User> Put([FromUri]Guid userId, [FromBody]Models.User user)
        {
            User userDM = new User { Id = userId, Name = user.Name };
            return _users.CreateOrUpdate(userDM);
        }

        /// <summary>
        /// Deletes the specified user by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>204(no content)</returns>
        [HttpDelete]
        [Route("{userId:guid}")]
        public Task Delete([FromUri]Guid userId)
        {
            return _users.Delete(userId);
        }
    }
}
