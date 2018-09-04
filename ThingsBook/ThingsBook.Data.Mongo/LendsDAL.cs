using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using ThingsBook.Data.Interface;

namespace ThingsBook.Data.Mongo
{
    /// <summary>
    /// Mongo implementation of DAL interface for lends.
    /// </summary>
    /// <seealso cref="ThingsBook.Data.Interface.ILendsDAL" />
    public class LendsDAL : ILendsDAL
    {
        private ThingsBookContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="LendsDAL"/> class.
        /// </summary>
        /// <param name="db">The database context.</param>
        public LendsDAL(ThingsBookContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Creates the lend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thingId">The thing identifier.</param>
        /// <param name="lend">The lend.</param>
        public Task CreateLend(Guid userId, Guid thingId, Lend lend)
        {
            if (lend == null)
            {
                throw new ArgumentNullException(nameof(lend));
            }
            var update = Builders<Thing>.Update.Set(t => t.Lend, lend);
            return _db.Things.UpdateOneAsync(t => t.UserId == userId && t.Id == thingId, update);
        }

        /// <summary>
        /// Deletes the friend lends.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="friendId">The friend identifier.</param>
        public Task DeleteFriendLends(Guid userId, Guid friendId)
        {
            var update = Builders<Thing>.Update.Set(t => t.Lend, null);
            return _db.Things.UpdateManyAsync(t => t.UserId == userId && t.Lend.FriendId == friendId, update);
        }

        /// <summary>
        /// Deletes the lend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thingId">The thing identifier.</param>
        public Task DeleteLend(Guid userId, Guid thingId)
        {
            var update = Builders<Thing>.Update.Set(t => t.Lend, null);
            return _db.Things.UpdateOneAsync(t => t.UserId == userId && t.Id == thingId, update);
        }

        /// <summary>
        /// Updates the lend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thingId">The thing identifier.</param>
        /// <param name="lend">The lend.</param>
        public Task UpdateLend(Guid userId, Guid thingId, Lend lend)
        {
            if (lend == null)
            {
                throw new ArgumentNullException(nameof(lend));
            }
            var update = Builders<Thing>.Update
                .Set(t => t.Lend.FriendId, lend.FriendId)
                .Set(t => t.Lend.LendDate, lend.LendDate)
                .Set(t => t.Lend.Comment, lend.Comment);
            return _db.Things.UpdateOneAsync(t => t.UserId == userId && t.Id == thingId, update);
        }
    }
}
