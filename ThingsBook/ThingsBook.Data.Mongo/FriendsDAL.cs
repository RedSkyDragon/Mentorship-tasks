using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThingsBook.Data.Interface;

namespace ThingsBook.Data.Mongo
{
    /// <summary>
    /// Mongo implementation of DAL interface for friends.
    /// </summary>
    /// <seealso cref="ThingsBook.Data.Interface.IFriendsDAL" />
    public class FriendsDAL : IFriendsDAL
    {
        private readonly ThingsBookContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="FriendsDAL"/> class.
        /// </summary>
        /// <param name="db">The database context.</param>
        public FriendsDAL(ThingsBookContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Creates the friend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="friend">The friend.</param>
        public Task CreateFriend(Guid userId, Friend friend)
        {
            if (friend == null)
            {
                throw new ArgumentNullException(nameof(friend));
            }
            if (userId != friend.UserId)
            {
                throw new ArgumentException("Param userId must be equal to friend.UserId.", nameof(userId));
            }
            return _db.Friends.InsertOneAsync(friend);
        }

        /// <summary>
        /// Deletes the friend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The identifier.</param>
        public Task DeleteFriend(Guid userId, Guid id)
        {
            return _db.Friends.DeleteOneAsync(f => f.UserId == userId && f.Id == id);
        }

        /// <summary>
        /// Deletes all friends for user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public Task DeleteFriends(Guid userId)
        {
            return _db.Friends.DeleteManyAsync(f => f.UserId == userId);
        }

        /// <summary>
        /// Gets the friend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The friend identifier.</param>
        /// <returns>
        /// The friend.
        /// </returns>
        public async Task<Friend> GetFriend(Guid userId, Guid id)
        {
            var result = await _db.Friends.FindAsync(f => f.UserId == userId && f.Id == id);
            return await result.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets the friends.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// List of friends.
        /// </returns>
        public async Task<IEnumerable<Friend>> GetFriends(Guid userId)
        {
            var result = await _db.Friends.FindAsync(f => f.UserId == userId);
            return await result.ToListAsync();
        }

        /// <summary>
        /// Updates the friend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="friend">The friend.</param>
        public Task UpdateFriend(Guid userId, Friend friend)
        {
            if (friend == null)
            {
                throw new ArgumentNullException(nameof(friend));
            }
            if (userId != friend.UserId)
            {
                throw new ArgumentException("Param userId must be equal to friend.UserId.", nameof(userId));
            }
            var update = Builders<Friend>.Update
                .Set(f => f.Name, friend.Name)
                .Set(f => f.Contacts, friend.Contacts);
            return _db.Friends.UpdateOneAsync(f => f.UserId == userId && f.Id == friend.Id, update);
        }
    }
}
