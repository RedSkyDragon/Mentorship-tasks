using System;
using System.Threading.Tasks;
using System.Web.Http;
using ThingsBook.BusinessLogic;
using ThingsBook.Data.Interface;

namespace ThingsBook.WebAPI.Controllers
{
    [RoutePrefix("lends")]
    public class LendsController : ApiController
    {
        private ILendsBL _lends;

        public LendsController(ILendsBL lends)
        {
            _lends = lends;
        }

        [HttpPost]
        [Route("{userId}/{thingId}")]
        public async Task Post(Guid userId, Guid thingId, Lend lend)
        {
            await _lends.Create(userId, thingId, lend);
        }

        [HttpPut]
        [Route("{userId}/{thingId}")]
        public async Task Put(Guid userId, Guid thingId, Lend lend)
        {
            await _lends.Update(userId, thingId, lend);
        }

        [HttpDelete]
        [Route("{userId}/{thingId}")]
        public async Task Delete(Guid userId, Guid thingId, DateTime returnDate)
        {
            await _lends.Delete(userId, thingId, returnDate);
        }
    }
}
