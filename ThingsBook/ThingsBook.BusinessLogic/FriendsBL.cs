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

        public async Task Create(Guid userId, Friend friend)
        {
            await Data.Friends.CreateFriend(userId, friend);
        }

        public async Task Delete(Guid userId, Guid id)
        {
            var delFriend = Data.Friends.DeleteFriend(userId, id);
            var delLends = Data.Lends.DeleteFriendLends(userId, id);
            var delHist = Data.History.DeleteFriendHistory(userId, id);
            await Task.WhenAll(delFriend, delLends, delHist);
        }

        public async Task<IEnumerable<Friend>> GetAll(Guid userId)
        {
            return await Data.Friends.GetFriends(userId);
        }

        public async Task<FilteredLends> GetFriendLends(Guid userId, Guid friendId)
        {
            var friend = Data.Friends.GetFriend(userId, friendId);
            var hists = Data.History.GetFriendHistLends(userId, friendId);
            await Task.WhenAll(hists, friend);
            var active = LendsForFriend(userId, friend.Result);
            var history = HistLendsFromDM(userId, hists.Result, friend.Result);
            await Task.WhenAll(active, history);
            return new FilteredLends
            {
                ActiveLends = active.Result,
                History = history.Result
            };
        }

        public async Task<Friend> GetOne(Guid userId, Guid id)
        {
            return await Data.Friends.GetFriend(userId, id);
        }

        public async Task Update(Guid userId, Friend friend)
        {
            await Data.Friends.UpdateFriend(userId, friend);
        }

        private async Task<IEnumerable<HistLend>> HistLendsFromDM(Guid userId, IEnumerable<HistoricalLend> hists, Friend friend)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<HistoricalLend, HistLend>()).CreateMapper();
            var histsBL = hists.Select(h => mapper.Map<HistoricalLend, HistLend>(h));
            foreach (var hist in histsBL)
            {
                var thing = await Data.Things.GetThing(userId, hist.ThingId);
                hist.FriendName = friend.Name;
                hist.ThingName = thing.Name;
            }
            return histsBL;
        }

        private async Task<IEnumerable<ActiveLend>> LendsForFriend(Guid userId, Friend friend)
        {
            var things = await Data.Things.GetThindsForFriend(userId, friend.Id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Lend, ActiveLend>()).CreateMapper();
            var lends = new List<ActiveLend>();
            foreach (var thing in things)
            {
                var lend = mapper.Map<ActiveLend>(thing.Lend);
                lend.FriendName = friend.Name;
                lend.ThingId = thing.Id;
                lend.ThingName = thing.Name;
                lends.Add(lend);
            }
            return lends;
        }
    }
}
