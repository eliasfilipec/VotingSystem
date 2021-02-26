using Microsoft.AspNetCore.Mvc.Rendering;
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
        public List<UserViewModel> Users { get; set; }
        public List<RestaurantViewModel> Restaurants { get; set; }
        public string SelectedIdUser { get; set; }
        public string SelectedIdRestaurant { get; set; }
    }
}
