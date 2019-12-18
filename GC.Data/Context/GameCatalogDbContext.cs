using GC.Data.Entities;
using System.Data.Entity;

namespace GC.Data.Context
{
    public class GameCatalogDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}