using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotingSystem.Interfaces;
using VotingSystem.Models;

namespace VotingSystem.Controllers
{
    public class RestaurantController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<RestaurantViewModel> restaurants = null;

            try
            {
                restaurants = await RestService.For<IAPIVotingSystemRestaurant>("https://localhost:44381/APISystemVoting/").GetAllRestaurantsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(restaurants);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RestaurantViewModel restaurant)
        {
            try
            {
                if (!ValidationRestaurant(restaurant))
                {
                    ViewBag.Message = "Missing fields to be filled";
                    return View();
                }

                var result = await RestService.For<IAPIVotingSystemRestaurant>("https://localhost:44381/APISystemVoting/").PostRestaurantAsync(restaurant);

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

                var restaurant = await RestService.For<IAPIVotingSystemRestaurant>("https://localhost:44381/APISystemVoting/").GetRestaurantToIdAsync(id);

                if (!ValidationRestaurant(restaurant))
                {
                    ViewBag.Message = "Missing fields to be filled";
                    return View();
                }

                return View(restaurant);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RestaurantViewModel restaurant)
        {
            try
            {
                if (restaurant is null)
                {
                    ViewBag.Message = "Objeto user esta nulo.";
                    return View();
                }

                if (!ValidationRestaurant(restaurant))
                {
                    ViewBag.Message = "Missing fields to be filled";
                    return View();
                }

                var result = await RestService.For<IAPIVotingSystemRestaurant>("https://localhost:44381/APISystemVoting/").UpdateRestaurantAsync(restaurant);
                ViewBag.Message = result;
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

                var result = await RestService.For<IAPIVotingSystemRestaurant>("https://localhost:44381/APISystemVoting/").GetRestaurantToIdAsync(id);
                return View(result);
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

                var result = await RestService.For<IAPIVotingSystemRestaurant>("https://localhost:44381/APISystemVoting/").DeleteRestaurantAsync(id);
                ViewBag.Message = result;
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return RedirectToAction("Index");
        }

        private bool ValidationRestaurant(RestaurantViewModel restaurant)
        {
            if (string.IsNullOrEmpty(restaurant.Name) ||
                   string.IsNullOrEmpty(restaurant.Address) ||
                   string.IsNullOrEmpty(restaurant.Neighborhood) ||
                   string.IsNullOrEmpty(restaurant.Complement) ||
                   string.IsNullOrEmpty(restaurant.State) ||
                   string.IsNullOrEmpty(restaurant.Telephone) ||
                   string.IsNullOrEmpty(restaurant.ZipCode) ||
                   restaurant?.Number == null || restaurant.Number == 0)
                return false;
            return true;
        }
    }
}
