using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ThingsBook.BusinessLogic;
using ThingsBook.BusinessLogic.Models;

namespace ThingsBook.WebAPI.Controllers
{
    [RoutePrefix("history")]
    public class LendsHistoryController : ApiController
    {
        private ILendsBL _lends;

        public LendsHistoryController(ILendsBL lends)
        {
            _lends = lends;
        }

        [HttpGet]
        [Route("")]
        public Task<IEnumerable<HistLend>> Get([FromUri]Guid userId)
        {
            return _lends.GetHistoricalLends(userId);
        }

        [HttpGet]
        [Route("{lendId:guid}")]
        public Task<HistLend> Get([FromUri]Guid userId, [FromUri]Guid lendId)
        {
            return _lends.GetHistoricalLend(userId, lendId);
        }

        [HttpDelete]
        [Route("{lendId:guid}")]
        public Task Delete([FromUri]Guid userId, [FromUri]Guid lendId)
        {
            return _lends.DeleteHistoricalLend(userId, lendId);
        }
    }
}
