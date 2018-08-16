using System;
using System.Threading.Tasks;
using System.Web.Http;
using ThingsBook.BusinessLogic;
using ThingsBook.Data.Interface;

namespace ThingsBook.WebAPI.Controllers
{
    public class LendsController : ApiController
    {
        private ILendsBL _lends;

        public LendsController(ILendsBL lends)
        {
            _lends = lends;
        }

        [HttpPost]
        public async Task Post(Guid userId, Guid thingId, Lend lend)
        {
            await _lends.Create(userId, thingId, lend);
        }

        [HttpPut]
        public async Task Put(Guid userId, Guid thingId, Lend lend)
        {
            await _lends.Update(userId, thingId, lend);
        }

        [HttpDelete]
        public async Task Delete(Guid userId, Guid thingId, DateTime returnDate)
        {
            await _lends.Delete(userId, thingId, returnDate);
        }
    }
}
