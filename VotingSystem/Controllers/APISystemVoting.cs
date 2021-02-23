using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace VotingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APISystemVoting : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return $"API Sytem Voting On-line [{DateTime.UtcNow}]";
        }

        public List<string> GetUsers()
        {
            return new List<string>();
        }
    }
}
