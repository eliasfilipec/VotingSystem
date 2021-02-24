using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace APIVotingSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class APISystemVotingController : ControllerBase
    {
        private readonly VotingContext _db;
        private readonly ILogger<APISystemVotingController> _logger;

        public APISystemVotingController(ILogger<APISystemVotingController> logger, VotingContext db)
        {
            _logger = logger;
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
            return await _db.User.ToListAsync();
        }

        [Route("Get/Restaurants"), HttpGet]
        public async Task<List<Restaurant>> GetRestaurants()
        {
            return await _db.Restaurant.ToListAsync();
        }

        [Route("Get/LoadSampleData"), HttpGet]
        //SAMPLE
        public async Task<string> LoadSampleData()
        {
            var text = string.Empty;

            if (_db.Restaurant.Count() == 0)
            {
                string file = System.IO.File.ReadAllText("generated_restaurants.json");
                var restaurants = JsonSerializer.Deserialize<List<Restaurant>>(file);
                _db.AddRange(restaurants);
                await _db.SaveChangesAsync();
                text += "Sample Data Restaurants Add\n";
            }
            else
            {
                text += "Contains Data Restaurant\n";
            }

            if (_db.User.Count() == 0)
            {
                string file = System.IO.File.ReadAllText("generated_users.json");
                var users = JsonSerializer.Deserialize<List<User>>(file);
                _db.AddRange(users);
                await _db.SaveChangesAsync();
                text += "Sample Data Restaurants Add";
            }
            else
            {
                text += "Contains Data User";
            }

            return text;
        }
    }
}
