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
        /// <param name="storage">The storage.</param>
        public UsersBL(Storage storage) : base(storage) { }

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
            await Storage.Users.CreateUser(ModelsConverter.ToDataModel(user));
            await Storage.Categories.CreateCategory
                (user.Id, new Category
                {
                    Name = "Other",
                    About = "Things which are difficult to classify",
                    UserId = user.Id
                });
            return ModelsConverter.ToBLModel(await Storage.Users.GetUser(user.Id));
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
            return await Update(user);
        }

        /// <summary>
        /// Deletes the specified by identifier user.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns></returns>
        public async Task Delete(Guid id)
        {
            await Storage.History.DeleteUserHistory(id);
            await Storage.Things.DeleteThings(id);
            await Storage.Friends.DeleteFriends(id);
            await Storage.Categories.DeleteCategories(id);
            await Storage.Users.DeleteUser(id);
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
            return ModelsConverter.ToBLModel(await Storage.Users.GetUser(id));
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>
        /// List of users.
        /// </returns>
        public async Task<IEnumerable<Models.User>> GetAll()
        {
            var result = await Storage.Users.GetUsers();
            return result.Select(ModelsConverter.ToBLModel);
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
            await Storage.Users.UpdateUser(ModelsConverter.ToDataModel(user));
            return ModelsConverter.ToBLModel(await Storage.Users.GetUser(user.Id));
        }
    }
}
