using IncomeAndExpenses.DataAccessImplement;
using IncomeAndExpenses.DataAccessInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web.Controllers
{
    public class BaseController : Controller
    {
        protected IUnitOfWork _unitOfWork;
        protected string UserId { get { return (HttpContext?.User.Identity as ClaimsIdentity)?.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value; } }

        public BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public BaseController() : this(null) { }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}