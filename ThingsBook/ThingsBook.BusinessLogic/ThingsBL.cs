using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            var tasks = new List<Task>();
            tasks.Add(Data.Categories.DeleteCategory(userId, categoryId));
            var things = await Data.Things.GetThingsForCategory(userId, categoryId);
            foreach (var thing in things)
            {
                thing.CategoryId = replacementId;
                tasks.Add(Data.Things.UpdateThing(userId, thing));
            }
            await Task.WhenAll(tasks);
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
    }
}
