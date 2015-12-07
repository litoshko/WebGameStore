using System.Collections.Generic;
using WebGameStore.Model;

namespace WebGameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<WebGameStore.DAL.StoreDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(WebGameStore.DAL.StoreDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            
            var games = new List<Game>
            {
                new Game { Key = "1", Name = "One Game", Description = "First game description"},
                new Game { Key = "2", Name = "Two Game", Description = "Second game description"}
            };
            games.ForEach(g => context.Games.AddOrUpdate(e => e.Name, g));
            context.SaveChanges();

            var comments = new List<Comment>
            {
                new Comment {Name = "First Comment", Body = "Comment body #1", GameKey = "1"},
                new Comment {Name = "Second Comment", Body = "Comment body #2", GameKey = "2"},
                new Comment {Name = "Third Comment", Body = "Comment body #3", ParentCommentId = 1}
            };
            comments.ForEach(c => context.Comments.AddOrUpdate(e => e.Name, c));
            context.SaveChanges();

            var genres = new List<Genre>
            {
                new Genre {Name = "Strategy"},
                new Genre {Name = "RPG"},
                new Genre {Name = "Sports"},
                new Genre {Name = "Races"},
                new Genre {Name = "Action"},
                new Genre {Name = "Adventure"},
                new Genre {Name = "Puzzle&Skill"},
                new Genre {Name = "RTS", ParentName = "Strategy"},
                new Genre {Name = "TBS", ParentName = "Strategy"},
                new Genre {Name = "Rally", ParentName = "Races"},
                new Genre {Name = "Arcade", ParentName = "Races"},
                new Genre {Name = "Formula", ParentName = "Races"},
                new Genre {Name = "Off-Road", ParentName = "Races"}
            };
            genres.ForEach(g => context.Genres.AddOrUpdate(e => e.Name, g));
            context.SaveChanges();

            var gameGenres = new List<GameGenre>
            {
                new GameGenre {GameKey = "1", GenreName = "Strategy"},
                new GameGenre {GameKey = "2", GenreName = "RTS"}
            };
            gameGenres.ForEach(gg => context.GameGenres.AddOrUpdate(e => e.GameKey, gg));
            context.SaveChanges();

            var platforms = new List<PlatformType>
            {
                new PlatformType {Type = "mobile"},
                new PlatformType {Type = "browser"},
                new PlatformType {Type = "desktop"},
                new PlatformType {Type = "console"}
            };
            platforms.ForEach(p => context.PlatformTypes.AddOrUpdate(e => e.Type, p));
            context.SaveChanges();

            var gamePlatforms = new List<GamePlatformType>
            {
                new GamePlatformType {GameKey = "1", PlatformTypeType = "mobile"},
                new GamePlatformType {GameKey = "2", PlatformTypeType = "browser"}
            };
            gamePlatforms.ForEach(gp => context.GamePlatformTypes.AddOrUpdate(e => e.GameKey, gp));
            context.SaveChanges();
            
        }
    }
}
