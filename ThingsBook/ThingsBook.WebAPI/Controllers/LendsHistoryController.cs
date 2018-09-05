using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ThingsBook.BusinessLogic;
using ThingsBook.BusinessLogic.Models;

namespace ThingsBook.WebAPI.Controllers
{

    /// <summary>
    /// Controller for history management.
    /// </summary>
    /// <seealso cref="ThingsBook.WebAPI.Controllers.BaseController" />
    [RoutePrefix("history")]
    [Authorize]
    public class LendsHistoryController : BaseController
    {
        private readonly ILendsBL _lends;

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
        /// <returns>List of history records</returns>
        [HttpGet]
        [Route("")]
        public Task<IEnumerable<HistLend>> Get()
        {
            return _lends.GetHistoricalLends(ApiUser.Id);
        }

        /// <summary>
        /// Gets the specified by identifier history record.
        /// </summary>
        /// <param name="lendId">The record identifier.</param>
        /// <returns>History record</returns>
        [HttpGet]
        [Route("{lendId:guid}")]
        public Task<HistLend> Get(Guid lendId)
        {
            return _lends.GetHistoricalLend(ApiUser.Id, lendId);
        }

        /// <summary>
        /// Deletes the specified by identifier history record.
        /// </summary>
        /// <param name="lendId">The record identifier.</param>
        /// <returns>204(no content)</returns>
        [HttpDelete]
        [Route("{lendId:guid}")]
        public Task Delete(Guid lendId)
        {
            return _lends.DeleteHistoricalLend(ApiUser.Id, lendId);
        }
    }
}
