﻿using MongoDB.Driver;
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

        public async Task CreateFriend(Guid userId, Friend friend)
        {
            if (userId == friend.UserId)
            {
                await _db.Friends.InsertOneAsync(friend);
            }
        }

        public Task DeleteFriend(Guid userId, Guid id)
        {
            return _db.Friends.DeleteOneAsync(f => f.UserId == userId && f.Id == id);
        }

        public Task DeleteFriends(Guid userId)
        {
            return _db.Friends.DeleteManyAsync(f => f.UserId == userId);
        }

        public async Task<Friend> GetFriend(Guid userId, Guid id)
        {
            var result = await _db.Friends.FindAsync(f => f.UserId == userId && f.Id == id);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Friend>> GetFriends(Guid userId)
        {
            var result = await _db.Friends.FindAsync(f => f.UserId == userId);
            return result.ToEnumerable();
        }

        public Task UpdateFriend(Guid userId, Friend friend)
        {
            var update = Builders<Friend>.Update
                .Set(f => f.Name, friend.Name)
                .Set(f => f.Contacts, friend.Contacts);
            return _db.Friends.UpdateOneAsync(f => f.UserId == userId && f.Id == friend.Id, update);
        }
    }
}
