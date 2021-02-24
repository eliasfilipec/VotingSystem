using Microsoft.AspNetCore.Mvc;

namespace VotingSystem.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RegisterUser()
        {
            return View();
        }

        public IActionResult RegisterRestaurants()
        {
            return View();
        }
    }
}
