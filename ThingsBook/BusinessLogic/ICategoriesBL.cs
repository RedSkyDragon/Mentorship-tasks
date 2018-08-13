using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThingsBook.DataAccessInterface;

namespace BusinessLogic
{
    public interface ICategoriesBL
    {
        IEnumerable<Category> GetCategories(Guid userId);

        Category GetCategory(Guid userId, Guid id);

        void UpdateCategory(Guid userId, Category category);

        void DeleteCategory(Guid userId, Guid id);

        void CreateCategory(Guid userId, Category category);

    }
}
