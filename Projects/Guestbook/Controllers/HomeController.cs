using Microsoft.AspNetCore.Mvc;

namespace Guestbook.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("Name") != null)
                return View();
            else
                return RedirectToAction("Login", "Account");
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}