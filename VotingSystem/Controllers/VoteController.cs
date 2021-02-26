using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystem.Interfaces;
using VotingSystem.Models;

namespace VotingSystem.Controllers
{
    public class VoteController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var voteModel = new VoteViewModel();
            try
            {
                voteModel.Restaurants = await RestService.For<IAPIVotingSystemRestaurant>("https://localhost:44381/APISystemVoting/").GetRestaurantsAvailableAsync();
                voteModel.Users = await RestService.For<IAPIVotingSystemUser>("https://localhost:44381/APISystemVoting/").GetUsersAsync();

            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            
            return View(voteModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(VoteViewModel vote)
        {
            try
            {
                vote.Restaurants = await RestService.For<IAPIVotingSystemRestaurant>("https://localhost:44381/APISystemVoting/").GetRestaurantsAvailableAsync();
                vote.Users = await RestService.For<IAPIVotingSystemUser>("https://localhost:44381/APISystemVoting/").GetUsersAsync();

                if (string.IsNullOrEmpty(vote.SelectedIdUser))
                {
                    ViewBag.Message = "Select User Please!";
                    return View(vote);
                }
                if (string.IsNullOrEmpty(vote.SelectedIdRestaurant))
                {
                    ViewBag.Message = "Select Restaurant Please!";
                    return View(vote);
                }

                vote.DateVote = DateTime.UtcNow.AddHours(-3);
                
                var IdUser = Convert.ToInt32(vote.SelectedIdUser);
                var IdRestaurant = Convert.ToInt32(vote.SelectedIdRestaurant);

                //Verificar se valida antes das 11:45
                ViewBag.Message = await RestService.For<IAPIVotingSystemVote>("https://localhost:44381/APISystemVoting/")
                    .InsertVoteAsync(IdUser, IdRestaurant);
            }
            catch (Exception ex)
            {
                ViewBag.Message= ex.Message;
            }

            return View(vote);
        }
    }
}
