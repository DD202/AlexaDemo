namespace Alexa_GWV.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Alexa_GWV.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Alexa_GWV.Web.Models.ApplicationDbContext";
        }

        protected override void Seed(Alexa_GWV.Web.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.Courses.AddOrUpdate(
                c => c.Id,
                new Alexa.Entities.Course
                {
                    Id = 1,
                    Title = "Angular something",
                    Author = "John Pappa",
                    Content = "SOme long fun content",
                    Votes = 120,
                    DateCreated = new DateTime(2016,2,26)
                });

            context.Courses.AddOrUpdate(
                c => c.Id,
                new Alexa.Entities.Course
                {
                    Id = 2,
                    Title = "C# Fun",
                    Author = "Scott ALlen",
                    Content = "Something not boring",
                    Votes = 80,
                    DateCreated = new DateTime(2015, 9, 10)
                });

            context.Courses.AddOrUpdate(
                c => c.Id,
                new Alexa.Entities.Course
                {
                    Id = 3,
                    Title = "Fun web apps",
                    Author = "Jonathon Mills",
                    Content = "Something else",
                    Votes = 50,
                    DateCreated = new DateTime(2015, 9, 5)
                });

        }
    }
}
