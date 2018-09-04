﻿using AutoMapper;
using System;
using System.Collections.Generic;
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
        public LendsBL(Storage data) : base(data) { }

        /// <summary>
        /// Creates the lend for specified thing identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thingId">The thing identifier.</param>
        /// <param name="lend">The lend.</param>
        /// <returns>
        /// Thing with created lend.
        /// </returns>
        public async Task<ThingWithLend> Create(Guid userId, Guid thingId, Models.Lend lend)
        {
            CheckLend(lend);
            await Data.Lends.CreateLend(userId, thingId, ModelsConverter.ToDataModel(lend));
            return ModelsConverter.ToBLModel(await Data.Things.GetThing(userId, thingId));
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
            if (thing.Lend == null)
            {
                throw new ArgumentException("Thing with thingId must has not null lend", nameof(thingId));
            }
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
            var thingMapper = new MapperConfiguration(cfg => cfg.CreateMap<Data.Interface.Thing, Models.Thing>()).CreateMapper();
            var histLend = lendMapper.Map<HistoricalLend, HistLend>(hist);
            var friend = ModelsConverter.ToBLModel(await Data.Friends.GetFriend(userId, hist.FriendId));
            var thing = await Data.Things.GetThing(userId, hist.ThingId);
            histLend.Friend = friend;
            histLend.Thing = thingMapper.Map<Models.Thing>(thing);
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
            var thingMapper = new MapperConfiguration(cfg => cfg.CreateMap<Data.Interface.Thing, Models.Thing>()).CreateMapper();
            var histLends = new List<HistLend>();
            foreach (var hist in hists)
            {
                var friend = ModelsConverter.ToBLModel(await Data.Friends.GetFriend(userId, hist.FriendId));
                var thing = await Data.Things.GetThing(userId, hist.ThingId);
                var histLend = lendMapper.Map<HistoricalLend, HistLend>(hist);
                histLend.Friend = friend;
                histLend.Thing = thingMapper.Map<Models.Thing>(thing);
                histLends.Add(histLend);
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
        public async Task<ThingWithLend> Update(Guid userId, Guid thingId, Models.Lend lend)
        {
            CheckLend(lend);
            await Data.Lends.UpdateLend(userId, thingId, ModelsConverter.ToDataModel(lend));
            return ModelsConverter.ToBLModel(await Data.Things.GetThing(userId, thingId));
        }

        /// <summary>
        /// Returns the thing.
        /// </summary>
        /// <param name="thing">The thing.</param>
        /// <param name="returnDate">The return date.</param>
        /// <returns></returns>
        private HistoricalLend ReturnThing(Data.Interface.Thing thing, DateTime returnDate)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Data.Interface.Lend, HistoricalLend>());
            var hist = config.CreateMapper().Map<Data.Interface.Lend, HistoricalLend>(thing.Lend);
            hist.ReturnDate = returnDate;
            hist.UserId = thing.UserId;
            hist.ThingId = thing.Id;
            return hist;
        }

        /// <summary>
        /// Checks the lend.
        /// </summary>
        /// <param name="lend">The lend.</param>
        /// <exception cref="ArgumentNullException">Lend must not be null. - lend</exception>
        /// <exception cref="ModelValidationException">
        /// Friend id must not be empty/default.
        /// or
        /// Lend date must not be empty/default.
        /// </exception>
        private void CheckLend(Models.Lend lend)
        {
            if (lend == null)
            {
                throw new ArgumentNullException("Lend must not be null.", nameof(lend));
            }
            if (lend.FriendId == default(Guid))
            {
                throw new ModelValidationException("Friend id must not be empty/default.");
            }
            if (lend.LendDate == default(DateTime))
            {
                throw new ModelValidationException("Lend date must not be empty/default.");
            }
        }
    }
}
