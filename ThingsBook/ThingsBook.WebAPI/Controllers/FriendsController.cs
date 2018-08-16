using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ThingsBook.BusinessLogic;
using ThingsBook.BusinessLogic.Models;
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

        [HttpGet]
        public async Task<IEnumerable<Friend>> Get(Guid userId)
        {
            return await _friends.GetAll(userId);
        }

        [HttpGet]
        public async Task<Friend> Get(Guid userId, Guid friendId)
        {
            return await _friends.GetOne(userId,friendId);
        }

        [HttpGet]
        public async Task<FilteredLends> GetLends(Guid userId, Guid friendId)
        {
            return await _friends.GetFriendLends(userId, friendId);
        }

        [HttpPost]
        public async Task Post(Guid userId, Friend friend)
        {
            await _friends.Create(userId, friend);
        }

        [HttpPut]
        public async Task Put(Guid userId, Friend friend)
        {
            await _friends.Update(userId, friend);
        }

        [HttpDelete]
        public async Task Delete(Guid userId, Guid friendId)
        {
            await _friends.Delete(userId, friendId);
        }
    }
}
