using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ThingsBook.BusinessLogic;
using ThingsBook.BusinessLogic.Models;

namespace ThingsBook.WebAPI.Controllers
{
    /// <summary>
    /// Controller for categories management
    /// </summary>
    /// <seealso cref="ThingsBook.WebAPI.Controllers.BaseController" />
    [RoutePrefix("category")]
    [Authorize]
    public class CategoriesController : BaseController
    {
        private readonly IThingsBL _things;

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
        /// <returns>List of categories</returns>
        [HttpGet]
        [Route("~/categories")]
        public Task<IEnumerable<Category>> Get()
        {
            return _things.GetCategories(ApiUser.Id);
        }

        /// <summary>
        /// Gets the specified category by identifier.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>Category</returns>
        [HttpGet]
        [Route("{categoryId:guid}")]
        public Task<Category> Get(Guid categoryId)
        {
            return _things.GetCategory(ApiUser.Id, categoryId);
        }


        /// <summary>
        /// Gets all things for specified category identifier.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>List of things</returns>
        [HttpGet]
        [Route("{categoryId:guid}/things")]
        public Task<IEnumerable<ThingWithLend>> GetForCategory(Guid categoryId)
        {
            return _things.GetThingsForCategory(ApiUser.Id, categoryId);
        }

        /// <summary>
        /// Creates specified category.
        /// </summary>
        /// <param name="category">The category information.</param>
        /// <returns>Created category</returns>
        [HttpPost]
        [Route("")]
        public Task<Category> Post([FromBody]Models.Category category)
        {
            var categoryBL = new Category
            {
                Name = category.Name,
                About = category.About
            };
            return _things.CreateCategory(ApiUser.Id, categoryBL);
        }

        /// <summary>
        /// Updates the specified category.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="category">The category information</param>
        /// <returns>Updated category</returns>
        [HttpPut]
        [Route("{categoryId:guid}")]
        public Task<Category> Put(Guid categoryId, [FromBody]Models.Category category)
        {
            var categoryBL = new Category
            {
                Id = categoryId,
                Name = category.Name,
                About = category.About
            };
            return _things.UpdateCategory(ApiUser.Id, categoryBL);
        }

        /// <summary>
        /// Deletes the specified category by identifier.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>204(no content)</returns>
        [HttpDelete]
        [Route("{categoryId:guid}")]
        public Task Delete(Guid categoryId)
        {
            return _things.DeleteCategoryWithThings(ApiUser.Id, categoryId);
        }

        /// <summary>
        /// Deletes the specified category and replace category for all things in this category.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="replacementId">The replacement category identifier.</param>
        /// <returns>204(no content)</returns>
        [HttpDelete]
        [Route("{categoryId:guid}/replace")]
        public Task DeleteAndReplace(Guid categoryId, [FromUri]Guid replacementId)
        {
            return _things.DeleteCategoryWithReplacement(ApiUser.Id, categoryId, replacementId);
        }
    }
}
