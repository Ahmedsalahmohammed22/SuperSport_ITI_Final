using FinalProject_ITI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_ITI.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Player> players { get; set; }
        public DbSet<Team> teams { get; set; }
        public DbSet<TopLeague> topLeagues { get; set; }
        public ApplicationDbContext(DbContextOptions options):base(options)
        {
            
        }
    }
}
