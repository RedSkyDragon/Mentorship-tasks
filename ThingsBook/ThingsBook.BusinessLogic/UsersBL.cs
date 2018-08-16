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
            await Data.Users.CreateUser(user);
            await Data.Categories.CreateCategory(user.Id, new Category { Name = "Other", About = "Things which are difficult to classify", UserId = user.Id });
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

        public Task Update(User user)
        {
            return Data.Users.UpdateUser(user);
        }
    }
}
