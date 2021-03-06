﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <param name="storage">The storage.</param>
        public ThingsBL(Storage storage) : base(storage) { }

        /// <summary>
        /// Creates the category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="category">The category.</param>
        /// <returns>
        /// Created category.
        /// </returns>
        public async Task<Models.Category> CreateCategory(Guid userId, Models.Category category)
        {
            CheckCategory(category);
            await Storage.Categories.CreateCategory(userId, ModelsConverter.ToDataModel(category, userId));
            return ModelsConverter.ToBLModel(await Storage.Categories.GetCategory(userId, category.Id));
        }

        /// <summary>
        /// Creates the thing.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thing">The thing.</param>
        /// <returns>
        /// Created thing.
        /// </returns>
        public async Task<ThingWithLend> CreateThing(Guid userId, ThingWithLend thing)
        {
            CheckThing(thing);
            await Storage.Things.CreateThing(userId, ModelsConverter.ToDataModel(thing, userId));
            return ModelsConverter.ToBLModel(await Storage.Things.GetThing(userId, thing.Id));
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
            await Storage.Things.UpdateThingsCategory(userId, categoryId, replacementId);
            await Storage.Categories.DeleteCategory(userId, categoryId);
        }

        /// <summary>
        /// Deletes the category with things.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The category identifier.</param>
        /// <returns></returns>
        public async Task DeleteCategoryWithThings(Guid userId, Guid id)
        {
            await Storage.Things.DeleteThingsForCategory(userId, id);
            await Storage.Categories.DeleteCategory(userId, id);
        }

        /// <summary>
        /// Deletes the thing.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The thing identifier.</param>
        /// <returns></returns>
        public async Task DeleteThing(Guid userId, Guid id)
        {
            await Storage.History.DeleteThingHistory(userId, id);
            await Storage.Things.DeleteThing(userId, id);
        }

        /// <summary>
        /// Gets all categories of user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// List of categories
        /// </returns>
        public async Task<IEnumerable<Models.Category>> GetCategories(Guid userId)
        {
            var result = await Storage.Categories.GetCategories(userId);     
            return result.Select(ModelsConverter.ToBLModel);
        }

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The category identifier.</param>
        /// <returns>
        /// Requested category
        /// </returns>
        public async Task<Models.Category> GetCategory(Guid userId, Guid id)
        {
            return ModelsConverter.ToBLModel(await Storage.Categories.GetCategory(userId, id));
        }

        /// <summary>
        /// Gets the thing.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The thing identifier.</param>
        /// <returns>
        /// Requested thing.
        /// </returns>
        public async Task<ThingWithLend> GetThing(Guid userId, Guid id)
        {
            return ModelsConverter.ToBLModel(await Storage.Things.GetThing(userId, id));
        }

        /// <summary>
        /// Gets all things for user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// List of things.
        /// </returns>
        public async Task<IEnumerable<ThingWithLend>> GetThings(Guid userId)
        {
            var result = await Storage.Things.GetThings(userId);
            return result.Select(ModelsConverter.ToBLModel);
        }

        /// <summary>
        /// Gets the things of requested category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>
        /// List of things.
        /// </returns>
        public async Task<IEnumerable<ThingWithLend>> GetThingsForCategory(Guid userId, Guid categoryId)
        {
            var result = await Storage.Things.GetThingsForCategory(userId, categoryId);
            return result.Select(ModelsConverter.ToBLModel);
        }

        /// <summary>
        /// Updates the category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="category">The category.</param>
        /// <returns>
        /// Updated category
        /// </returns>
        public async Task<Models.Category> UpdateCategory(Guid userId, Models.Category category)
        {
            CheckCategory(category);
            await Storage.Categories.UpdateCategory(userId, ModelsConverter.ToDataModel(category, userId));
            return ModelsConverter.ToBLModel(await Storage.Categories.GetCategory(userId, category.Id));
        }

        /// <summary>
        /// Updates the thing.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thing">The thing.</param>
        /// <returns>
        /// Updated thing.
        /// </returns>
        public async Task<ThingWithLend> UpdateThing(Guid userId, ThingWithLend thing)
        {
            CheckThing(thing);
            await Storage.Things.UpdateThing(userId, ModelsConverter.ToDataModel(thing, userId));
            return ModelsConverter.ToBLModel(await Storage.Things.GetThing(userId, thing.Id));
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
            var thing = ModelsConverter.ToBLModel(await Storage.Things.GetThing(userId, thingId));
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
        private async Task<IEnumerable<HistLend>> GetHistoryLends(Guid userId, ThingWithLend thing)
        {
            var history = await Storage.History.GetThingHistLends(userId, thing.Id);
            var lendMapper = new MapperConfiguration(cfg => cfg.CreateMap<HistoricalLend, HistLend>()).CreateMapper();
            var thingMapper = new MapperConfiguration(cfg => cfg.CreateMap<ThingWithLend, Models.Thing>()).CreateMapper();
            var lends = new List<HistLend>();
            foreach (var item in history)
            {
                var lend = lendMapper.Map<HistoricalLend, HistLend>(item.Key);
                lend.Friend = ModelsConverter.ToBLModel(item.Value);
                lend.Thing = thingMapper.Map<Models.Thing>(thing);
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
        private async Task<ActiveLend> GetActiveLends(Guid userId, ThingWithLend thing)
        {
            var lendMapper = new MapperConfiguration(cfg => cfg.CreateMap<Models.Lend, ActiveLend>()).CreateMapper();
            var thingMapper = new MapperConfiguration(cfg => cfg.CreateMap<ThingWithLend, Models.Thing>()).CreateMapper();
            var lendBL = lendMapper.Map<Models.Lend, ActiveLend>(thing.Lend);
            var friend = await Storage.Friends.GetFriend(userId, thing.Lend.FriendId);
            lendBL.Friend = ModelsConverter.ToBLModel(friend);
            lendBL.Thing = thingMapper.Map<Models.Thing>(thing);
            return lendBL;
        }

        /// <summary>
        /// Checks the category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <exception cref="ArgumentNullException">Category must not be null. - category</exception>
        /// <exception cref="ModelValidationException">Category must not be empty.</exception>
        private void CheckCategory(Models.Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Category must not be null.");
            }
            if (string.IsNullOrEmpty(category.Name) && string.IsNullOrEmpty(category.About))
            {
                throw new ModelValidationException("Category must not be empty.");
            }
        }

        /// <summary>
        /// Checks the thing.
        /// </summary>
        /// <param name="thing">The thing.</param>
        /// <exception cref="ArgumentNullException">Thing must not be null. - thing</exception>
        /// <exception cref="ModelValidationException">Thing must not be empty.</exception>
        private void CheckThing(ThingWithLend thing)
        {
            if (thing == null)
            {
                throw new ArgumentNullException(nameof(thing), "Thing must not be null.");
            }
            if (string.IsNullOrEmpty(thing.Name) && string.IsNullOrEmpty(thing.About))
            {
                throw new ModelValidationException("Thing must not be empty.");
            }
        }
    }
}
