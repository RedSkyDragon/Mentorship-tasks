using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThingsBook.BusinessLogic.Models;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic
{
    public class FriendsBL : BaseBL, IFriendsBL
    {
        public FriendsBL(CommonDAL data): base(data) { }

        public Task Create(Guid userId, Friend friend)
        {
            return Data.Friends.CreateFriend(userId, friend);
        }

        public async Task Delete(Guid userId, Guid id)
        {
            await Data.Lends.DeleteFriendLends(userId, id);
            await Data.History.DeleteFriendHistory(userId, id);
            await Data.Friends.DeleteFriend(userId, id);
        }

        public Task<IEnumerable<Friend>> GetAll(Guid userId)
        {
            return Data.Friends.GetFriends(userId);
        }

        public async Task<FilteredLends> GetFriendLends(Guid userId, Guid friendId)
        {
            var friend = await Data.Friends.GetFriend(userId, friendId);
            var active = GetActiveLends(userId, friend);
            var history = GetHistoryLends(userId, friend);
            await Task.WhenAll(active, history);
            return new FilteredLends
            {
                ActiveLends = active.Result,
                History = history.Result
            };
        }

        public Task<Friend> GetOne(Guid userId, Guid id)
        {
            return Data.Friends.GetFriend(userId, id);
        }

        public Task Update(Guid userId, Friend friend)
        {
            return Data.Friends.UpdateFriend(userId, friend);
        }

        private async Task<IEnumerable<HistLend>> GetHistoryLends(Guid userId, Friend friend)
        {
            var history = await Data.History.GetFriendHistLends(userId, friend.Id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<HistoricalLend, HistLend>()).CreateMapper();
            var lends = new List<HistLend>();
            foreach (var item in history)
            {
                var lend = mapper.Map<HistoricalLend, HistLend>(item.Key);
                lend.Friend = friend;
                lend.Thing = item.Value;
                lends.Add(lend);
            }
            return lends;
        }

        private async Task<IEnumerable<ActiveLend>> GetActiveLends(Guid userId, Friend friend)
        {
            var things = await Data.Things.GetThingsForFriend(userId, friend.Id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Lend, ActiveLend>()).CreateMapper();
            var lends = new List<ActiveLend>();
            foreach (var thing in things)
            {
                var lend = mapper.Map<ActiveLend>(thing.Lend);
                lend.Friend = friend;
                lend.Thing = thing;
            }
            return lends;
        }
    }
}
