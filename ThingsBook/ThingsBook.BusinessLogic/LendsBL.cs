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

        public async Task<HistLend> GetHistoricalLend(Guid userId, Guid id)
        {
            var hist = await Data.History.GetHistLend(userId, id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<HistoricalLend, HistLend>()).CreateMapper();
            var histLend = mapper.Map<HistoricalLend, HistLend>(hist);
            var friend = await Data.Friends.GetFriend(userId, hist.FriendId);
            var thing = await Data.Things.GetThing(userId, hist.ThingId);
            histLend.FriendName = friend.Name;
            histLend.ThingName = thing.Name;
            return histLend;
        }

        public async Task<IEnumerable<HistLend>> GetHistoricalLends(Guid userId)
        {
            var hists = await Data.History.GetHistLends(userId);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<HistoricalLend, HistLend>()).CreateMapper();
            var histLends = hists.Select(h => mapper.Map<HistoricalLend, HistLend>(h));
            foreach (var hist in histLends)
            {
                var friend = await Data.Friends.GetFriend(userId, hist.FriendId);
                var thing = await Data.Things.GetThing(userId, hist.ThingId);
                hist.FriendName = friend.Name;
                hist.ThingName = thing.Name;
            }
            return histLends;
        }

        public async Task Update(Guid userId, Guid thingId, Lend lend)
        {
            await Data.Lends.UpdateLend(userId, thingId, lend);
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
