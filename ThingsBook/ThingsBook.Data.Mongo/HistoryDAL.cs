using System;
using System.Collections.Generic;
using MongoDB.Driver;
using ThingsBook.Data.Interface;
using System.Threading.Tasks;

namespace ThingsBook.Data.Mongo
{
    /// <summary>
    /// Mongo implementation of DAL interface for historical lends.
    /// </summary>
    /// <seealso cref="ThingsBook.Data.Interface.IHistoryDAL" />
    public class HistoryDAL : IHistoryDAL
    {
        private ThingsBookContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoryDAL"/> class.
        /// </summary>
        /// <param name="db">The database context.</param>
        public HistoryDAL(ThingsBookContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Creates the historical lend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="lend">The lend.</param>
        public async Task CreateHistLend(Guid userId, HistoricalLend lend)
        {
            if (lend == null)
            {
                throw new ArgumentNullException(nameof(lend));
            }
            if (userId != lend.UserId)
            {
                throw new ArgumentException("Param userId must be equal to lend.UserId.", nameof(userId));
            }
            await _db.History.InsertOneAsync(lend);
        }

        /// <summary>
        /// Deletes the friend history.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="friendId">The friend identifier.</param>
        public Task DeleteFriendHistory(Guid userId, Guid friendId)
        {
            return _db.History.DeleteManyAsync(h => h.UserId == userId && h.FriendId == friendId);
        }

        /// <summary>
        /// Deletes the historical lend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The lend identifier.</param>
        public Task DeleteHistLend(Guid userId, Guid id)
        {
            return _db.History.DeleteOneAsync(h => h.UserId == userId && h.Id == id);
        }

        /// <summary>
        /// Deletes the thing history.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thingId">The thing identifier.</param>
        public Task DeleteThingHistory(Guid userId, Guid thingId)
        {
            return _db.History.DeleteManyAsync(h => h.UserId == userId && h.ThingId == thingId);
        }

        /// <summary>
        /// Deletes the user history.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public Task DeleteUserHistory(Guid userId)
        {
            return _db.History.DeleteManyAsync(h => h.UserId == userId); 
        }

        /// <summary>
        /// Gets the friend historical lends.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="friendId">The friend identifier.</param>
        /// <returns>
        /// Dictionary with lend as key and lended thing as value.
        /// </returns>
        public async Task<IDictionary<HistoricalLend, Thing>> GetFriendHistLends(Guid userId, Guid friendId)
        {
            var result = new Dictionary<HistoricalLend, Thing>();
            using (var cursor = await _db.History.FindAsync(h => h.UserId == userId && h.FriendId == friendId))
            {
                while (await cursor.MoveNextAsync())
                {
                    var lends = cursor.Current;
                    foreach (var lend in lends)
                    {
                        var thing = await _db.Things.FindAsync(t => t.Id == lend.ThingId);
                        result.Add(lend, thing.FirstOrDefault());
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Gets the historical lend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The historical lend identifier.</param>
        /// <returns>
        /// Historical lend
        /// </returns>
        public async Task<HistoricalLend> GetHistLend(Guid userId, Guid id)
        {
            var result = await _db.History.FindAsync(h => h.UserId == userId && h.Id == id);
            return result.FirstOrDefault();
        }

        /// <summary>
        /// Gets all historical lends of user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// List of historical lends.
        /// </returns>
        public async Task<IEnumerable<HistoricalLend>> GetHistLends(Guid userId)
        {
            var result = await _db.History.FindAsync(h => h.UserId == userId);
            return await result.ToListAsync();
        }

        /// <summary>
        /// Gets the thing historical lends.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thingId">The thing identifier.</param>
        /// <returns>
        /// Dictionary with lend as key and friend as value.
        /// </returns>
        public async Task<IDictionary<HistoricalLend, Friend>> GetThingHistLends(Guid userId, Guid thingId)
        {
            var result = new Dictionary<HistoricalLend, Friend>();
            using (var cursor = await _db.History.FindAsync(h => h.UserId == userId && h.ThingId == thingId))
            {
                while (await cursor.MoveNextAsync())
                {
                    var lends = cursor.Current;
                    foreach (var lend in lends)
                    {
                        var friend = await _db.Friends.FindAsync(t => t.Id == lend.FriendId);
                        result.Add(lend, friend.FirstOrDefault());
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Updates the historical lend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="lend">The lend.</param>
        /// <returns></returns>
        public Task UpdateHistLend(Guid userId, HistoricalLend lend)
        {
            if (lend == null)
            {
                throw new ArgumentNullException(nameof(lend));
            }
            if (userId != lend.UserId)
            {
                throw new ArgumentException("Param userId must be equal to lend.UserId.", nameof(userId));
            }
            var update = Builders<HistoricalLend>.Update
                .Set(h => h.FriendId, lend.FriendId)
                .Set(h => h.ThingId, lend.ThingId)
                .Set(h => h.LendDate, lend.LendDate)
                .Set(h => h.ReturnDate, lend.ReturnDate)
                .Set(h => h.Comment, lend.Comment);
            return _db.History.UpdateOneAsync(h => h.UserId == userId && h.Id == lend.Id, update);
        }
    }
}
