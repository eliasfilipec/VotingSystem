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
        private readonly ILogger<APISystemVotingController> _logger;

        public APISystemVotingController(ILogger<APISystemVotingController> logger)
        {
            _logger = logger;
        }

        [Route("Get"), HttpGet]
        public string Get()
        {
            return $"API Sytem Voting On-line [{DateTime.UtcNow}]";
        }

        [Route("Get/Users"), HttpGet]
        public async Task<List<User>> GetUsers()
        {
            return await new HelpersDB().GetUsersAsync();
        }

        [Route("Get/Restaurants"), HttpGet]
        public async Task<List<Restaurant>> GetRestaurants()
        {
            return await new HelpersDB().GetRestaurantsAsync();
        }

        [Route("Get/LoadSampleData"), HttpGet]
        //SAMPLE
        public async Task<string> LoadSampleData()
        {
            return await new HelpersDB().LoadSampleDataAsync();
        }
    }
}
