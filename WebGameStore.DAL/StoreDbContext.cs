using WebGameStore.Model;

namespace WebGameStore.DAL
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class StoreDbContext : DbContext
    {
        // Your context has been configured to use a 'StoreDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'WebGameStore.DAL.StoreDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'StoreDbContext' 
        // connection string in the application configuration file.
        public StoreDbContext()
            : base("name=StoreDbContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<PlatformType> PlatformTypes { get; set; }
        public DbSet<GameGenre> GameGenres { get; set; }
        public DbSet<GamePlatformType> GamePlatformTypes { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}