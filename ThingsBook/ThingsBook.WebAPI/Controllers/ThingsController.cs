using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ThingsBook.BusinessLogic;
using ThingsBook.BusinessLogic.Models;
using ThingsBook.Data.Interface;

namespace ThingsBook.WebAPI.Controllers
{
    [RoutePrefix("thing")]
    public class ThingsController : ApiController
    {
        private IThingsBL _things;

        public ThingsController(IThingsBL things)
        {
            _things = things;
        }

        [HttpGet]
        [Route("~/things")]
        public Task<IEnumerable<Thing>> Get([FromUri]Guid userId)
        {
            return _things.GetThings(userId);
        }

        [HttpGet]
        [Route("{thingId:guid}")]
        public Task<Thing> Get([FromUri]Guid userId, [FromUri]Guid thingId)
        {
            return _things.GetThing(userId, thingId);
        }

        [HttpGet]
        [Route("{thingId:guid}/lends")]
        public Task<FilteredLends> GetLends([FromUri]Guid userId, [FromUri]Guid thingId)
        {
            return _things.GetThingLends(userId, thingId);
        }

        [HttpPost]
        [Route("")]
        public async Task<Thing> Post([FromUri]Guid userId, [FromBody]Models.Thing thing)
        {
            var thingDM = new Thing { Name = thing.Name, About = thing.About, CategoryId = thing.CategoryId, UserId = userId };
            await _things.CreateThing(userId, thingDM);
            return await _things.GetThing(userId, thingDM.Id);
        }

        [HttpPut]
        [Route("{thingId:guid}")]
        public async Task<Thing> Put([FromUri]Guid userId, [FromUri]Guid thingId, [FromBody]Models.Thing thing)
        {
            var thingDM = new Thing { Id = thingId, Name = thing.Name, About = thing.About, CategoryId = thing.CategoryId, UserId = userId };
            await _things.UpdateThing(userId, thingDM);
            return await _things.GetThing(userId, thingDM.Id);
        }

        [HttpDelete]
        [Route("{thingId:guid}")]
        public Task Delete([FromUri]Guid userId, [FromUri]Guid thingId)
        {
            return _things.DeleteThing(userId, thingId);
        }
    }
}
