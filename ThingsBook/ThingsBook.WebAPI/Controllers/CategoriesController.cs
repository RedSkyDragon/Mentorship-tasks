using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ThingsBook.BusinessLogic;
using ThingsBook.Data.Interface;

namespace ThingsBook.WebAPI.Controllers
{
    /// <summary>
    /// Controller for categories management
    /// </summary>
    /// <seealso cref="ThingsBook.WebAPI.Controllers.BaseController" />
    [RoutePrefix("category")]
    public class CategoriesController : BaseController
    {
        private IThingsBL _things;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriesController"/> class.
        /// </summary>
        /// <param name="things">The things business logic class</param>
        public CategoriesController(IThingsBL things)
        {
            _things = things;
        }

        /// <summary>
        /// Gets all categories for specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of categories</returns>
        [HttpGet]
        [Route("~/categories")]
        public Task<IEnumerable<Category>> Get([FromUri]Guid userId)
        {
            return _things.GetCategories(userId);
        }

        /// <summary>
        /// Gets the specified category by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>Category</returns>
        [HttpGet]
        [Route("{categoryId:guid}")]
        public Task<Category> Get([FromUri]Guid userId, [FromUri]Guid categoryId)
        {
            return _things.GetCategory(userId, categoryId);
        }


        /// <summary>
        /// Gets all things for specified category identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>List of things</returns>
        [HttpGet]
        [Route("{categoryId:guid}/things")]
        public Task<IEnumerable<Thing>> GetForCategory([FromUri]Guid userId, [FromUri]Guid categoryId)
        {
            return _things.GetThingsForCategory(userId, categoryId);
        }

        /// <summary>
        /// Creates specified category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="category">The category information.</param>
        /// <returns>Created category</returns>
        [HttpPost]
        [Route("")]
        public Task<Category> Post([FromUri]Guid userId, [FromBody]Models.Category category)
        {
            Category categoryDM = new Category
            {
                UserId = userId,
                Name = category.Name,
                About = category.About
            };
            return _things.CreateCategory(userId, categoryDM);
        }

        /// <summary>
        /// Updates the specified category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="category">The category information</param>
        /// <returns>Updated category</returns>
        [HttpPut]
        [Route("{categoryId:guid}")]
        public Task<Category> Put([FromUri]Guid userId, [FromUri]Guid categoryId, [FromBody]Models.Category category)
        {
            Category categoryDM = new Category
            {
                Id = categoryId,
                UserId = userId,
                Name = category.Name,
                About = category.About
            };
            return _things.UpdateCategory(userId, categoryDM);
        }

        /// <summary>
        /// Deletes the specified category by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>204(no content)</returns>
        [HttpDelete]
        [Route("{categoryId:guid}")]
        public Task Delete([FromUri]Guid userId, [FromUri]Guid categoryId)
        {
            return _things.DeleteCategoryWithThings(userId, categoryId);
        }

        /// <summary>
        /// Deletes the specified category and replace category for all things in this category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="replacementId">The replacement category identifier.</param>
        /// <returns>204(no content)</returns>
        [HttpDelete]
        [Route("{categoryId:guid}/replace")]
        public Task DeleteAndReplace([FromUri]Guid userId, [FromUri]Guid categoryId, [FromUri]Guid replacementId)
        {
            return _things.DeleteCategoryWithReplacement(userId, categoryId, replacementId);
        }
    }
}
