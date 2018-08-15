using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThingsBook.Data.Interface
{
    public interface ICategories
    {
        Task<IEnumerable<Category>> GetCategories(Guid userId);

        Task<Category> GetCategory(Guid id);

        Task UpdateCategory(Category category);

        Task DeleteCategory(Guid id);

        Task DeleteCategories(Guid userId);

        Task CreateCategory(Category category);

    }
}
