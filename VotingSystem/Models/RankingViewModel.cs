using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystem.Models
{
    public class RankingViewModel
    {
        public List<UserViewModel> usersVote { get; set; }
        public RestaurantViewModel restaurantVote { get; set; }
        public int CountVotes { get; set; }    
    }
}
