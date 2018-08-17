using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ThingsBook.BusinessLogic;
using ThingsBook.Data.Interface;

namespace ThingsBook.WebAPI.Controllers
{
    [RoutePrefix("category")]
    public class CategoriesController : BaseController
    {
        private IThingsBL _things;

        public CategoriesController(IThingsBL things)
        {
            _things = things;
        }

        [HttpGet]
        [Route("~/categories")]
        public Task<IEnumerable<Category>> Get([FromUri]Guid userId)
        {
            return _things.GetCategories(userId);
        }

        [HttpGet]
        [Route("{categoryId:guid}")]
        public Task<Category> Get([FromUri]Guid userId, [FromUri]Guid categoryId)
        {
            return _things.GetCategory(userId, categoryId);
        }


        [HttpGet]
        [Route("{categoryId:guid}/things")]
        public Task<IEnumerable<Thing>> GetForCategory([FromUri]Guid userId, [FromUri]Guid categoryId)
        {
            return _things.GetThingsForCategory(userId, categoryId);
        }

        [HttpPost]
        [Route("")]
        public async Task<Category> Post([FromUri]Guid userId, [FromBody]Models.Category category)
        {
            Category categoryDM = new Category { UserId = userId, Name = category.Name, About = category.About };
            await _things.CreateCategory(userId, categoryDM);
            return await _things.GetCategory(userId, categoryDM.Id);
        }

        [HttpPut]
        [Route("{categoryId:guid}")]
        public async Task<Category> Put([FromUri]Guid userId, [FromUri]Guid categoryId, [FromBody]Models.Category category)
        {
            Category categoryDM = new Category { Id = categoryId, UserId = userId, Name = category.Name, About = category.About };
            await _things.UpdateCategory(userId, categoryDM);
            return await _things.GetCategory(userId, categoryDM.Id);
        }

        [HttpDelete]
        [Route("{categoryId:guid}")]
        public Task Delete([FromUri]Guid userId, [FromUri]Guid categoryId)
        {
            return _things.DeleteCategoryWithThings(userId, categoryId);
        }
        
        [HttpDelete]
        [Route("{categoryId:guid}")]
        public Task DeleteAndReplace([FromUri]Guid userId, [FromUri]Guid categoryId, [FromUri]Guid replacementId)
        {
            return _things.DeleteCategoryWithReplacement(userId, categoryId, replacementId);
        }
    }
}
