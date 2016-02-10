using System.Web.Mvc;

namespace EsAws.WebApi.Controllers.Mvc
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
