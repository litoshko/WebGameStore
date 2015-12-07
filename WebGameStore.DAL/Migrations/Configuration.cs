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

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
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

            ////var courses = new List<Course>
            ////{
            ////    new Course {CourseID = 1050, Title = "Chemistry",      Credits = 3, },
            ////    new Course {CourseID = 4022, Title = "Microeconomics", Credits = 3, }
            ////};
            ////courses.ForEach(s => context.Courses.AddOrUpdate(p => p.Title, s));
            ////context.SaveChanges();

            ////var enrollments = new List<Enrollment>
            ////{
            ////    new Enrollment {
            ////        StudentID = students.Single(s => s.LastName == "Alexander").ID,
            ////        CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
            ////        Grade = Grade.A
            ////    },
            ////     new Enrollment {
            ////        StudentID = students.Single(s => s.LastName == "Alexander").ID,
            ////        CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
            ////        Grade = Grade.C
            ////     }
            ////};

            ////foreach (Enrollment e in enrollments)
            ////{
            ////    var enrollmentInDataBase = context.Enrollments.Where(
            ////        s =>
            ////             s.Student.ID == e.StudentID &&
            ////             s.Course.CourseID == e.CourseID).SingleOrDefault();
            ////    if (enrollmentInDataBase == null)
            ////    {
            ////        context.Enrollments.Add(e);
            ////    }
            ////}
            ////context.SaveChanges();
        }
    }
}
