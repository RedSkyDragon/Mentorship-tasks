using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ThingsBook.BusinessLogic;
using ThingsBook.BusinessLogic.Models;

namespace ThingsBook.WebAPI.Controllers
{
    /// <summary>
    /// Controller for history management
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [RoutePrefix("history")]
    public class LendsHistoryController : ApiController
    {
        private ILendsBL _lends;

        /// <summary>
        /// Initializes a new instance of the <see cref="LendsHistoryController"/> class.
        /// </summary>
        /// <param name="lends">The lends business logic.</param>
        public LendsHistoryController(ILendsBL lends)
        {
            _lends = lends;
        }

        /// <summary>
        /// Gets full history fot the specified by identifier user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of history records</returns>
        [HttpGet]
        [Route("")]
        public Task<IEnumerable<HistLend>> Get([FromUri]Guid userId)
        {
            return _lends.GetHistoricalLends(userId);
        }

        /// <summary>
        /// Gets the specified by identifier history record.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="lendId">The record identifier.</param>
        /// <returns>History record</returns>
        [HttpGet]
        [Route("{lendId:guid}")]
        public Task<HistLend> Get([FromUri]Guid userId, [FromUri]Guid lendId)
        {
            return _lends.GetHistoricalLend(userId, lendId);
        }

        /// <summary>
        /// Deletes the specified by identifier history record.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="lendId">The record identifier.</param>
        /// <returns>204(no content)</returns>
        [HttpDelete]
        [Route("{lendId:guid}")]
        public Task Delete([FromUri]Guid userId, [FromUri]Guid lendId)
        {
            return _lends.DeleteHistoricalLend(userId, lendId);
        }
    }
}
