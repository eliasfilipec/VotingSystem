using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace APIVotingSystem
{
    public class HelpersDB
    {
        private readonly VotingContext _db;

        public HelpersDB()
        {
        }

        public HelpersDB(VotingContext db)
        {
            _db = db;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _db.User.ToListAsync();
        }

        public async Task<List<Restaurant>> GetRestaurantsAsync()
        {
            return await _db.Restaurant.ToListAsync();
        }

        public async Task<string> LoadSampleDataAsync()
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

        public async Task<string> InsertVote(int idUser, int idRestaurant)
        {
            try
            {
                var userResult = await _db.User.FindAsync(idUser);
                if (userResult == null)
                    return "Usuario Inexistente!";

                var restaurantResult = await _db.Restaurant.FindAsync(idRestaurant);
                if (restaurantResult == null)
                    return "Restaurante Inexistente!";

                var now = DateTime.UtcNow;

                var vote = new Vote();
                vote.User = userResult;
                vote.Restaurant = restaurantResult;

                return "";
            }
            catch (Exception ex)
            {
                return $"ERROR: {ex.Message}";
            }
        }
    }
}
