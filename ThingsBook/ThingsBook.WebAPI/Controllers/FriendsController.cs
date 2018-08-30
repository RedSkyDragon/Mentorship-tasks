using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ThingsBook.BusinessLogic;
using ThingsBook.BusinessLogic.Models;

namespace ThingsBook.WebAPI.Controllers
{
    /// <summary>
    /// Controller for friends management
    /// </summary>
    /// <seealso cref="ThingsBook.WebAPI.Controllers.BaseController" />
    [RoutePrefix("friend")]
    [Authorize]
    public class FriendsController : BaseController
    {
        private IFriendsBL _friends;

        /// <summary>
        /// Initializes a new instance of the <see cref="FriendsController"/> class.
        /// </summary>
        /// <param name="friends">The friends business logic</param>
        public FriendsController(IFriendsBL friends)
        {
            _friends = friends;
        }

        /// <summary>
        /// Gets all friends for the specified user identifier.
        /// </summary>
        /// <returns>List of friends</returns>
        [HttpGet]
        [Route("~/friends")]
        public Task<IEnumerable<Friend>> Get()
        {
            return _friends.GetAll(ApiUser.Id);
        }

        /// <summary>
        /// Gets the specified friend by identifier.
        /// </summary>
        /// <param name="friendId">The friend identifier.</param>
        /// <returns>Friend</returns>
        [HttpGet]
        [Route("{friendId:guid}")]
        public Task<Friend> Get([FromUri]Guid friendId)
        {
            return _friends.GetOne(ApiUser.Id, friendId);
        }

        /// <summary>
        /// Gets the lends for specified by identifier friend.
        /// </summary>
        /// <param name="friendId">The friend identifier.</param>
        /// <returns>Filtered lends with active records and history</returns>
        [HttpGet]
        [Route("{friendId:guid}/lends")]
        public Task<FilteredLends> GetLends([FromUri]Guid friendId)
        {
            return _friends.GetFriendLends(ApiUser.Id, friendId);
        }

        /// <summary>
        /// Creates new friend.
        /// </summary>
        /// <param name="friend">The friend information.</param>
        /// <returns>Created friend.</returns>
        [HttpPost]
        [Route("")]
        public Task<Friend> Post([FromBody]Models.Friend friend)
        {
            Friend friendBL = new Friend
            {
                Name = friend.Name,
                Contacts = friend.Contacts
            };
            return _friends.Create(ApiUser.Id, friendBL);
        }

        /// <summary>
        /// Updates the specified by identifier friend.
        /// </summary>
        /// <param name="friendId">The friend identifier.</param>
        /// <param name="friend">The friend information.</param>
        /// <returns>Updated friend.</returns>
        [HttpPut]
        [Route("{friendId:guid}")]
        public Task<Friend> Put([FromUri]Guid friendId, [FromBody]Models.Friend friend)
        {
            Friend friendBL = new Friend
            {
                Id = friendId,
                Name = friend.Name,
                Contacts = friend.Contacts
            };
            return _friends.Update(ApiUser.Id, friendBL);
        }

        /// <summary>
        /// Deletes the specified by identifier friend.
        /// </summary>
        /// <param name="friendId">The friend identifier.</param>
        /// <returns>204(no content)</returns>
        [HttpDelete]
        [Route("{friendId:guid}")]
        public Task Delete([FromUri]Guid friendId)
        {
            return _friends.Delete(ApiUser.Id, friendId);
        }
    }
}
