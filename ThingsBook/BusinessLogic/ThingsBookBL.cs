using DataAccessImplement;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThingsBook.DataAccessInterface;

namespace BusinessLogic
{
    public class ThingsBookBL : IBusinessLogic
    {
        private ThingsBookContext _db;

        public ThingsBookBL(ThingsBookContext context)
        {
            _db = context;
        }

        public void CreateCategory(Guid userId, Category category)
        {
            var user = _db.Users.Find(u => u.Id == userId).First();
            var cats = new List<Category>();
            cats.Add(category);
            user.Categories.Concat(cats);
            _db.Users.ReplaceOne(u => u.Id == user.Id, user);
        }

        public void CreateFriend(Guid userId, Friend friend)
        {
            var user = _db.Users.Find(u => u.Id == userId).First();
            var friends = new List<Friend>();
            friends.Add(friend);
            user.Friends.Concat(friends);
            _db.Users.ReplaceOne(u => u.Id == user.Id, user);
        }

        public void CreateHistLend(HistoricalLend lend)
        {
            _db.History.InsertOne(lend);
        }

        public void CreateLend(Guid userId, Guid categoryId, Guid thingId, Lend lend)
        {
            //_db.Users.AsQueryable().Where(u => u.Id == userId).First().Categories.Where(c => c.Id == categoryId).First().Things.Where(th => th.Id == thingId).First().Lend = lend;
            //user.Categories.Where()
            //_db.Users.ReplaceOne(u => u.Id == user.Id, user);

            
        }

        public void CreateThing(Guid userId, Guid categoryId, Thing thing)
        {
            throw new NotImplementedException();
        }

        public void CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteCategory(Guid userId, Guid id)
        {
            throw new NotImplementedException();
        }

        public void DeleteFriend(Guid userId, Guid id)
        {
            throw new NotImplementedException();
        }

        public void DeleteHistLend(Guid id)
        {
            throw new NotImplementedException();
        }

        public void DeleteLend(Guid userId, Guid categoryId, Guid thingId)
        {
            throw new NotImplementedException();
        }

        public void DeleteThing(Guid userId, Guid categoryId, Guid id)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetCategories(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Category GetCategory(Guid userId, Guid id)
        {
            throw new NotImplementedException();
        }

        public Friend GetFriend(Guid userId, Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Friend> GetFriends(Guid userId)
        {
            throw new NotImplementedException();
        }

        public HistoricalLend GetHistLend(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HistoricalLend> GetHistLends(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Lend GetLend(Guid userId, Guid categoryId, Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Lend> GetLends(Guid userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Lend> GetLends(Guid userId, Guid categoryId, Guid thingId)
        {
            throw new NotImplementedException();
        }

        public Thing GetThing(Guid userId, Guid categoryId, Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Thing> GetThings(Guid userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Thing> GetThings(Guid userId, Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public User GetUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public void UpdateCategory(Guid userId, Category category)
        {
            throw new NotImplementedException();
        }

        public void UpdateFriend(Guid userId, Friend friend)
        {
            throw new NotImplementedException();
        }

        public void UpdateHistLend(HistoricalLend lend)
        {
            throw new NotImplementedException();
        }

        public void UpdateLend(Guid userId, Guid categoryId, Guid thingId, Lend lend)
        {
            throw new NotImplementedException();
        }

        public void UpdateThing(Guid userId, Guid categoryId, Thing thing)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
