using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic
{
    public class UsersBL : BaseBL, IUsersBL
    {
        public UsersBL(CommonDAL data)
        {
            _data = data;
        }

        public async Task Create(User user)
        {
            var createUser = _data.Users.CreateUser(user);
            var createCat = _data.Categories.CreateCategory(new Category { Name = "Other", About = "Things which are difficult to classify", UserId = user.Id });
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
            var delUser = _data.Users.DeleteUser(id);
            var delCats = _data.Categories.DeleteCategories(id);
            var delFriends = _data.Friends.DeleteFriends(id);
            var delThings = _data.Things.DeleteThings(id);
            await Task.WhenAll(delUser, delCats, delFriends, delThings);
        }

        public async Task<User> Get(Guid id)
        {
            return await _data.Users.GetUser(id);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _data.Users.GetUsers();
        }

        public async Task Update(User user)
        {
            await _data.Users.UpdateUser(user);
        }
    }
}
