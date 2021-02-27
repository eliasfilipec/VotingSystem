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
        [Get("/Get/RestaurantsAvailable")]
        Task<List<RestaurantViewModel>> GetRestaurantsAvailableAsync();

        [Get("/Get/Restaurants")]
        Task<List<RestaurantViewModel>> GetAllRestaurantsAsync();

        [Get("/Get/Restaurant/{id}")]
        Task<RestaurantViewModel> GetRestaurantToIdAsync(int? id);

        [Post("/Post/Restaurant")]
        Task<string> PostRestaurantAsync(RestaurantViewModel restaurant);

        [Put("/Put/Restaurant")]
        Task<string> UpdateRestaurantAsync(RestaurantViewModel restaurant);

        [Delete("/Delete/Restaurant/{id}")]
        Task<string> DeleteRestaurantAsync(int? id);
    }

    public interface IAPIVotingSystemRanking
    {
        [Post("/Post/RankingToDate")]
        Task<List<RankingViewModel>> RankingToDateAsync(DateTime? datevote);
    }

    public interface IAPIVotingSystemVote
    {
        [Put("/Put/InsertVote")]
        Task<string> InsertVoteAsync(int idUser, int idRestaurant);
    }
}
