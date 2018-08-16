using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ThingsBook.BusinessLogic;
using ThingsBook.BusinessLogic.Models;
using ThingsBook.Data.Interface;

namespace ThingsBook.WebAPI.Controllers
{
    public class ThingsController : ApiController
    {
        private IThingsBL _things;

        public ThingsController(IThingsBL things)
        {
            _things = things;
        }

        [HttpGet]
        public async Task<IEnumerable<Thing>> Get(Guid userId)
        {
            return await _things.GetThings(userId);
        }

        [HttpGet]
        public async Task<IEnumerable<Thing>> GetForCategory(Guid userId, Guid categoryId)
        {
            return await _things.GetThingsForCategory(userId, categoryId);
        }

        [HttpGet]
        public async Task<Thing> Get(Guid userId, Guid thingId)
        {
            return await _things.GetThing(userId, thingId);
        }

        [HttpGet]
        public async Task<FilteredLends> GetLends(Guid userId, Guid thingId)
        {
            return await _things.GetThingLends(userId, thingId);
        }

        [HttpPost]
        public async Task Post(Guid userId, Thing thing)
        {
            await _things.CreateThing(userId, thing);
        }

        [HttpPut]
        public async Task Put(Guid userId, Thing thing)
        {
            await _things.UpdateThing(userId, thing);
        }

        [HttpDelete]
        public async Task Delete(Guid userId, Guid thingId)
        {
            await _things.DeleteThing(userId, thingId);
        }
    }
}
