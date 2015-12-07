using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGameStore.Model;

namespace WebGameStore.DAL
{
    public class StoreDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Comment> Comments { get; set; }
        ////public DbSet<Genre> Genres { get; set; }
        ////public DbSet<PlatformType> PlatformTypes { get; set; }
        ////public DbSet<GameGenre> GameGenres { get; set; }
        ////public DbSet<GamePlatformType> GamePlatformTypes { get; set; }

        public StoreDbContext() : base("StoreDbConnection")
        {

        }
    }
}
