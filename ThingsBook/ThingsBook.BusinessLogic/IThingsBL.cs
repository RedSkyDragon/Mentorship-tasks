using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic
{
    public interface IThingsBL
    {
        Task CreateThing(Guid userId, Thing thing);

        Task UpdateThing(Guid userId, Thing thing);

        Task DeleteThing(Guid userId, Guid id);

        Task<Thing> GetThing(Guid userId, Guid id);

        Task<IEnumerable<Thing>> GetThings(Guid userId);

        Task<IEnumerable<Thing>> GetThingsForCategory(Guid userId, Guid categoryId);

        Task CreateCategory(Guid userId, Category category);

        Task UpdateCategory(Guid userId, Category category);

        Task DeleteCategoryWithThings(Guid userId, Guid id);

        Task DeleteCategoryWithReplacement(Guid userId, Guid categoryId, Guid replacementId);

        Task<Category> GetCategory(Guid userId, Guid id);

        Task<IEnumerable<Category>> GetCategories(Guid userId);
    }
}
