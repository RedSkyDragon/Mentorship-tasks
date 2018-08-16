using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ThingsBook.BusinessLogic;
using ThingsBook.BusinessLogic.Models;
using ThingsBook.Data.Interface;

namespace ThingsBook.WebAPI.Controllers
{
    [RoutePrefix("friends")]
    public class FriendsController : BaseController
    {
        private IFriendsBL _friends;

        public FriendsController(IFriendsBL friends)
        {
            _friends = friends;
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IEnumerable<Friend>> Get(Guid userId)
        {
            return await _friends.GetAll(userId);
        }

        [HttpGet]
        [Route("{userId}/{friendId}")]
        public async Task<Friend> Get(Guid userId, Guid friendId)
        {
            return await _friends.GetOne(userId,friendId);
        }

        [HttpGet]
        [Route("{userId}/{friendId}/lends")]
        public async Task<FilteredLends> GetLends(Guid userId, Guid friendId)
        {
            return await _friends.GetFriendLends(userId, friendId);
        }

        [HttpPost]
        [Route("{userId}")]
        public async Task Post(Guid userId, Friend friend)
        {
            await _friends.Create(userId, friend);
        }

        [HttpPut]
        [Route("{userId}")]
        public async Task Put(Guid userId, Friend friend)
        {
            await _friends.Update(userId, friend);
        }

        [HttpDelete]
        [Route("{userId}/{friendId}")]
        public async Task Delete(Guid userId, Guid friendId)
        {
            await _friends.Delete(userId, friendId);
        }
    }
}
