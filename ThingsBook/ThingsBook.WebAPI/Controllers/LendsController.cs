using System;
using System.Threading.Tasks;
using System.Web.Http;
using ThingsBook.BusinessLogic;
using ThingsBook.BusinessLogic.Models;

namespace ThingsBook.WebAPI.Controllers
{
    /// <summary>
    /// Controller for active lends management
    /// </summary>
    /// <seealso cref="ThingsBook.WebAPI.Controllers.BaseController" />
    [RoutePrefix("lend")]
    [Authorize]
    public class LendsController : BaseController
    {
        private ILendsBL _lends;

        /// <summary>
        /// Initializes a new instance of the <see cref="LendsController"/> class.
        /// </summary>
        /// <param name="lends">The lends business logic.</param>
        /// <param name="things">The things business logic.</param>
        public LendsController(ILendsBL lends)
        {
            _lends = lends;
        }

        /// <summary>
        /// Creates new lend.
        /// </summary>
        /// <param name="thingId">The thing identifier.</param>
        /// <param name="lend">The lend information</param>
        /// <returns>Lended thing</returns>
        [HttpPost]
        [Route("{thingId:guid}")]
        public Task<ThingWithLend> Post([FromUri]Guid thingId, [FromBody]Lend lend)
        {
            return _lends.Create(ApiUser.Id, thingId, lend);
        }

        /// <summary>
        /// Updates the lend of specified by identifier thing.
        /// </summary>
        /// <param name="thingId">The thing identifier.</param>
        /// <param name="lend">The lend information.</param>
        /// <returns>Updated thing</returns>
        [HttpPut]
        [Route("{thingId:guid}")]
        public Task<ThingWithLend> Put([FromUri]Guid thingId, [FromBody]Lend lend)
        {
            return _lends.Update(ApiUser.Id, thingId, lend);
        }

        /// <summary>
        /// Turnes the lend of specified by identifier thing into history
        /// </summary>
        /// <param name="thingId">The thing identifier.</param>
        /// <param name="returnDate">The return date.</param>
        /// <returns>204(no content)</returns>
        [HttpDelete]
        [Route("{thingId:guid}")]
        public Task Delete([FromUri]Guid thingId, [FromBody]DateTime? returnDate)
        {
            if (!returnDate.HasValue)
            {
                returnDate = DateTime.Now;
            }
            return _lends.Delete(ApiUser.Id, thingId, returnDate.Value);
        }
    }
}
