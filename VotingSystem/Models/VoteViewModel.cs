using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystem.Models
{
    public class VoteViewModel
    {
        public int Id { get; set; }
        public DateTime DateVote { get; set; }
        public UserViewModel User { get; set; }
        public RestaurantViewModel Restaurant { get; set; }
    }
}
