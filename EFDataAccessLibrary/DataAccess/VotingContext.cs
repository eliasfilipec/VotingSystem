using EFDataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace EFDataAccessLibrary.DataAccess
{
    public class VotingContext : DbContext
    {
        public VotingContext(DbContextOptions option) : base(option) { }
        public DbSet<User> User { get; set; }
        public DbSet<Restaurant> Restaurant { get; set; }
        public DbSet<Vote> Vote { get; set; }
    }
}
