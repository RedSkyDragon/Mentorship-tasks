using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ThingsBook.BusinessLogic;
using ThingsBook.BusinessLogic.Models;
using ThingsBook.Data.Interface;

namespace ThingsBook.WebAPI.Controllers
{
    [RoutePrefix("friend")]
    public class FriendsController : BaseController
    {
        private IFriendsBL _friends;

        public FriendsController(IFriendsBL friends)
        {
            _friends = friends;
        }

        [HttpGet]
        [Route("~/friends")]
        public Task<IEnumerable<Friend>> Get([FromUri]Guid userId)
        {
            return _friends.GetAll(userId);
        }

        [HttpGet]
        [Route("{friendId:guid}")]
        public Task<Friend> Get([FromUri]Guid userId, [FromUri]Guid friendId)
        {
            return _friends.GetOne(userId,friendId);
        }

        [HttpGet]
        [Route("{friendId:guid}/lends")]
        public Task<FilteredLends> GetLends([FromUri]Guid userId, [FromUri]Guid friendId)
        {
            return _friends.GetFriendLends(userId, friendId);
        }

        [HttpPost]
        [Route("")]
        public async Task<Friend> Post([FromUri]Guid userId, [FromBody]Models.Friend friend)
        {
            Friend friendDM = new Friend { Name = friend.Name, Contacts = friend.Contacts, UserId = userId };
            await _friends.Create(userId, friendDM);
            return await _friends.GetOne(userId, friendDM.Id);
        }

        [HttpPut]
        [Route("{friendId:guid}")]
        public async Task<Friend> Put([FromUri]Guid userId, [FromUri]Guid friendId, [FromBody]Models.Friend friend)
        {
            Friend friendDM = new Friend { Id = friendId, Name = friend.Name, Contacts = friend.Contacts, UserId = userId };
            await _friends.Update(userId, friendDM);
            return await _friends.GetOne(userId, friendDM.Id);
        }

        [HttpDelete]
        [Route("{friendId:guid}")]
        public Task Delete([FromUri]Guid userId, [FromUri]Guid friendId)
        {
            return _friends.Delete(userId, friendId);
        }
    }
}
