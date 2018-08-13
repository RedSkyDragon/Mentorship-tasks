using IncomeAndExpenses.DataAccessInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncomeAndExpenses.BusinessLogic
{
    /// <summary>
    /// Interface to provide business logic for users
    /// </summary>
    public interface IUsersBL
    {

        /// <summary>
        /// Creates user
        /// </summary>
        /// <param name="user">the user</param>
        void CreateUser(UserDM user);

        /// <summary>
        /// Updates user
        /// </summary>
        /// <param name="user">the user</param>
        void UpdateUser(UserDM user);

        /// <summary>
        /// Gets User
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>User</returns>
        UserDM GetUser(string id);

        /// <summary>
        /// Creates the or update user.
        /// </summary>
        /// <param name="user">The user.</param>
        void CreateOrUpdateUser(UserDM user);
    }
}
