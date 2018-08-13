using System.Collections.Generic;
using System.Linq;
using IncomeAndExpenses.DataAccessInterface;
using System.Web.Helpers;
using IncomeAndExpenses.BusinessLogic.Models;

namespace IncomeAndExpenses.BusinessLogic
{
    /// <summary>
    /// Implements IIncomesBL interface
    /// </summary>
    public class IncomesBL : IIncomesBL
    {
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="unitOfWork">The unitOfWork.</param>
        public IncomesBL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates income
        /// </summary>
        /// <param name="income">the income</param>
        public void CreateIncome(IncomeDM income)
        {
            _unitOfWork.Repository<IncomeDM>().Create(income);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Creates income type
        /// </summary>
        /// <param name="type">the income type</param>
        public void CreateIncomeType(IncomeTypeDM type)
        {
            _unitOfWork.Repository<IncomeTypeDM>().Create(type);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Deletes income type with incomes
        /// </summary>
        /// <param name="id">Id of the income type</param>
        public void DeleteIncomeType(int id)
        {
            var incomes = _unitOfWork.Repository<IncomeDM>().All().Where(ex => ex.IncomeTypeId == id);
            foreach (var income in incomes)
            {
                _unitOfWork.Repository<IncomeDM>().Delete(income.Id);
            }
            _unitOfWork.Repository<IncomeTypeDM>().Delete(id);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Deletes income type and replaces incomes' type
        /// </summary>
        /// <param name="id">Id of the income type</param>
        /// <param name="replaceId">Id of the replacement type</param>
        public void DeleteAndReplaceIncomeType(int id, int replaceId)
        {
            var incomes = _unitOfWork.Repository<IncomeDM>().All().Where(ex => ex.IncomeTypeId == id);
            foreach (var income in incomes)
            {
                var upd = new IncomeDM { Id = income.Id, Amount = income.Amount, Comment = income.Comment, Date = income.Date, IncomeTypeId = replaceId };
                _unitOfWork.Repository<IncomeDM>().Update(upd);
            }
            _unitOfWork.Repository<IncomeTypeDM>().Delete(id);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Deletes income
        /// </summary>
        /// <param name="id">Id of the income</param>
        public void DeleteIncome(int id)
        {
            _unitOfWork.Repository<IncomeDM>().Delete(id);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Gets income with requested Id
        /// </summary>
        /// <param name="id">Id of the income</param>
        /// <returns>
        /// Income
        /// </returns>
        public IncomeDM GetIncome(int id)
        {
            return _unitOfWork.Repository<IncomeDM>().Get(id);
        }

        /// <summary>
        /// Gets income type with requested Id
        /// </summary>
        /// <param name="id">Id of the type</param>
        /// <returns>
        /// Income type
        /// </returns>
        public IncomeTypeDM GetIncomeType(int id)
        {
            return _unitOfWork.Repository<IncomeTypeDM>().Get(id);
        }

        /// <summary>
        /// Gets all incomes using requested filters
        /// </summary>
        /// <param name="filter">FilterBLModel for filtration</param>
        /// <returns>
        /// Filled IncomesBLModel
        /// </returns>
        public Incomes GetAllIncomes(FilterBL filter)
        {
            var types = _unitOfWork.Repository<IncomeTypeDM>().All();
            if (!string.IsNullOrEmpty(filter.UserId))
            {
                types = types.Where(t => t.UserId == filter.UserId);
            }
            var incomes = types.Join(_unitOfWork.Repository<IncomeDM>().All(), t => t.Id, e => e.IncomeTypeId,
                     (t, e) => new Income { Id = e.Id, Amount = e.Amount, Date = e.Date, TypeName = t.Name });
            if (!string.IsNullOrEmpty(filter.TypeName))
            {
                incomes = incomes.Where(e => e.TypeName.Contains(filter.TypeName));
            }
            if (filter.MinAmount.HasValue)
            {
                incomes = incomes.Where(e => e.Amount >= filter.MinAmount.Value);
            }
            if (filter.MaxAmount.HasValue)
            {
                incomes = incomes.Where(e => e.Amount <= filter.MaxAmount.Value);
            }
            if (filter.MinDate.HasValue)
            {
                incomes = incomes.Where(e => e.Date >= filter.MinDate.Value);
            }
            if (filter.MaxDate.HasValue)
            {
                incomes = incomes.Where(e => e.Date <= filter.MaxDate.Value);
            }
            int count = incomes.Count();
            incomes = SortIncomeBLModel(incomes, filter.SortCol, filter.SortDir).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            return new Incomes { IncomesList = incomes, TotalCount = count };
        }

        /// <summary>
        /// Gets all income types for current user
        /// </summary>
        /// <param name="userId">current user Id</param>
        /// <returns>
        /// IEnumerable of income types
        /// </returns>
        public IEnumerable<IncomeTypeDM> GetAllIncomeTypes(string userId)
        {
            return _unitOfWork.Repository<IncomeTypeDM>().All().Where(t => t.UserId == userId).OrderBy(t => t.Name);
        }

        /// <summary>
        /// Updates income
        /// </summary>
        /// <param name="income">the income</param>
        public void UpdateIncome(IncomeDM income)
        {
            _unitOfWork.Repository<IncomeDM>().Update(income);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Updates income type
        /// </summary>
        /// <param name="type">the income type</param>
        public void UpdateIncomeType(IncomeTypeDM type)
        {
            _unitOfWork.Repository<IncomeTypeDM>().Update(type);
            _unitOfWork.Save();
        }

        private IQueryable<Income> SortIncomeBLModel(IQueryable<Income> incomes, string colName, SortDirection sortDir)
        {
            var result = incomes as IOrderedQueryable<Income>;
            switch (colName)
            {
                case nameof(Income.Amount):
                    if (sortDir == SortDirection.Ascending)
                    {
                        result = result.OrderBy(r => r.Amount);
                    }
                    else
                    {
                        result = result.OrderByDescending(r => r.Amount);
                    }
                    break;
                case nameof(Income.Date):
                    if (sortDir == SortDirection.Ascending)
                    {
                        result = result.OrderBy(r => r.Date);
                    }
                    else
                    {
                        result = result.OrderByDescending(r => r.Date);
                    }
                    break;
                case nameof(Income.TypeName):
                    if (sortDir == SortDirection.Ascending)
                    {
                        result = result.OrderBy(r => r.TypeName);
                    }
                    else
                    {
                        result = result.OrderByDescending(r => r.TypeName);
                    }
                    break;
                default:
                    result = result.OrderByDescending(r => r.Id);
                    break;
            }
            return result.ThenByDescending(r => r.Id);
        }
    }
}