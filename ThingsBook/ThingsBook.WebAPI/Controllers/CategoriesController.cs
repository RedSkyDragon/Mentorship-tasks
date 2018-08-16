using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ThingsBook.BusinessLogic;
using ThingsBook.Data.Interface;

namespace ThingsBook.WebAPI.Controllers
{
    [RoutePrefix("categories")]
    public class CategoriesController : BaseController
    {
        private IThingsBL _things;

        public CategoriesController(IThingsBL things)
        {
            _things = things;
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IEnumerable<Category>> Get(Guid userId)
        {
            return await _things.GetCategories(userId);
        }

        [HttpGet]
        [Route("{userId}/{categoryId}")]
        public async Task<Category> Get(Guid userId, Guid categoryId)
        {
            return await _things.GetCategory(userId, categoryId);
        }

        [HttpPost]
        [Route("{userId}")]
        public async Task Post(Guid userId, Category category)
        {
            await _things.CreateCategory(userId, category);
        }

        [HttpPut]
        [Route("{userId}")]
        public async Task Put(Guid userId, Category category)
        {
            await _things.UpdateCategory(userId, category);
        }

        [HttpDelete]
        [Route("{userId}/{categoryId}")]
        public async Task Delete(Guid userId, Guid categoryId)
        {
            await _things.DeleteCategoryWithThings(userId, categoryId);
        }
        
        [HttpDelete]
        [Route("{userId}/{categoryId}/{replacementId}")]
        public async Task DeleteAndReplace(Guid userId, Guid categoryId, Guid replacementId)
        {
            await _things.DeleteCategoryWithReplacement(userId, categoryId, replacementId);
        }
    }
}
