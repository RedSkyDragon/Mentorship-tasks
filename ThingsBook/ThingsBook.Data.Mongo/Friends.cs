using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task CreateFriend(Friend friend)
        {
            await _db.Friends.InsertOneAsync(friend);
        }

        public async Task DeleteFriend(Guid id)
        {
            await _db.Friends.DeleteOneAsync(f => f.Id == id);
        }

        public async Task DeleteFriends(Guid userId)
        {
            await _db.Friends.DeleteManyAsync(f => f.UserId == userId);
        }

        public async Task<Friend> GetFriend(Guid id)
        {
            var result = await _db.Friends.FindAsync(f => f.Id == id);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Friend>> GetFriends(Guid userId)
        {
            var result = await _db.Friends.FindAsync(f => f.UserId == userId);
            return result.ToEnumerable();
        }

        public async Task UpdateFriend(Friend friend)
        {
            var update = Builders<Friend>.Update
                .Set(f => f.Name, friend.Name)
                .Set(f => f.Contacts, friend.Contacts)
                .Set(f => f.UserId, friend.UserId);
            await _db.Friends.UpdateOneAsync(f => f.Id == friend.Id, update);
        }
    }
}
