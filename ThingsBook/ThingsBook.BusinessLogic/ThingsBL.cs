using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThingsBook.BusinessLogic.Models;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic
{
    public class ThingsBL : BaseBL, IThingsBL
    {
        public ThingsBL(CommonDAL data) : base(data) { }

        public async Task<Category> CreateCategory(Guid userId, Category category)
        {
            await Data.Categories.CreateCategory(userId, category);
            return await Data.Categories.GetCategory(userId, category.Id);
        }

        public async Task<Thing> CreateThing(Guid userId, Thing thing)
        {
            await Data.Things.CreateThing(userId, thing);
            return await Data.Things.GetThing(userId, thing.Id);
        }

        public async Task DeleteCategoryWithReplacement(Guid userId, Guid categoryId, Guid replacementId)
        {
            await Data.Things.UpdateThingsCategory(userId, categoryId, replacementId);
            await Data.Categories.DeleteCategory(userId, categoryId);
        }

        public async Task DeleteCategoryWithThings(Guid userId, Guid id)
        {          
            await Data.Things.DeleteThingsForCategory(userId, id);
            await Data.Categories.DeleteCategory(userId, id);
        }

        public Task DeleteThing(Guid userId, Guid id)
        {
            return Data.Things.DeleteThing(userId, id);
        }

        public Task<IEnumerable<Category>> GetCategories(Guid userId)
        {
            return Data.Categories.GetCategories(userId);
        }

        public Task<Category> GetCategory(Guid userId, Guid id)
        {
            return Data.Categories.GetCategory(userId, id);
        }

        public Task<Thing> GetThing(Guid userId, Guid id)
        {
            return Data.Things.GetThing(userId, id);
        }

        public Task<IEnumerable<Thing>> GetThings(Guid userId)
        {
            return Data.Things.GetThings(userId);
        }

        public Task<IEnumerable<Thing>> GetThingsForCategory(Guid userId, Guid categoryId)
        {
            return Data.Things.GetThingsForCategory(userId, categoryId);
        }

        public async Task<Category> UpdateCategory(Guid userId, Category category)
        {
            await Data.Categories.UpdateCategory(userId, category);
            return await Data.Categories.GetCategory(userId, category.Id);
        }

        public async Task<Thing> UpdateThing(Guid userId, Thing thing)
        {
            await Data.Things.UpdateThing(userId, thing);
            return await Data.Things.GetThing(userId, thing.Id);
        }

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
