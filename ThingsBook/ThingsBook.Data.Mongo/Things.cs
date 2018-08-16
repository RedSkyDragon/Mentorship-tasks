﻿using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThingsBook.Data.Interface;

namespace ThingsBook.Data.Mongo
{
    public class Things : IThings
    {
        private ThingsBookContext _db;

        public Things(ThingsBookContext db)
        {
            _db = db;
        }

        public async Task CreateThing(Guid userId, Thing thing)
        {
            if (userId == thing.UserId)
            {
                await _db.Things.InsertOneAsync(thing);
            }
        }

        public Task DeleteThing(Guid userId, Guid id)
        {
            return _db.Things.DeleteOneAsync(t => t.UserId == userId && t.Id == id);
        }

        public Task DeleteThings(Guid userId)
        {
            return _db.Things.DeleteManyAsync(t => t.UserId == userId);
        }

        public Task DeleteThingsForCategory(Guid userId, Guid categoryId)
        {
            return _db.Things.DeleteManyAsync(t => t.UserId == userId && t.CategoryId == categoryId);
        }

        public async Task<IEnumerable<Thing>> GetThingsForFriend(Guid userId, Guid friedId)
        {
            var result = await _db.Things.FindAsync(t => t.UserId == userId && t.Lend.FriendId == friedId);
            return result.ToEnumerable();
        }

        public async Task<Thing> GetThing(Guid userId, Guid id)
        {
            var result = await _db.Things.FindAsync(t => t.UserId == userId && t.Id == id);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Thing>> GetThings(Guid userId)
        {
            var result = await _db.Things.FindAsync(t => t.UserId == userId);
            return result.ToEnumerable();
        }

        public async Task<IEnumerable<Thing>> GetThingsForCategory(Guid userId, Guid categoryId)
        {
            var result = await _db.Things.FindAsync(t => t.UserId == userId && t.CategoryId == categoryId);
            return result.ToEnumerable();
        }

        public Task UpdateThing(Guid userId, Thing thing)
        {
            var update = Builders<Thing>.Update
                .Set(t => t.Name, thing.Name)
                .Set(t => t.About, thing.About)
                .Set(t => t.CategoryId, thing.CategoryId);
            return _db.Things.UpdateOneAsync(t => t.UserId == userId && t.Id == thing.Id, update);
        }

        public Task UpdateThingsCategory(Guid userId, Guid categoryId, Guid replacementId)
        {
            var update = Builders<Thing>.Update
                .Set(t => t.CategoryId, replacementId);
            return _db.Things.UpdateManyAsync(t => t.UserId == userId && t.CategoryId == categoryId, update);
        }
    }
}
