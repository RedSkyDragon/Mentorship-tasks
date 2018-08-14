using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic
{
    public interface IThingsBL
    {
        Task CreateThing(Thing thing);

        Task UpdateThing(Thing thing);

        Task DeleteThing(Guid id);

        Task<Thing> GetThing(Guid id);

        Task<IEnumerable<Thing>> GetThings(Guid userId);

        Task<IEnumerable<Thing>> GetThingsForCategory(Guid categoryId);

        Task CreateCategory(Category category);

        Task UpdateCategory(Category category);

        Task DeleteCategoryWithThings(Guid id);

        Task DeleteCategoryWithReplacement(Guid categoryId, Guid replacementId);

        Task<Category> GetCategory(Guid id);

        Task<IEnumerable<Category>> GetCategories(Guid userId);
    }
}
