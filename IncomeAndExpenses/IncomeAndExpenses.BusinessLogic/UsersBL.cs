using IncomeAndExpenses.DataAccessInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncomeAndExpenses.BusinessLogic
{
    /// <summary>
    /// Implements IUsersBL interface
    /// </summary>
    public class UsersBL: IUsersBL
    {
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="unitOfWork">The unitOfWork.</param>
        public UsersBL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets User
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>
        /// User
        /// </returns>
        public UserDM GetUser(string id)
        {
            return _unitOfWork.Repository<string, UserDM>().Get(id);
        }

        /// <summary>
        /// Creates user
        /// </summary>
        /// <param name="user">the user</param>
        public void CreateUser(UserDM user)
        {
            _unitOfWork.Repository<string, UserDM>().Create(user);
            _unitOfWork.Repository<IncomeTypeDM>().Create(new IncomeTypeDM { UserId = user.Id, Name = "Other", Description = "Income that are difficult to classify as specific type." });
            _unitOfWork.Repository<ExpenseTypeDM>().Create(new ExpenseTypeDM { UserId = user.Id, Name = "Other", Description = "Expense that are difficult to classify as specific type." });
            _unitOfWork.Save();
        }

        /// <summary>
        /// Updates user
        /// </summary>
        /// <param name="user">the user</param>
        public void UpdateUser(UserDM user)
        {
            _unitOfWork.Repository<string, UserDM>().Update(user);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Creates the or update user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void CreateOrUpdateUser(UserDM user)
        {
            if (GetUser(user.Id) == null)
            {
                CreateUser(user);
            }
            else
            {
                UpdateUser(user);
            }
        }
    }
}
