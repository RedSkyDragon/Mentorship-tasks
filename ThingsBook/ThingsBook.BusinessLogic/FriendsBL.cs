using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThingsBook.BusinessLogic.Models;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic
{
    public class FriendsBL : BaseBL, IFriendsBL
    {
        public FriendsBL(CommonDAL data)
        {
            _data = data;
        }

        public async Task Create(Friend friend)
        {
            await _data.Friends.CreateFriend(friend);
        }

        public async Task Delete(Guid id)
        {
            var delFriend = _data.Friends.DeleteFriend(id);
            var delLends = _data.Lends.DeleteFriendLends(id);
            var delHist = _data.History.DeleteFriendHistory(id);
            await Task.WhenAll(delFriend, delLends, delHist);
        }

        public async Task<IEnumerable<Friend>> GetAll(Guid userId)
        {
            return await _data.Friends.GetFriends(userId);
        }

        public async Task<FilteredLends> GetFriendLends(Guid userId, Guid friendId)
        {
            Task<IEnumerable<LendBL>> lends = Task.Run(async () =>
            {
                var activeLends = (await _data.Lends.GetFriendLends(userId, friendId))
                    .Select(t => LendBLFromModel(t));
                return activeLends;
            });
            Task<IEnumerable<HistLendBL>> hist = Task.Run(async () =>
            {
                var histLends = (await _data.History.GetFriendHistLends(friendId))
                    .Select(t => HistLendBLFromModel(t));
                return histLends;
            });
            await Task.WhenAll(lends, hist);
            return new FilteredLends { ActiveLends = lends.Result, History = hist.Result };
        }

        private HistLendBL HistLendBLFromModel(HistoricalLend hist)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<HistoricalLend, HistLendBL>());
            var histBL = config.CreateMapper().Map<HistoricalLend, HistLendBL>(hist);
            var friend = _data.Friends.GetFriend(hist.FriendId);
            var thing = _data.Things.GetThing(hist.ThingId);
            Task.WhenAll(friend, thing).Wait();
            histBL.FriendName = friend.Result.Name;
            histBL.ThingId = thing.Result.Id;
            histBL.ThingName = thing.Result.Name;
            return histBL;
        }

        private LendBL LendBLFromModel(Lend lend)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Lend, LendBL>());
            var lendBL = config.CreateMapper().Map<Lend, LendBL>(lend);
            var friend = _data.Friends.GetFriend(lend.FriendId);
            var thing = _data.Things.GetThingForLend(lend.Id);
            Task.WhenAll(friend, thing).Wait();
            lendBL.FriendName = friend.Result.Name;
            lendBL.ThingId = thing.Result.Id;
            lendBL.ThingName = thing.Result.Name;
            return lendBL;
        }

        public async Task<Friend> GetOne(Guid id)
        {
            return await _data.Friends.GetFriend(id);
        }

        public async Task Update(Friend friend)
        {
            await _data.Friends.UpdateFriend(friend);
        }
    }
}
