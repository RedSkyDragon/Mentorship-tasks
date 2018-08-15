using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThingsBook.BusinessLogic.Models;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic
{
    public class LendsBL : BaseBL, ILendsBL
    {
        public LendsBL(CommonDAL data)
        {
            _data = data;
        }

        public async Task Create(Guid thingId, Lend lend)
        {
            await _data.Lends.CreateLend(thingId, lend);
        }

        public async Task Delete(Guid thingId, DateTime returnDate)
        {
            var thing = await _data.Things.GetThing(thingId);
            var delLend = _data.Lends.DeleteLend(thingId);
            var hist = MakeLendHistorical(thing, returnDate);
            var createHist = _data.History.CreateHistLend(hist);
            await Task.WhenAll(delLend, createHist);
        }

        public async Task DeleteHistoricalLend(Guid id)
        {
            await _data.History.DeleteHistLend(id);
        }

        public async Task<HistoricalLend> GetHistoricalLend(Guid id)
        {
            return await _data.History.GetHistLend(id);
        }

        public async Task<IEnumerable<HistoricalLend>> GetHistoricalLends(Guid userId)
        {
            return await _data.History.GetHistLends(userId);
        }

        public async Task<FilteredLends> GetThingLends(Guid userId, Guid thingId)
        {
            var activeLends = new List<LendBL>();
            var thing = _data.Things.GetThing(thingId);            
            var hist = _data.History.GetThingHistLends(thingId));
            await Task.WhenAll(thing, hist);
            if (thing.Result.Lend != null)
            {
                activeLends.Add(LendBLFromThing(thing.Result));
            }
            return new FilteredLends { ActiveLends = activeLends, History = hist.Result.Select(t => HistLendBLFromModel(t)) };
        }

        public async Task Update(Guid thingId, Lend lend)
        {
            await _data.Lends.UpdateLend(thingId, lend);
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

        private LendBL LendBLFromThing(Thing thing)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Lend, LendBL>());
            var lendBL = config.CreateMapper().Map<Lend, LendBL>(thing.Lend);
            var friend = _data.Friends.GetFriend(thing.Lend.FriendId);
            friend.Wait();
            lendBL.FriendName = friend.Result.Name;
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
