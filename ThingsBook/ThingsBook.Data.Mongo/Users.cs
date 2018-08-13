using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void CreateUser(User user)
        {
            _db.Users.InsertOne(user);
        }

        public void DeleteUser(Guid id)
        {
            _db.Users.FindOneAndDelete(u => u.Id == id);
        }

        public User GetUser(Guid id)
        {
            return _db.Users.Find(u => u.Id == id).FirstOrDefault();
        }

        public IEnumerable<User> GetUsers()
        {
            var filter = new FilterDefinitionBuilder<User>().Empty;
            return _db.Users.Find(filter).ToEnumerable();
        }

        public void UpdateUser(User user)
        {
            var update = Builders<User>.Update.Set(u=> u.Name, user.Name);
            _db.Users.UpdateOne(u => u.Id == user.Id, update);
        }
    }
}
