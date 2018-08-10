using System;
using System.Collections.Generic;
using ThingsBook.DataAccessInterface;

namespace BusinessLogic
{
    public interface IBusinessLogic
    {
        IEnumerable<User> GetUsers();

        User GetUser(Guid id);

        void UpdateUser(User user);

        void DeleteUser(Guid id);

        void CreateUser(User user);

        IEnumerable<HistoricalLend> GetHistLends(Guid userId);

        HistoricalLend GetHistLend(Guid id);

        void UpdateHistLend(HistoricalLend lend);

        void DeleteHistLend(Guid id);

        void CreateHistLend(HistoricalLend lend);

        IEnumerable<Category> GetCategories(Guid userId);

        Category GetCategory(Guid userId, Guid id);

        void UpdateCategory(Guid userId, Category category);

        void DeleteCategory(Guid userId, Guid id);

        void CreateCategory(Guid userId, Category category);

        IEnumerable<Friend> GetFriends(Guid userId);

        Friend GetFriend(Guid userId, Guid id);

        void UpdateFriend(Guid userId, Friend friend);

        void DeleteFriend(Guid userId, Guid id);

        void CreateFriend(Guid userId, Friend friend);

        IEnumerable<Thing> GetThings(Guid userId);

        IEnumerable<Thing> GetThings(Guid userId, Guid categoryId);

        Thing GetThing(Guid userId, Guid categoryId, Guid id);

        void UpdateThing(Guid userId, Guid categoryId, Thing thing);

        void DeleteThing(Guid userId, Guid categoryId, Guid id);

        void CreateThing(Guid userId, Guid categoryId, Thing thing);

        IEnumerable<Lend> GetLends(Guid userId);

        IEnumerable<Lend> GetLends(Guid userId, Guid categoryId, Guid thingId);

        Lend GetLend(Guid userId, Guid categoryId, Guid thingId);

        void UpdateLend(Guid userId, Guid categoryId, Guid thingId, Lend lend);

        void DeleteLend(Guid userId, Guid categoryId, Guid thingId);

        void CreateLend(Guid userId, Guid categoryId, Guid thingId, Lend lend);
    }
}
