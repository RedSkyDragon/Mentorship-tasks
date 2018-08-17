﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThingsBook.BusinessLogic.Models;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic
{
    /// <summary>
    /// Implementation of IThingsBL interface.
    /// </summary>
    /// <seealso cref="ThingsBook.BusinessLogic.BaseBL" />
    /// <seealso cref="ThingsBook.BusinessLogic.IThingsBL" />
    public class ThingsBL : BaseBL, IThingsBL
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThingsBL"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public ThingsBL(CommonDAL data) : base(data) { }

        /// <summary>
        /// Creates the category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="category">The category.</param>
        /// <returns>
        /// Created category.
        /// </returns>
        public async Task<Category> CreateCategory(Guid userId, Category category)
        {
            await Data.Categories.CreateCategory(userId, category);
            return await Data.Categories.GetCategory(userId, category.Id);
        }

        /// <summary>
        /// Creates the thing.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thing">The thing.</param>
        /// <returns>
        /// Created thing.
        /// </returns>
        public async Task<Thing> CreateThing(Guid userId, Thing thing)
        {
            await Data.Things.CreateThing(userId, thing);
            return await Data.Things.GetThing(userId, thing.Id);
        }

        /// <summary>
        /// Deletes the category with replacement things category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="replacementId">The replacement category identifier.</param>
        /// <returns></returns>
        public async Task DeleteCategoryWithReplacement(Guid userId, Guid categoryId, Guid replacementId)
        {
            await Data.Things.UpdateThingsCategory(userId, categoryId, replacementId);
            await Data.Categories.DeleteCategory(userId, categoryId);
        }

        /// <summary>
        /// Deletes the category with things.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The category identifier.</param>
        /// <returns></returns>
        public async Task DeleteCategoryWithThings(Guid userId, Guid id)
        {
            await Data.Things.DeleteThingsForCategory(userId, id);
            await Data.Categories.DeleteCategory(userId, id);
        }

        /// <summary>
        /// Deletes the thing.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The thing identifier.</param>
        /// <returns></returns>
        public Task DeleteThing(Guid userId, Guid id)
        {
            return Data.Things.DeleteThing(userId, id);
        }

        /// <summary>
        /// Gets all categories of user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// List of categories
        /// </returns>
        public Task<IEnumerable<Category>> GetCategories(Guid userId)
        {
            return Data.Categories.GetCategories(userId);
        }

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The category identifier.</param>
        /// <returns>
        /// Requested category
        /// </returns>
        public Task<Category> GetCategory(Guid userId, Guid id)
        {
            return Data.Categories.GetCategory(userId, id);
        }

        /// <summary>
        /// Gets the thing.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The thing identifier.</param>
        /// <returns>
        /// Requested thing.
        /// </returns>
        public Task<Thing> GetThing(Guid userId, Guid id)
        {
            return Data.Things.GetThing(userId, id);
        }

        /// <summary>
        /// Gets all things for user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// List of things.
        /// </returns>
        public Task<IEnumerable<Thing>> GetThings(Guid userId)
        {
            return Data.Things.GetThings(userId);
        }

        /// <summary>
        /// Gets the things of requested category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>
        /// List of things.
        /// </returns>
        public Task<IEnumerable<Thing>> GetThingsForCategory(Guid userId, Guid categoryId)
        {
            return Data.Things.GetThingsForCategory(userId, categoryId);
        }

        /// <summary>
        /// Updates the category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="category">The category.</param>
        /// <returns>
        /// Updated category
        /// </returns>
        public async Task<Category> UpdateCategory(Guid userId, Category category)
        {
            await Data.Categories.UpdateCategory(userId, category);
            return await Data.Categories.GetCategory(userId, category.Id);
        }

        /// <summary>
        /// Updates the thing.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thing">The thing.</param>
        /// <returns>
        /// Updated thing.
        /// </returns>
        public async Task<Thing> UpdateThing(Guid userId, Thing thing)
        {
            await Data.Things.UpdateThing(userId, thing);
            return await Data.Things.GetThing(userId, thing.Id);
        }

        /// <summary>
        /// Gets the thing active lend and lends history.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thingId">The thing identifier.</param>
        /// <returns>
        /// Filtered lends
        /// </returns>
        public async Task<FilteredLends> GetThingLends(Guid userId, Guid thingId)
        {
            var activeLends = new List<ActiveLend>();
            var thing = await Data.Things.GetThing(userId, thingId);
            if (thing.Lend != null)
            {
                activeLends.Add(await GetActiveLends(userId, thing));
            }
            return new FilteredLends
            {
                ActiveLends = activeLends,
                History = await GetHistoryLends(userId, thing)
            };
        }

        /// <summary>
        /// Gets the history lends.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thing">The thing.</param>
        /// <returns></returns>
        private async Task<IEnumerable<HistLend>> GetHistoryLends(Guid userId, Thing thing)
        {
            var history = await Data.History.GetThingHistLends(userId, thing.Id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<HistoricalLend, HistLend>()).CreateMapper();
            var lends = new List<HistLend>();
            foreach (var item in history)
            {
                var lend = mapper.Map<HistoricalLend, HistLend>(item.Key);
                lend.Friend = item.Value;
                lend.Thing = thing;
                lends.Add(lend);
            }
            return lends;
        }

        /// <summary>
        /// Gets the active lends.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thing">The thing.</param>
        /// <returns></returns>
        private async Task<ActiveLend> GetActiveLends(Guid userId, Thing thing)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Lend, ActiveLend>());
            var lendBL = config.CreateMapper().Map<Lend, ActiveLend>(thing.Lend);
            var friend = await Data.Friends.GetFriend(userId, thing.Lend.FriendId);
            lendBL.Friend = friend;
            lendBL.Thing = thing;
            return lendBL;
        }
    }
}
