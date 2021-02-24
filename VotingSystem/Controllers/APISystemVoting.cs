using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

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

        [Route("Get"), HttpGet]
        public string Get()
        {
            return $"API Sytem Voting On-line [{DateTime.UtcNow}]";
        }
                
        [Route("Get/Users"), HttpGet]
        public async Task<List<User>> GetUsers()
        {
            LoadSampleData();
            return await _db.User.ToListAsync();
        }

        [Route("Get/Restaurants"), HttpGet]
        public async Task<List<Restaurant>> GetRestaurants()
        {
            LoadSampleData();
            return await _db.Restaurant.ToListAsync();
        }

        //SAMPLE
        private void LoadSampleData()
        {
            if (_db.Restaurant.Count() == 0)
            {
                string file = System.IO.File.ReadAllText("generated_restaurants.json");
                var restaurants = JsonSerializer.Deserialize<List<Restaurant>>(file);
                _db.AddRange(restaurants);
                _db.SaveChanges();
            }

            if (_db.User.Count() == 0)
            {
                string file = System.IO.File.ReadAllText("generated_users.json");
                var users = JsonSerializer.Deserialize<List<User>>(file);
                _db.AddRange(users);
                _db.SaveChanges();
            }
        }
    }
}
