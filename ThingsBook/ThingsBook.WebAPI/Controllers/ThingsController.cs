using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ThingsBook.BusinessLogic;
using ThingsBook.BusinessLogic.Models;
using ThingsBook.Data.Interface;

namespace ThingsBook.WebAPI.Controllers
{
    [RoutePrefix("things")]
    public class ThingsController : ApiController
    {
        private IThingsBL _things;

        public ThingsController(IThingsBL things)
        {
            _things = things;
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IEnumerable<Thing>> Get(Guid userId)
        {
            return await _things.GetThings(userId);
        }

        [HttpGet]
        [Route("{userId}/category/{categoryId}")]
        public async Task<IEnumerable<Thing>> GetForCategory(Guid userId, Guid categoryId)
        {
            return await _things.GetThingsForCategory(userId, categoryId);
        }

        [HttpGet]
        [Route("{userId}/{thingId}")]
        public async Task<Thing> Get(Guid userId, Guid thingId)
        {
            return await _things.GetThing(userId, thingId);
        }

        [HttpGet]
        [Route("{userId}/{thingId}/lends")]
        public async Task<FilteredLends> GetLends(Guid userId, Guid thingId)
        {
            return await _things.GetThingLends(userId, thingId);
        }

        [HttpPost]
        [Route("{userId}")]
        public async Task Post(Guid userId, Thing thing)
        {
            await _things.CreateThing(userId, thing);
        }

        [HttpPut]
        [Route("{userId}")]
        public async Task Put(Guid userId, Thing thing)
        {
            await _things.UpdateThing(userId, thing);
        }

        [HttpDelete]
        [Route("{userId}/{thingId}")]
        public async Task Delete(Guid userId, Guid thingId)
        {
            await _things.DeleteThing(userId, thingId);
        }
    }
}
