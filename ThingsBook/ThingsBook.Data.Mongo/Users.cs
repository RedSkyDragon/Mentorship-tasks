using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThingsBook.Data.Interface;

namespace ThingsBook.Data.Mongo
{
    /// <summary>
    /// Mongo implementation of DAL interface for users.
    /// </summary>
    /// <seealso cref="ThingsBook.Data.Interface.IUsers" />
    public class Users : IUsers
    {
        private ThingsBookContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="Users"/> class.
        /// </summary>
        /// <param name="db">The database context.</param>
        public Users(ThingsBookContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        public Task CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return _db.Users.InsertOneAsync(user);
        }

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        public Task DeleteUser(Guid id)
        {
            return _db.Users.DeleteOneAsync(u => u.Id == id);
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns>
        /// The user.
        /// </returns>
        public async Task<User> GetUser(Guid id)
        {
            var result = await _db.Users.FindAsync(u => u.Id == id);
            return result.FirstOrDefault();
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>
        /// List of users
        /// </returns>
        public async Task<IEnumerable<User>> GetUsers()
        {
            var filter = new FilterDefinitionBuilder<User>().Empty;
            var result = await _db.Users.FindAsync(filter);
            return result.ToEnumerable();
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        public Task UpdateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            var update = Builders<User>.Update.Set(u=> u.Name, user.Name);
            return _db.Users.UpdateOneAsync(u => u.Id == user.Id, update);
        }
    }
}
