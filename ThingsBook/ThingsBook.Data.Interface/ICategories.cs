using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThingsBook.Data.Interface
{
    public interface ICategories
    {
        Task<IEnumerable<Category>> GetCategories(Guid userId);

        Task<Category> GetCategory(Guid userId, Guid id);

        Task UpdateCategory(Guid userId, Category category);

        Task DeleteCategory(Guid userId, Guid id);

        Task DeleteCategories(Guid userId);

        Task CreateCategory(Guid userId, Category category);

    }
}
