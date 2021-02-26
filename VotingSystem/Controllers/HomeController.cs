using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Refit;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using VotingSystem.Interfaces;
using VotingSystem.Models;

namespace VotingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {           
            var ranking = await RestService.For<IAPIVotingSystemRanking>("https://localhost:44381/APISystemVoting/").RankingToDateAsync(DateTime.UtcNow.AddHours(-3));

            return View(ranking);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
