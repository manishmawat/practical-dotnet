using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApp001.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult UsersOnly()
        {
            return View();
        }

        [Authorize("Admin")]
        public IActionResult AdminsOnly()
        {
            return View();
        }

        public IActionResult Forbidden()
        {
            return View();
        }
    }
}