﻿using EFDataAccessLibrary.DataAccess;
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

        #region USER
        [Route("Get/Users"), HttpGet]
        public async Task<List<User>> GetUsersAsync()
        {
            return await _db.User.ToListAsync();
        }

        [Route("Get/User/{id}"), HttpGet]
        public async Task<User> GetUserToIdAsync(int? id)
        {
            var result = await _db.User.Where(w => w.Id.Equals(id)).FirstOrDefaultAsync();
            if (result == null)
                result = new User();

            return result;
        }

        [Route("Put/User"), HttpPut]
        public async Task<string> UpdateUserAsync(User user)
        {
            try
            {
                if (user == null)
                    return "Objeto usuario esta nulo.";

                var entity = _db.User.FirstOrDefault(i => i.Id == user.Id);

                if (entity != null)
                {
                    entity.Name = user.Name;
                    await _db.SaveChangesAsync();
                    return "Usuario alterado com Sucesso!";
                }
                else
                {
                    return "Usuario Inexistente.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [Route("Post/User"), HttpPost]
        public async Task<string> InsertUserAsync(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                    return "Campo Nome do usuario esta vazio.";

                var users = GetUsersAsync();
                users.Wait();
                if (users.Result.Where(w => w.Name.ToUpper().Contains(name.ToUpper())).Any())
                    return "Usuario ja Existente.";

                var user = new User
                {
                    Name = name
                };

                await _db.AddAsync(user);
                await _db.SaveChangesAsync();

                return "Usuario inserido com Sucesso!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [Route("Delete/User/{id}"), HttpDelete]
        public async Task<string> DeleteUserAsync(int? id)
        {
            try
            {
                var result = await _db.User.Where(w => w.Id.Equals(id)).FirstOrDefaultAsync();

                if (result != null)
                {
                    _db.User.Remove(result);
                    await _db.SaveChangesAsync();
                    return "Usuario removido com Sucesso.";
                }
                else
                {
                    return "Nenhum registro encontrado.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion

        #region RESTAURANT
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
                    var datequery = DateTime.Now.AddDays(-i);

                    var resultVoteToDate = await _db.Vote.Where(w => w.DateVote.Date.Equals(datequery.Date))
                    .Include(r => r.Restaurant).ToListAsync();

                    if (resultVoteToDate.Count != 0)
                    {
                        var query = resultVoteToDate.GroupBy(x => x.Restaurant.Id)
                           .Select(group => new { result = group, Count = group.Count() })
                           .OrderByDescending(x => x.Count).FirstOrDefault();

                        var restaurant = query.result.Select(x => x.Restaurant).FirstOrDefault();
                        listRestaurants.Remove(restaurant);
                    }
                }
            }

            return listRestaurants;
        }
        #endregion

        #region SAMPLE DATA
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

        #endregion

        #region VOTE
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

        [Route("Put/InsertVote"), HttpPut]
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

        #endregion
    }
}