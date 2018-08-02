using System.Web.Mvc;

namespace IncomeAndExpenses.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}