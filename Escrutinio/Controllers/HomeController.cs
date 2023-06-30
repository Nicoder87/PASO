using Escrutinio.Filters;
using System.Web.Mvc;

namespace Escrutinio.Controllers
{
    public class HomeController : Controller
    {
        [AuthorizeRule]
        public ActionResult Index()
        {
            return View();
        }
    }
}
