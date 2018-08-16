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
            var hist = MakeLendHistorical(thing, returnDate);
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
            var activeLends = new List<ActiveLend>();
            var thing = Data.Things.GetThing(userId, thingId);            
            var hists = Data.History.GetThingHistLends(userId, thingId);
            await Task.WhenAll(thing, hists);
            if (thing.Result.Lend != null)
            {
                activeLends.Add(await ActiveLendFromThing(userId, thing.Result));
            }
            return new FilteredLends
            {
                ActiveLends = activeLends,
                History = await HistLendsFromModel(userId, hists.Result, thing.Result)
            };
        }

        public async Task Update(Guid userId, Guid thingId, Lend lend)
        {
            await Data.Lends.UpdateLend(userId, thingId, lend);
        }

        private async Task<IEnumerable<HistLend>> HistLendsFromModel(Guid userId, IEnumerable<HistoricalLend> hists, Thing thing)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<HistoricalLend, HistLend>()).CreateMapper();
            var histsBL = hists.Select(h => mapper.Map<HistoricalLend, HistLend>(h));
            foreach (var hist in histsBL)
            {
                var friend = await Data.Friends.GetFriend(userId, hist.FriendId);
                hist.FriendName = friend.Name;
                hist.ThingName = thing.Name;
            }
            return histsBL;
        }

        private async Task<ActiveLend> ActiveLendFromThing(Guid userId, Thing thing)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Lend, ActiveLend>());
            var lendBL = config.CreateMapper().Map<Lend, ActiveLend>(thing.Lend);
            var friend = await Data.Friends.GetFriend(userId, thing.Lend.FriendId);
            lendBL.FriendName = friend.Name;
            lendBL.ThingId = thing.Id;
            lendBL.ThingName = thing.Name;
            return lendBL;
        }

        private HistoricalLend MakeLendHistorical(Thing thing, DateTime returnDate)
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
