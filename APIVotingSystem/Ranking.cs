using EFDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIVotingSystem
{
    public class Ranking
    {
        public List<User> usersVote { get; set; }
        public Restaurant restaurantVote { get; set; }
        public int CountVotes { get; set; }
    }
}
