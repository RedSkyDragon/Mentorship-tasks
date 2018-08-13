using System;
using System.Collections.Generic;

namespace ThingsBook.Data.Interface
{
    public interface ICategories
    {
        IEnumerable<Category> GetCategories(Guid userId);

        Category GetCategory(Guid userId, Guid id);

        void UpdateCategory(Guid userId, Category category);

        void DeleteCategory(Guid userId, Guid id);

        void CreateCategory(Guid userId, Category category);

    }
}
