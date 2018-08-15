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
        public ThingsBL(CommonDAL data)
        {
            _data = data;
        }

        public async Task CreateCategory(Category category)
        {
            await _data.Categories.CreateCategory(category);
        }

        public async Task CreateThing(Thing thing)
        {
            await _data.Things.CreateThing(thing);
        }

        public async Task DeleteCategoryWithReplacement(Guid categoryId, Guid replacementId)
        {
            var tasks = new List<Task>();
            tasks.Add(_data.Categories.DeleteCategory(categoryId));
            var things = await _data.Things.GetThingsForCategory(categoryId);
            foreach (var thing in things)
            {
                thing.CategoryId = replacementId;
                tasks.Add(_data.Things.UpdateThing(thing));
            }
            await Task.WhenAll(tasks);
        }

        public async Task DeleteCategoryWithThings(Guid id)
        {
            var delCat = _data.Categories.DeleteCategory(id);
            var delThings = _data.Things.DeleteThingsForCategory(id);
            await Task.WhenAll(delCat, delThings);
        }

        public async Task DeleteThing(Guid id)
        {
            await _data.Things.DeleteThing(id);
        }

        public async Task<IEnumerable<Category>> GetCategories(Guid userId)
        {
            return await _data.Categories.GetCategories(userId);
        }

        public async Task<Category> GetCategory(Guid id)
        {
            return await _data.Categories.GetCategory(id);
        }

        public async Task<Thing> GetThing(Guid id)
        {
            return await _data.Things.GetThing(id);
        }

        public async Task<IEnumerable<Thing>> GetThings(Guid userId)
        {
            return await _data.Things.GetThings(userId);
        }

        public async Task<IEnumerable<Thing>> GetThingsForCategory(Guid categoryId)
        {
            return await _data.Things.GetThingsForCategory(categoryId);
        }

        public async Task UpdateCategory(Category category)
        {
            await _data.Categories.UpdateCategory(category);
        }

        public async Task UpdateThing(Thing thing)
        {
            await _data.Things.UpdateThing(thing);
        }
    }
}
