using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic
{
    public class UsersBL : BaseBL, IUsersBL
    {
        public UsersBL(CommonDAL data) : base(data) { }

        public async Task Create(User user)
        {
            var createUser = Data.Users.CreateUser(user);
            var createCat = Data.Categories.CreateCategory(new Category { Name = "Other", About = "Things which are difficult to classify", UserId = user.Id });
            await Task.WhenAll(createUser, createCat);
        }

        public async Task CreateOrUpdate(User user)
        {
            var current = await Get(user.Id);
            if (current == null)
            {
                await Create(user);
            }
            else
            {
                await Update(user);
            }
        }

        public async Task Delete(Guid id)
        {
            var delUser = Data.Users.DeleteUser(id);
            var delCats = Data.Categories.DeleteCategories(id);
            var delFriends = Data.Friends.DeleteFriends(id);
            var delThings = Data.Things.DeleteThings(id);
            await Task.WhenAll(delUser, delCats, delFriends, delThings);
        }

        public async Task<User> Get(Guid id)
        {
            return await Data.Users.GetUser(id);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await Data.Users.GetUsers();
        }

        public async Task Update(User user)
        {
            await Data.Users.UpdateUser(user);
        }
    }
}
