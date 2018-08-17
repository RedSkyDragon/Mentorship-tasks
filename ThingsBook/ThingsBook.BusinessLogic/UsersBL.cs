using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic
{
    public class UsersBL : BaseBL, IUsersBL
    {
        public UsersBL(CommonDAL data) : base(data) { }

        public async Task<User> Create(User user)
        {
            await Data.Users.CreateUser(user);
            await Data.Categories.CreateCategory(user.Id, new Category { Name = "Other", About = "Things which are difficult to classify", UserId = user.Id });
            return await Data.Users.GetUser(user.Id);
        }

        public async Task<User> CreateOrUpdate(User user)
        {
            var current = await Get(user.Id);
            if (current == null)
            {
                return await Create(user);
            }
            else
            {
                return await Update(user);
            }
        }

        public async Task Delete(Guid id)
        {
            await Data.Things.DeleteThings(id);
            await Data.Friends.DeleteFriends(id);
            await Data.Categories.DeleteCategories(id);
            await Data.Users.DeleteUser(id);
        }

        public Task<User> Get(Guid id)
        {
            return Data.Users.GetUser(id);
        }

        public Task<IEnumerable<User>> GetAll()
        {
            return Data.Users.GetUsers();
        }

        public async Task<User> Update(User user)
        {
            await Data.Users.UpdateUser(user);
            return await Data.Users.GetUser(user.Id);
        }
    }
}
