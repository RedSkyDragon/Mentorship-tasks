using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThingsBook.Data.Interface;

namespace ThingsBook.Data.Mongo
{
    /// <summary>
    /// Mongo implementation of DAL interface for things.
    /// </summary>
    /// <seealso cref="ThingsBook.Data.Interface.IThings" />
    public class Things : IThings
    {
        private ThingsBookContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="Things"/> class.
        /// </summary>
        /// <param name="db">The database context.</param>
        public Things(ThingsBookContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Creates the thing.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thing">The thing.</param>
        public async Task CreateThing(Guid userId, Thing thing)
        {
            if (thing == null)
            {
                throw new ArgumentNullException("thing");
            }
            if (userId != thing.UserId)
            {
                throw new ArgumentException("Param userId must be equal to thing.UserId.");
            }
            await _db.Things.InsertOneAsync(thing);
        }

        /// <summary>
        /// Deletes the thing.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The thing identifier.</param>
        public Task DeleteThing(Guid userId, Guid id)
        {
            return _db.Things.DeleteOneAsync(t => t.UserId == userId && t.Id == id);
        }

        /// <summary>
        /// Deletes all things.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public Task DeleteThings(Guid userId)
        {
            return _db.Things.DeleteManyAsync(t => t.UserId == userId);
        }

        /// <summary>
        /// Deletes all things with specified category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="categoryId">The category identifier.</param>
        public async Task DeleteThingsForCategory(Guid userId, Guid categoryId)
        {
            var options = new FindOptions<Thing> { Projection = new ProjectionDefinitionBuilder<Thing>().Include(p => p.Id) };
            var things = (await _db.Things.FindAsync(t => t.UserId == userId && t.CategoryId == categoryId, options)).ToList().Select(t => t.Id);
            await _db.History.DeleteManyAsync(h => h.UserId == userId && things.Contains(h.ThingId));
            await _db.Things.DeleteManyAsync(t => t.UserId == userId && t.CategoryId == categoryId);          
        }

        /// <summary>
        /// Gets the things lended by the specified friend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="friedId">The fried identifier.</param>
        /// <returns>
        /// List of things.
        /// </returns>
        public async Task<IEnumerable<Thing>> GetThingsForFriend(Guid userId, Guid friedId)
        {
            var result = await _db.Things.FindAsync(t => t.UserId == userId && t.Lend.FriendId == friedId);
            return result.ToEnumerable();
        }

        /// <summary>
        /// Gets the thing.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The thing identifier.</param>
        /// <returns>
        /// The thing.
        /// </returns>
        public async Task<Thing> GetThing(Guid userId, Guid id)
        {
            var result = await _db.Things.FindAsync(t => t.UserId == userId && t.Id == id);
            return result.FirstOrDefault();
        }

        /// <summary>
        /// Gets all things.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// List of things
        /// </returns>
        public async Task<IEnumerable<Thing>> GetThings(Guid userId)
        {
            var result = await _db.Things.FindAsync(t => t.UserId == userId);
            return result.ToEnumerable();
        }

        /// <summary>
        /// Gets the things with specified category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>
        /// List of things.
        /// </returns>
        public async Task<IEnumerable<Thing>> GetThingsForCategory(Guid userId, Guid categoryId)
        {
            var result = await _db.Things.FindAsync(t => t.UserId == userId && t.CategoryId == categoryId);
            return result.ToEnumerable();
        }

        /// <summary>
        /// Updates the thing.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thing">The thing.</param>
        public Task UpdateThing(Guid userId, Thing thing)
        {
            if (thing == null)
            {
                throw new ArgumentNullException("thing");
            }
            if (userId != thing.UserId)
            {
                throw new ArgumentException("Param userId must be equal to thing.UserId.");
            }
            var update = Builders<Thing>.Update
                .Set(t => t.Name, thing.Name)
                .Set(t => t.About, thing.About)
                .Set(t => t.CategoryId, thing.CategoryId);
            return _db.Things.UpdateOneAsync(t => t.UserId == userId && t.Id == thing.Id, update);
        }

        /// <summary>
        /// Updates the things category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="replacementId">The replacement identifier.</param>
        public Task UpdateThingsCategory(Guid userId, Guid categoryId, Guid replacementId)
        {
            var update = Builders<Thing>.Update
                .Set(t => t.CategoryId, replacementId);
            return _db.Things.UpdateManyAsync(t => t.UserId == userId && t.CategoryId == categoryId, update);
        }
    }
}
