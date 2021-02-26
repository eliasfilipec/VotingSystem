using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotingSystem.Interfaces;
using VotingSystem.Models;

namespace VotingSystem.Controllers
{
    public class UsersController : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {
            List<UserViewModel> users = null;

            try
            {
                users = await RestService.For<IAPIVotingSystemUser>("https://localhost:44381/APISystemVoting/").GetUsersAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            try
            {
                var result = await RestService.For<IAPIVotingSystemUser>("https://localhost:44381/APISystemVoting/").PostUserAsync(name);

                ViewBag.Message = result;
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id is null)
                {
                    ViewBag.Message = "Id esta Nulo.";
                    return View();
                }

                var result = await RestService.For<IAPIVotingSystemUser>("https://localhost:44381/APISystemVoting/").GetUserToIdAsync(id);
                return View(result);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id is null)
                {
                    ViewBag.Message = "Id esta Nulo.";
                    return View();
                }

                var result = await RestService.For<IAPIVotingSystemUser>("https://localhost:44381/APISystemVoting/").GetUserToIdAsync(id);
                return View(result);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel user)
        {
            try
            {
                if (user is null)
                {
                    ViewBag.Message = "Objeto user esta nulo.";
                    return View();
                }

                var result = await RestService.For<IAPIVotingSystemUser>("https://localhost:44381/APISystemVoting/").UpdateUserAsync(user);
                ViewBag.Message = result;
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;

            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id is null)
                {
                    ViewBag.Message = "Id esta Nulo.";
                    return RedirectToAction("Index");
                }

                var result = await RestService.For<IAPIVotingSystemUser>("https://localhost:44381/APISystemVoting/").DeleteUserAsync(id);
                ViewBag.Message = result;
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}
