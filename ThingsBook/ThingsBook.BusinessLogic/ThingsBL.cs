using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic
{
    public class ThingsBL : BaseBL, IThingsBL
    {
        public ThingsBL(CommonDAL data) : base(data) { }

        public async Task CreateCategory(Category category)
        {
            await Data.Categories.CreateCategory(category);
        }

        public async Task CreateThing(Thing thing)
        {
            await Data.Things.CreateThing(thing);
        }

        public async Task DeleteCategoryWithReplacement(Guid categoryId, Guid replacementId)
        {
            var tasks = new List<Task>();
            tasks.Add(Data.Categories.DeleteCategory(categoryId));
            var things = await Data.Things.GetThingsForCategory(categoryId);
            foreach (var thing in things)
            {
                thing.CategoryId = replacementId;
                tasks.Add(Data.Things.UpdateThing(thing));
            }
            await Task.WhenAll(tasks);
        }

        public async Task DeleteCategoryWithThings(Guid id)
        {
            var delCat = Data.Categories.DeleteCategory(id);
            var delThings = Data.Things.DeleteThingsForCategory(id);
            await Task.WhenAll(delCat, delThings);
        }

        public async Task DeleteThing(Guid id)
        {
            await Data.Things.DeleteThing(id);
        }

        public async Task<IEnumerable<Category>> GetCategories(Guid userId)
        {
            return await Data.Categories.GetCategories(userId);
        }

        public async Task<Category> GetCategory(Guid id)
        {
            return await Data.Categories.GetCategory(id);
        }

        public async Task<Thing> GetThing(Guid id)
        {
            return await Data.Things.GetThing(id);
        }

        public async Task<IEnumerable<Thing>> GetThings(Guid userId)
        {
            return await Data.Things.GetThings(userId);
        }

        public async Task<IEnumerable<Thing>> GetThingsForCategory(Guid categoryId)
        {
            return await Data.Things.GetThingsForCategory(categoryId);
        }

        public async Task UpdateCategory(Category category)
        {
            await Data.Categories.UpdateCategory(category);
        }

        public async Task UpdateThing(Thing thing)
        {
            await Data.Things.UpdateThing(thing);
        }
    }
}
