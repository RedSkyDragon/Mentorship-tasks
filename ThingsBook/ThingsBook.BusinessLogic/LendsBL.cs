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

        public async Task<Thing> Create(Guid userId, Guid thingId, Lend lend)
        {
            await Data.Lends.CreateLend(userId, thingId, lend);
            return await Data.Things.GetThing(userId, thingId);
        }

        public async Task Delete(Guid userId, Guid thingId, DateTime returnDate)
        {
            var thing = await Data.Things.GetThing(userId, thingId);
            var historyLend = ReturnThing(thing, returnDate);
            await Data.History.CreateHistLend(userId, historyLend);
            await Data.Lends.DeleteLend(userId, thingId);
        }

        public Task DeleteHistoricalLend(Guid userId, Guid id)
        {
            return Data.History.DeleteHistLend(userId, id);
        }

        public async Task<HistLend> GetHistoricalLend(Guid userId, Guid id)
        {
            var hist = await Data.History.GetHistLend(userId, id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<HistoricalLend, HistLend>()).CreateMapper();
            var histLend = mapper.Map<HistoricalLend, HistLend>(hist);
            var friend = await Data.Friends.GetFriend(userId, hist.FriendId);
            var thing = await Data.Things.GetThing(userId, hist.ThingId);
            histLend.Friend = friend;
            histLend.Thing = thing;
            return histLend;
        }

        public async Task<IEnumerable<HistLend>> GetHistoricalLends(Guid userId)
        {
            var hists = await Data.History.GetHistLends(userId);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<HistoricalLend, HistLend>()).CreateMapper();
            var histLends = hists.Select(h => mapper.Map<HistoricalLend, HistLend>(h));
            foreach (var hist in histLends)
            {
                var friend = await Data.Friends.GetFriend(userId, hist.Friend.Id);
                var thing = await Data.Things.GetThing(userId, hist.Thing.Id);
                hist.Friend = friend;
                hist.Thing = thing;
            }
            return histLends;
        }

        public async Task<Thing> Update(Guid userId, Guid thingId, Lend lend)
        {
            await Data.Lends.UpdateLend(userId, thingId, lend);
            return await Data.Things.GetThing(userId, thingId);
        }

        private HistoricalLend ReturnThing(Thing thing, DateTime returnDate)
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
