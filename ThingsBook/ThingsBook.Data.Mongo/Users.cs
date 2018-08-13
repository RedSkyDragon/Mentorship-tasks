using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using ThingsBook.Data.Interface;

namespace ThingsBook.Data.Mongo
{
    public class UsersBL : IUsersBL
    {
        private ThingsBookContext _db;

        public UsersBL(ThingsBookContext db)
        {
            _db = db;
        }

        public void CreateUser(User user)
        {
            _db.Users.InsertOne(user);
        }

        public void DeleteUser(Guid id)
        {
            var filter = new FilterDefinitionBuilder<User>().Eq(u => u.Id, id);
            _db.Users.FindOneAndDelete(filter);
        }

        public User GetUser(Guid id)
        {
            var filter = new FilterDefinitionBuilder<User>().Eq(u => u.Id, id);
            return _db.Users.Find(filter).ToList().FirstOrDefault();
        }

        public IEnumerable<User> GetUsers()
        {
            var filter = new FilterDefinitionBuilder<User>().Empty;
            return _db.Users.Find(filter).ToList();
        }

        public void UpdateUser(User user)
        {
            var filter = new FilterDefinitionBuilder<User>().Eq(u => u.Id, user.Id);
            _db.Users.ReplaceOne(filter, user);
        }
    }
}
