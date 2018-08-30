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
    [Authorize]
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
        /// <returns>List of things</returns>
        [HttpGet]
        [Route("~/things")]
        public Task<IEnumerable<ThingWithLend>> Get()
        {
            return _things.GetThings(ApiUser.Id);
        }

        /// <summary>
        /// Gets the specified by identifier thing.
        /// </summary>
        /// <param name="thingId">The thing identifier.</param>
        /// <returns>Thing</returns>
        [HttpGet]
        [Route("{thingId:guid}")]
        public Task<ThingWithLend> Get([FromUri]Guid thingId)
        {
            return _things.GetThing(ApiUser.Id, thingId);
        }

        /// <summary>
        /// Gets the lend and lends history for the specified by identifier thing.
        /// </summary>
        /// <param name="thingId">The thing identifier.</param>
        /// <returns>Filtered lends</returns>
        [HttpGet]
        [Route("{thingId:guid}/lends")]
        public Task<FilteredLends> GetLends([FromUri]Guid thingId)
        {
            return _things.GetThingLends(ApiUser.Id, thingId);
        }

        /// <summary>
        /// Creates new thing
        /// </summary>
        /// <param name="thing">The thing information.</param>
        /// <returns>Created thing</returns>
        [HttpPost]
        [Route("")]
        public Task<ThingWithLend> Post([FromBody]Models.Thing thing)
        {
            var thingBL = new ThingWithLend
            {
                Name = thing.Name,
                About = thing.About,
                CategoryId = thing.CategoryId
            };
            return _things.CreateThing(ApiUser.Id, thingBL);
        }

        /// <summary>
        /// Updates the specified by identifier thing.
        /// </summary>
        /// <param name="thingId">The thing identifier.</param>
        /// <param name="thing">The thing information.</param>
        /// <returns>Updated thing</returns>
        [HttpPut]
        [Route("{thingId:guid}")]
        public Task<ThingWithLend> Put([FromUri]Guid thingId, [FromBody]Models.Thing thing)
        {
            var thingBL = new ThingWithLend
            {
                Id = thingId,
                Name = thing.Name,
                About = thing.About,
                CategoryId = thing.CategoryId
            };
            return _things.UpdateThing(ApiUser.Id, thingBL);
        }

        /// <summary>
        /// Deletes the specified by identifier thing.
        /// </summary>
        /// <param name="thingId">The thing identifier.</param>
        /// <returns>204(no content)</returns>
        [HttpDelete]
        [Route("{thingId:guid}")]
        public Task Delete([FromUri]Guid thingId)
        {
            return _things.DeleteThing(ApiUser.Id, thingId);
        }
    }
}
