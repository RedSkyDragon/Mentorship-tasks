using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThingsBook.BusinessLogic.Models;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic
{
    public class ThingsBL : BaseBL, IThingsBL
    {
        public ThingsBL(CommonDAL data) : base(data) { }

        public async Task CreateCategory(Guid userId, Category category)
        {
            await Data.Categories.CreateCategory(userId, category);
        }

        public async Task CreateThing(Guid userId, Thing thing)
        {
            await Data.Things.CreateThing(userId, thing);
        }

        public async Task DeleteCategoryWithReplacement(Guid userId, Guid categoryId, Guid replacementId)
        {
            await Data.Things.UpdateThingsCategory(userId, categoryId, replacementId);
            await Data.Categories.DeleteCategory(userId, categoryId);
        }

        public async Task DeleteCategoryWithThings(Guid userId, Guid id)
        {
            var delCat = Data.Categories.DeleteCategory(userId, id);
            var delThings = Data.Things.DeleteThingsForCategory(userId, id);
            await Task.WhenAll(delCat, delThings);
        }

        public async Task DeleteThing(Guid userId, Guid id)
        {
            await Data.Things.DeleteThing(userId, id);
        }

        public async Task<IEnumerable<Category>> GetCategories(Guid userId)
        {
            return await Data.Categories.GetCategories(userId);
        }

        public async Task<Category> GetCategory(Guid userId, Guid id)
        {
            return await Data.Categories.GetCategory(userId, id);
        }

        public async Task<Thing> GetThing(Guid userId, Guid id)
        {
            return await Data.Things.GetThing(userId, id);
        }

        public async Task<IEnumerable<Thing>> GetThings(Guid userId)
        {
            return await Data.Things.GetThings(userId);
        }

        public async Task<IEnumerable<Thing>> GetThingsForCategory(Guid userId, Guid categoryId)
        {
            return await Data.Things.GetThingsForCategory(userId, categoryId);
        }

        public async Task UpdateCategory(Guid userId, Category category)
        {
            await Data.Categories.UpdateCategory(userId, category);
        }

        public async Task UpdateThing(Guid userId, Thing thing)
        {
            await Data.Things.UpdateThing(userId, thing);
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
                History = await HistLendsForThing(userId, hists.Result, thing.Result)
            };
        }

        private async Task<IEnumerable<HistLend>> HistLendsForThing(Guid userId, IEnumerable<HistoricalLend> hists, Thing thing)
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

    }
}
