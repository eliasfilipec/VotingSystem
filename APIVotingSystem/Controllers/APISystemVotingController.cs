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
        private readonly VotingContext _db;

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
        public async Task<List<User>> GetUsersAsync()
        {
            return await _db.User.ToListAsync();
        }

        [Route("Get/Restaurants"), HttpGet]
        public async Task<List<Restaurant>> GetAllRestaurantsAsync()
        {
            return await _db.Restaurant.ToListAsync();
        }

        [Route("Get/RestaurantsAvailable"), HttpGet]
        public async Task<List<Restaurant>> GetRestaurantsAvailableAsync()
        {
            var dataweek = (int)DateTime.Now.DayOfWeek;

            var listRestaurants = new List<Restaurant>();
            listRestaurants = await _db.Restaurant.ToListAsync();
            if (dataweek != 1)
            {
                for (int i = 1; i < dataweek; i++)
                {
                    var datequery = DateTime.Now.AddDays(-1);

                    var resultVoteToDate = await _db.Vote.Where(w => w.DateVote.Date.Equals(datequery.Date))
                    .Include(r => r.Restaurant).ToListAsync();

                    var query = resultVoteToDate.GroupBy(x => x.Restaurant.Id)
                        .Select(group => new { result = group, Count = group.Count() })
                        .OrderByDescending(x => x.Count).FirstOrDefault();

                    var teste = query.result.Select(x => x.Restaurant).FirstOrDefault();
                    listRestaurants.Remove(teste);
                }
            }

            return listRestaurants;
        }

        [Route("Get/LoadSampleData"), HttpGet]
        public async Task<string> LoadSampleDataAsync()
        {
            var text = string.Empty;

            if (!_db.Restaurant.Any())
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

            if (!_db.User.Any())
            {
                string file = System.IO.File.ReadAllText("generated_users.json");
                var users = JsonSerializer.Deserialize<List<User>>(file);
                _db.AddRange(users);
                await _db.SaveChangesAsync();
                text += "Sample Data User Add";
            }
            else
            {
                text += "Contains Data User";
            }

            return text;
        }

        [Route("Post/RankingToDate"), HttpPost]
        public async Task<List<Ranking>> RankingToDateAsync(DateTime? dateRanking)
        {
            if (!dateRanking.HasValue)
                dateRanking = DateTime.UtcNow.AddHours(-3);

            var resultVoteToDate = await _db.Vote.Where(w => w.DateVote.Date.Equals(dateRanking.Value.Date))
                .Include(u => u.User)
                .Include(r => r.Restaurant).ToListAsync();

            List<Ranking> listranking = new List<Ranking>();

            if (!resultVoteToDate.Any())
                return listranking;

            var restaurants = await GetRestaurantsAsync();

            var query = resultVoteToDate.GroupBy(x => x.Restaurant.Id)
                .Select(group => new { result = group, Count = group.Count() })
                .OrderByDescending(x => x.Count).ToList();
            
            foreach (var rest in query)
            {
                Ranking itemRanking = new Ranking();
                itemRanking.CountVotes = rest.Count;
                itemRanking.restaurantVote = rest.result.Select(x => x.Restaurant).FirstOrDefault();
                itemRanking.usersVote = rest.result.Select(s => s.User).ToList();
                listranking.Add(itemRanking);
            }

            return listranking;
        }

        [Route("Post/InsertVote"), HttpPost]
        public async Task<string> InsertVoteAsync(int idUser, int idRestaurant)
        {
            try
            {
                var userResult = await _db.User.FindAsync(idUser);
                if (userResult == null)
                    return "Usuario Inexistente!";

                var restaurantResult = await _db.Restaurant.FindAsync(idRestaurant);
                if (restaurantResult == null)
                    return "Restaurante Inexistente!";

                var now = DateTime.UtcNow.AddHours(-3);

                if (now.Hour > 12)
                    return "Tempo de Votacao expirado!";

                var voteResult = await _db.Vote.
                    Where(w => w.DateVote.Date.Equals(now.Date)).
                    Where(w => w.User.Id.Equals(userResult.Id)).ToListAsync();

                if (voteResult.Any())
                    return "Usuario ja realizou o votou!";

                var vote = new Vote();
                vote.DateVote = now;
                vote.User = userResult;
                vote.Restaurant = restaurantResult;

                await _db.AddAsync(vote);
                await _db.SaveChangesAsync();

                return "Voto inserido com Sucesso!";
            }
            catch (Exception ex)
            {
                return $"ERROR: {ex.Message}";
            }
        }
    }
}