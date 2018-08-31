using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic
{
    /// <summary>
    /// Implementation of the IUsersBL interface
    /// </summary>
    /// <seealso cref="ThingsBook.BusinessLogic.BaseBL" />
    /// <seealso cref="ThingsBook.BusinessLogic.IUsersBL" />
    public class UsersBL : BaseBL, IUsersBL
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsersBL"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public UsersBL(CommonDAL data) : base(data) { }

        /// <summary>
        /// Creates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// Created user.
        /// </returns>
        public async Task<Models.User> Create(Models.User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            await Data.Users.CreateUser(ModelsConverter.ToDataModel(user));
            await Data.Categories.CreateCategory(user.Id, new Category { Name = "Other", About = "Things which are difficult to classify", UserId = user.Id });
            return ModelsConverter.ToBLModel(await Data.Users.GetUser(user.Id));
        }

        /// <summary>
        /// Creates or update the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// Created or updated user.
        /// </returns>
        public async Task<Models.User> CreateOrUpdate(Models.User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
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

        /// <summary>
        /// Deletes the specified by identifier user.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns></returns>
        public async Task Delete(Guid id)
        {
            await Data.Things.DeleteThings(id);
            await Data.Friends.DeleteFriends(id);
            await Data.Categories.DeleteCategories(id);
            await Data.History.DeleteUserHistory(id);
            await Data.Users.DeleteUser(id);
        }

        /// <summary>
        /// Gets the specified user.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns>
        /// Requested user.
        /// </returns>
        public async Task<Models.User> Get(Guid id)
        {
            return ModelsConverter.ToBLModel(await Data.Users.GetUser(id));
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>
        /// List of users.
        /// </returns>
        public async Task<IEnumerable<Models.User>> GetAll()
        {
            var result = await Data.Users.GetUsers();
            return result.Select(r => ModelsConverter.ToBLModel(r));
        }

        /// <summary>
        /// Updates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// Updated user.
        /// </returns>
        public async Task<Models.User> Update(Models.User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            await Data.Users.UpdateUser(ModelsConverter.ToDataModel(user));
            return ModelsConverter.ToBLModel(await Data.Users.GetUser(user.Id));
        }
    }
}
