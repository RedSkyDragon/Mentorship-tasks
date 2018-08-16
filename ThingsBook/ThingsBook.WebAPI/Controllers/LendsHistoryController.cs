using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ThingsBook.BusinessLogic;
using ThingsBook.BusinessLogic.Models;

namespace ThingsBook.WebAPI.Controllers
{
    public class LendsHistoryController : ApiController
    {
        private ILendsBL _lends;

        public LendsHistoryController(ILendsBL lends)
        {
            _lends = lends;
        }

        [HttpGet]
        public async Task<IEnumerable<HistLend>> Get(Guid userId)
        {
            return await _lends.GetHistoricalLends(userId);
        }

        [HttpGet]
        public async Task<HistLend> Get(Guid userId, Guid lendId)
        {
            return await _lends.GetHistoricalLend(userId, lendId);
        }

        [HttpDelete]
        public async Task Delete(Guid userId, Guid lendId)
        {
            await _lends.DeleteHistoricalLend(userId, lendId);
        }
    }
}
