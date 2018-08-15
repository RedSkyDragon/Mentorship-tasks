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
        public FriendsBL(CommonDAL data): base(data) { }

        public async Task Create(Friend friend)
        {
            await Data.Friends.CreateFriend(friend);
        }

        public async Task Delete(Guid id)
        {
            var delFriend = Data.Friends.DeleteFriend(id);
            var delLends = Data.Lends.DeleteFriendLends(id);
            var delHist = Data.History.DeleteFriendHistory(id);
            await Task.WhenAll(delFriend, delLends, delHist);
        }

        public async Task<IEnumerable<Friend>> GetAll(Guid userId)
        {
            return await Data.Friends.GetFriends(userId);
        }

        public async Task<FilteredLends> GetFriendLends(Guid userId, Guid friendId)
        {
            Task<IEnumerable<Lend>> lends = Data.Lends.GetFriendLends(userId, friendId);
            Task<IEnumerable<HistoricalLend>> hist = Data.History.GetFriendHistLends(friendId);
            await Task.WhenAll(lends, hist);
            return new FilteredLends { ActiveLends = lends.Result.Select(t => LendBLFromModel(t)), History = hist.Result.Select(t => HistLendBLFromModel(t)) };
        }

        private HistLendBL HistLendBLFromModel(HistoricalLend hist)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<HistoricalLend, HistLendBL>());
            var histBL = config.CreateMapper().Map<HistoricalLend, HistLendBL>(hist);
            var friend = Data.Friends.GetFriend(hist.FriendId);
            var thing = Data.Things.GetThing(hist.ThingId);
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
            var friend = Data.Friends.GetFriend(lend.FriendId);
            var thing = Data.Things.GetThingForLend(lend.Id);
            Task.WhenAll(friend, thing).Wait();
            lendBL.FriendName = friend.Result.Name;
            lendBL.ThingId = thing.Result.Id;
            lendBL.ThingName = thing.Result.Name;
            return lendBL;
        }

        public async Task<Friend> GetOne(Guid id)
        {
            return await Data.Friends.GetFriend(id);
        }

        public async Task Update(Friend friend)
        {
            await Data.Friends.UpdateFriend(friend);
        }
    }
}
