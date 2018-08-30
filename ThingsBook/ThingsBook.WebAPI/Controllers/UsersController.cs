using System.Threading.Tasks;
using System.Web.Http;
using ThingsBook.BusinessLogic;
using ThingsBook.BusinessLogic.Models;

namespace ThingsBook.WebAPI.Controllers
{

    /// <summary>
    /// Controller for users management
    /// </summary>
    /// <seealso cref="ThingsBook.WebAPI.Controllers.BaseController" />
    [RoutePrefix("user")]
    [Authorize]
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
        /// Gets the specified user from claims.
        /// </summary>
        /// <returns>User object</returns>
        [HttpGet]
        [Route("")]
        public Task<User> Get()
        {
            return _users.Get(ApiUser.Id);
        }

        /// <summary>
        /// Creates new user from claims.
        /// </summary>
        /// <returns>Created user</returns>
        [HttpPost]
        [Route("")]
        public Task<User> Post()
        {
            return _users.CreateOrUpdate(ApiUser);
        }

        /// <summary>
        /// Updates the specified user from claims.
        /// </summary>
        /// <returns>Updated user</returns>
        [HttpPut]
        [Route("")]
        public Task<User> Put()
        {
            return _users.CreateOrUpdate(ApiUser);
        }

        /// <summary>
        /// Deletes the specified user by identifier from claims.
        /// </summary>
        /// <returns>204(no content)</returns>
        [HttpDelete]
        [Route("")]
        public Task Delete()
        {
            return _users.Delete(ApiUser.Id);
        }
    }
}
