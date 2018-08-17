using System;
using System.Threading.Tasks;
using System.Web.Http;
using ThingsBook.BusinessLogic;
using ThingsBook.Data.Interface;

namespace ThingsBook.WebAPI.Controllers
{
    [RoutePrefix("lend")]
    public class LendsController : ApiController
    {
        private ILendsBL _lends;
        private IThingsBL _things;

        public LendsController(ILendsBL lends, IThingsBL things)
        {
            _lends = lends;
            _things = things;
        }

        [HttpPost]
        [Route("{thingId:guid}")]
        public async Task<Thing> Post([FromUri]Guid userId, [FromUri]Guid thingId, [FromBody]Lend lend)
        {
            await _lends.Create(userId, thingId, lend);
            return await _things.GetThing(userId, thingId);
        }

        [HttpPut]
        [Route("{thingId:guid}")]
        public async Task<Thing> Put([FromUri]Guid userId, [FromUri]Guid thingId, [FromBody]Lend lend)
        {
            await _lends.Update(userId, thingId, lend);
            return await _things.GetThing(userId, thingId);
        }

        [HttpDelete]
        [Route("{thingId:guid}")]
        public Task Delete([FromUri]Guid userId, [FromUri]Guid thingId, [FromBody]DateTime? returnDate)
        {
            if (!returnDate.HasValue)
            {
                returnDate = DateTime.Now;
            }
            return _lends.Delete(userId, thingId, returnDate.Value);
        }
    }
}
