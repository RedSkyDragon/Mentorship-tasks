using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThingsBook.BusinessLogic.Models;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic
{
    public class LendsBL : BaseBL, ILendsBL
    {
        public LendsBL(CommonDAL data) : base(data) { }

        public async Task Create(Guid userId, Guid thingId, Lend lend)
        {
            await Data.Lends.CreateLend(userId, thingId, lend);
        }

        public async Task Delete(Guid userId, Guid thingId, DateTime returnDate)
        {
            var thing = await Data.Things.GetThing(userId, thingId);
            var delLend = Data.Lends.DeleteLend(userId, thingId);
            var hist = MakeLendHistorical(userId, thing, returnDate);
            var createHist = Data.History.CreateHistLend(userId, hist);
            await Task.WhenAll(delLend, createHist);
        }

        public async Task DeleteHistoricalLend(Guid userId, Guid id)
        {
            await Data.History.DeleteHistLend(userId, id);
        }

        public async Task<HistoricalLend> GetHistoricalLend(Guid userId, Guid id)
        {
            return await Data.History.GetHistLend(userId, id);
        }

        public async Task<IEnumerable<HistoricalLend>> GetHistoricalLends(Guid userId)
        {
            return await Data.History.GetHistLends(userId);
        }

        public async Task<FilteredLends> GetThingLends(Guid userId, Guid thingId)
        {
            var activeLends = new List<LendBL>();
            var thing = Data.Things.GetThing(userId, thingId);            
            var hist = Data.History.GetThingHistLends(userId, thingId);
            await Task.WhenAll(thing, hist);
            if (thing.Result.Lend != null)
            {
                activeLends.Add(LendBLFromThing(userId, thing.Result));
            }
            return new FilteredLends { ActiveLends = activeLends, History = hist.Result.Select(t => HistLendBLFromModel(userId, t)) };
        }

        public async Task Update(Guid userId, Guid thingId, Lend lend)
        {
            await Data.Lends.UpdateLend(userId, thingId, lend);
        }

        private HistLendBL HistLendBLFromModel(Guid userId, HistoricalLend hist)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<HistoricalLend, HistLendBL>());
            var histBL = config.CreateMapper().Map<HistoricalLend, HistLendBL>(hist);
            var friend = Data.Friends.GetFriend(userId, hist.FriendId);
            var thing = Data.Things.GetThing(userId, hist.ThingId);
            Task.WhenAll(friend, thing).Wait();
            histBL.FriendName = friend.Result.Name;
            histBL.ThingId = thing.Result.Id;
            histBL.ThingName = thing.Result.Name;
            return histBL;
        }

        private LendBL LendBLFromThing(Guid userId, Thing thing)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Lend, LendBL>());
            var lendBL = config.CreateMapper().Map<Lend, LendBL>(thing.Lend);
            var friend = Data.Friends.GetFriend(userId, thing.Lend.FriendId);
            friend.Wait();
            lendBL.FriendName = friend.Result.Name;
            lendBL.ThingId = thing.Id;
            lendBL.ThingName = thing.Name;
            return lendBL;
        }

        private HistoricalLend MakeLendHistorical(Guid userId, Thing thing, DateTime returnDate)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Lend, HistoricalLend>());
            var hist = config.CreateMapper().Map<Lend, HistoricalLend>(thing.Lend);
            hist.ReturnDate = returnDate;
            hist.UserId = thing.UserId;
            hist.ThingId = thing.Id;
            return hist;
        }
    }
}
