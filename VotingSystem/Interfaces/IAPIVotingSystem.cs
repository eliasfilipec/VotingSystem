using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotingSystem.Models;

namespace VotingSystem.Interfaces
{
    public interface IAPIVotingSystemUser
    {
        [Get("/Get/Users")]
        Task<List<UserViewModel>> GetUsersAsync();

        [Get("/Get/User/{id}")]
        Task<UserViewModel> GetUserToIdAsync(int? id);

        [Post("/Post/User")]
        Task<string> PostUserAsync(string Name);

        [Put("/Put/User")]
        Task<string> UpdateUserAsync(UserViewModel user);

        [Delete("/Delete/User/{id}")]
        Task<string> DeleteUserAsync(int? id);
    }

    public interface IAPIVotingSystemRestaurant
    {
        [Get("Get/RestaurantsAvailable")]
        Task<List<RestaurantViewModel>> GetRestaurantsAvailableAsync();
    }

    public interface IAPIVotingSystemRanking
    {
        [Post("/Post/RankingToDate")]
        Task<List<RankingViewModel>> RankingToDateAsync(DateTime? datevote);
    }
}
