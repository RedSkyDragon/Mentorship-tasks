using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ThingsBook.BusinessLogic;
using ThingsBook.BusinessLogic.Models;

namespace ThingsBook.WebAPI.Controllers
{

    /// <summary>
    /// Controller for things management
    /// </summary>
    /// <seealso cref="ThingsBook.WebAPI.Controllers.BaseController" />
    [RoutePrefix("thing")]
    public class ThingsController : BaseController
    {
        private IThingsBL _things;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThingsController"/> class.
        /// </summary>
        /// <param name="things">The things business logic</param>
        public ThingsController(IThingsBL things)
        {
            _things = things;
        }

        /// <summary>
        /// Gets all things for the specified by identifier user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of things</returns>
        [HttpGet]
        [Route("~/things")]
        public Task<IEnumerable<ThingWithLend>> Get([FromUri]Guid userId)
        {
            return _things.GetThings(userId);
        }

        /// <summary>
        /// Gets the specified by identifier thing.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thingId">The thing identifier.</param>
        /// <returns>Thing</returns>
        [HttpGet]
        [Route("{thingId:guid}")]
        public Task<ThingWithLend> Get([FromUri]Guid userId, [FromUri]Guid thingId)
        {
            return _things.GetThing(userId, thingId);
        }

        /// <summary>
        /// Gets the lend and lends history for the specified by identifier thing.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thingId">The thing identifier.</param>
        /// <returns>Filtered lends</returns>
        [HttpGet]
        [Route("{thingId:guid}/lends")]
        public Task<FilteredLends> GetLends([FromUri]Guid userId, [FromUri]Guid thingId)
        {
            return _things.GetThingLends(userId, thingId);
        }

        /// <summary>
        /// Creates new thing
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thing">The thing information.</param>
        /// <returns>Created thing</returns>
        [HttpPost]
        [Route("")]
        public Task<ThingWithLend> Post([FromUri]Guid userId, [FromBody]Models.Thing thing)
        {
            var thingBL = new ThingWithLend
            {
                Name = thing.Name,
                About = thing.About,
                CategoryId = thing.CategoryId
            };
            return _things.CreateThing(userId, thingBL);
        }

        /// <summary>
        /// Updates the specified by identifier thing.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thingId">The thing identifier.</param>
        /// <param name="thing">The thing information.</param>
        /// <returns>Updated thing</returns>
        [HttpPut]
        [Route("{thingId:guid}")]
        public Task<ThingWithLend> Put([FromUri]Guid userId, [FromUri]Guid thingId, [FromBody]Models.Thing thing)
        {
            var thingBL = new ThingWithLend
            {
                Id = thingId,
                Name = thing.Name,
                About = thing.About,
                CategoryId = thing.CategoryId
            };
            return _things.UpdateThing(userId, thingBL);
        }

        /// <summary>
        /// Deletes the specified by identifier thing.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thingId">The thing identifier.</param>
        /// <returns>204(no content)</returns>
        [HttpDelete]
        [Route("{thingId:guid}")]
        public Task Delete([FromUri]Guid userId, [FromUri]Guid thingId)
        {
            return _things.DeleteThing(userId, thingId);
        }
    }
}
