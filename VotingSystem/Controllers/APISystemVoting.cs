using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace VotingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APISystemVoting : ControllerBase
    {
        private readonly VotingContext _db;

        public APISystemVoting(VotingContext db)
        {
            _db = db;
        }

        [HttpGet]
        public string Get()
        {
            return $"API Sytem Voting On-line [{DateTime.UtcNow}]";
        }

        [HttpGet]
        [Route("GetUsers")]
        public List<string> GetUsers()
        {
            return new List<string>();
        }

        [HttpGet]
        [Route("GetRestaurants")]
        public List<Restaurant> GetRestaurants()
        {
            LoadSampleData();
            return _db.Restaurant.ToList();
        }

        private void LoadSampleData()
        {
            if (_db.Restaurant.Count() == 0)
            {
                string file = System.IO.File.ReadAllText("generated_restaurants.json");
                var restaurants = JsonSerializer.Deserialize<List<Restaurant>>(file);
                _db.AddRange(restaurants);
                _db.SaveChanges();
            }
        }
    }
}
