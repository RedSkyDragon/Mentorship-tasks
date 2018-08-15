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
            var createHist = Task.Run(async () =>
            {
                var hist = MakeLendHistorical(thing, returnDate);
                await _data.History.CreateHistLend(hist);
            });
            await Task.WhenAll(delLend, createHist);
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
            Task<List<LendBL>> lends = Task.Run(async () =>
            {
                var activeLends = new List<LendBL>();
                var lend = LendBLFromThing((await _data.Things.GetThing(thingId)));
                if (lend != null)
                {
                    activeLends.Add(lend);
                }
                return activeLends;
            });
            Task<IEnumerable<HistLendBL>> hist = Task.Run(async () =>
            {
                var histLends = (await _data.History.GetThingHistLends(thingId))
                    .Select(t => HistLendBLFromModel(t));
                return histLends;
            });
            await Task.WhenAll(lends, hist);
            return new FilteredLends { ActiveLends = lends.Result, History = hist.Result };
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
            if (thing.Lend == null)
            {
                return null;
            }
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Lend, LendBL>());
            var lendBL = config.CreateMapper().Map<Lend, LendBL>(thing.Lend);
            var friend = _data.Friends.GetFriend(thing.Lend.FriendId);
            friend.Wait();
            lendBL.FriendName = friend.Result.Name;
            lendBL.ThingId = thing.Id;
            lendBL.ThingName = thing.Name;
            return lendBL;
        }
    }
}
