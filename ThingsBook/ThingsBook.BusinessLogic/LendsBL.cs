using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThingsBook.BusinessLogic.Models;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic
{
    /// <summary>
    /// Implementation of ILendsBL interface.
    /// </summary>
    /// <seealso cref="ThingsBook.BusinessLogic.BaseBL" />
    /// <seealso cref="ThingsBook.BusinessLogic.ILendsBL" />
    public class LendsBL : BaseBL, ILendsBL
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LendsBL"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public LendsBL(CommonDAL data) : base(data) { }

        /// <summary>
        /// Creates the lend for specified thing identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thingId">The thing identifier.</param>
        /// <param name="lend">The lend.</param>
        /// <returns>
        /// Thing with created lend.
        /// </returns>
        public async Task<Thing> Create(Guid userId, Guid thingId, Lend lend)
        {
            await Data.Lends.CreateLend(userId, thingId, lend);
            return await Data.Things.GetThing(userId, thingId);
        }

        /// <summary>
        /// Deletes the specified lend record and creates history record.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thingId">The thing identifier.</param>
        /// <param name="returnDate">The return date.</param>
        /// <returns></returns>
        public async Task Delete(Guid userId, Guid thingId, DateTime returnDate)
        {
            var thing = await Data.Things.GetThing(userId, thingId);
            var historyLend = ReturnThing(thing, returnDate);
            await Data.History.CreateHistLend(userId, historyLend);
            await Data.Lends.DeleteLend(userId, thingId);
        }

        /// <summary>
        /// Deletes the historical lend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The lend identifier.</param>
        /// <returns></returns>
        public Task DeleteHistoricalLend(Guid userId, Guid id)
        {
            return Data.History.DeleteHistLend(userId, id);
        }

        /// <summary>
        /// Gets the historical lend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The lend identifier.</param>
        /// <returns>
        /// Historacal record
        /// </returns>
        public async Task<HistLend> GetHistoricalLend(Guid userId, Guid id)
        {
            var hist = await Data.History.GetHistLend(userId, id);
            var lendMapper = new MapperConfiguration(cfg => cfg.CreateMap<HistoricalLend, HistLend>()).CreateMapper();
            var thingMapper = new MapperConfiguration(cfg => cfg.CreateMap<Thing, ThingWithoutLend>()).CreateMapper();
            var histLend = lendMapper.Map<HistoricalLend, HistLend>(hist);
            var friend = await Data.Friends.GetFriend(userId, hist.FriendId);
            var thing = await Data.Things.GetThing(userId, hist.ThingId);
            histLend.Friend = friend;
            histLend.Thing = thingMapper.Map<ThingWithoutLend>(thing);
            return histLend;
        }

        /// <summary>
        /// Gets all historical lends for user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// List of historical lends
        /// </returns>
        public async Task<IEnumerable<HistLend>> GetHistoricalLends(Guid userId)
        {
            var hists = await Data.History.GetHistLends(userId);
            var lendMapper = new MapperConfiguration(cfg => cfg.CreateMap<HistoricalLend, HistLend>()).CreateMapper();
            var thingMapper = new MapperConfiguration(cfg => cfg.CreateMap<Thing, ThingWithoutLend>()).CreateMapper();
            var histLends = hists.Select(h => lendMapper.Map<HistoricalLend, HistLend>(h));
            foreach (var hist in histLends)
            {
                var friend = await Data.Friends.GetFriend(userId, hist.Friend.Id);
                var thing = await Data.Things.GetThing(userId, hist.Thing.Id);
                hist.Friend = friend;
                hist.Thing = thingMapper.Map<ThingWithoutLend>(thing);
            }
            return histLends;
        }

        /// <summary>
        /// Updates the lend for specified thing identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thingId">The thing identifier.</param>
        /// <param name="lend">The lend.</param>
        /// <returns>
        /// Updated thing with lend
        /// </returns>
        public async Task<Thing> Update(Guid userId, Guid thingId, Lend lend)
        {
            await Data.Lends.UpdateLend(userId, thingId, lend);
            return await Data.Things.GetThing(userId, thingId);
        }

        /// <summary>
        /// Returns the thing.
        /// </summary>
        /// <param name="thing">The thing.</param>
        /// <param name="returnDate">The return date.</param>
        /// <returns></returns>
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
