using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using ThingsBook.Data.Interface;

namespace ThingsBook.Data.Mongo
{ 
    public class Friends : IFriends
    {
        private ThingsBookContext _db;

        public Friends(ThingsBookContext db)
        {
            _db = db;
        }

        public void CreateFriend(Guid userId, Friend friend)
        {
            var update = Builders<User>.Update.Push(u => u.Friends, friend);
            _db.Users.FindOneAndUpdate(u => u.Id == userId, update);
        }

        public void DeleteFriend(Guid userId, Guid id)
        {
            var filter = Builders<Friend>.Filter.Where(c => c.Id == id);
            var update = Builders<User>.Update.PullFilter(u => u.Friends, filter);
            _db.Users.FindOneAndUpdate(u => u.Id == userId, update);
        }

        public Friend GetFriend(Guid userId, Guid id)
        {
            return _db.Users.Find(u => u.Id == userId).FirstOrDefault().Friends.Where(c => c.Id == id).FirstOrDefault();
            
        }

        public IEnumerable<Friend> GetFriends(Guid userId)
        {
            return _db.Users.Find(u => u.Id == userId).FirstOrDefault().Friends;
        }

        public void UpdateFriend(Guid userId, Friend friend)
        {
            var update = Builders<User>.Update.Set(u => u.Friends.ElementAt(-1).Name, friend.Name)
               .Set(u => u.Friends.ElementAt(-1).Contacts, friend.Contacts);
            _db.Users.FindOneAndUpdate(u => u.Id == userId && u.Friends.Any(f => f.Id == friend.Id), update);
        }
    }
}
