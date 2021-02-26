using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystem.Interfaces;

namespace VotingSystem.Controllers
{
    public class VoteController : Controller
    {
        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.Users = await RestService.For<IAPIVotingSystemUser>("https://localhost:44381/APISystemVoting/").GetUsersAsync();
                ViewBag.RestaurantAvailable = await RestService.For<IAPIVotingSystemRestaurant>("https://localhost:44381/APISystemVoting/").GetRestaurantsAvailableAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return View();
        }
    }
}
