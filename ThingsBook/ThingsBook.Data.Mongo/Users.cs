using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThingsBook.Data.Interface;

namespace ThingsBook.Data.Mongo
{
    public class Users : IUsers
    {
        private ThingsBookContext _db;

        public Users(ThingsBookContext db)
        {
            _db = db;
        }

        public Task CreateUser(User user)
        {
            return _db.Users.InsertOneAsync(user);
        }

        public Task DeleteUser(Guid id)
        {
            return _db.Users.DeleteOneAsync(u => u.Id == id);
        }

        public async Task<User> GetUser(Guid id)
        {
            var result = await _db.Users.FindAsync(u => u.Id == id);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var filter = new FilterDefinitionBuilder<User>().Empty;
            var result = await _db.Users.FindAsync(filter);
            return result.ToEnumerable();
        }

        public Task UpdateUser(User user)
        {
            var update = Builders<User>.Update.Set(u=> u.Name, user.Name);
            return _db.Users.UpdateOneAsync(u => u.Id == user.Id, update);
        }
    }
}
