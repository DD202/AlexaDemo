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
           
            context.InformationalFacts.AddOrUpdate(

                c => c.Id,
                new Alexa.Entities.InformationalFact
                {
                    Id = 1,
                    Fact = "If you have 3 quarters, 4 dimes, and 4 pennies, you have $1.19. You also have the largest amount of money in coins without being able to make change for a dollar.",
                    Votes = 0
                });
            context.InformationalFacts.AddOrUpdate(

                c => c.Id,
                new Alexa.Entities.InformationalFact
                {
                    Id = 2,
                    Fact = "The numbers '172' can be found on the back of the U.S. $5 dollar bill in the bushes at the base of the Lincoln Memorial.",
                    Votes = 0
                });

            context.InformationalFacts.AddOrUpdate(

                c => c.Id,
                new Alexa.Entities.InformationalFact
                {
                    Id = 3,
                    Fact = "President Kennedy was the fastest random speaker in the world with upwards of 350 words per minute.",
                    Votes = 0
                });

            context.InformationalFacts.AddOrUpdate(

                c => c.Id,
                new Alexa.Entities.InformationalFact
                {
                    Id = 4,
                    Fact = "In the average lifetime, a person will walk the equivalent of 5 times around the equator.",
                    Votes = 0
                });

            context.InformationalFacts.AddOrUpdate(

                c => c.Id,
                new Alexa.Entities.InformationalFact
                {
                    Id = 5,
                    Fact = "Odontophobia is the fear of teeth.",
                    Votes = 0
                });

            context.InformationalFacts.AddOrUpdate(

                c => c.Id,
                new Alexa.Entities.InformationalFact
                {
                    Id = 6,
                    Fact = "The 57 on Heinz ketchup bottles represents the number of varieties of pickles the company once had.",
                    Votes = 0
                });

            context.InformationalFacts.AddOrUpdate(

                c => c.Id,
                new Alexa.Entities.InformationalFact
                {
                    Id = 7,
                    Fact = "The surface area of an average-sized brick is 79 cm squared.",
                    Votes = 0
                });

            context.InformationalFacts.AddOrUpdate(

                c => c.Id,
                new Alexa.Entities.InformationalFact
                {
                    Id = 8,
                    Fact = "Cats sleep 16 to 18 hours per day.",
                    Votes = 0
                });

            context.InformationalFacts.AddOrUpdate(

                c => c.Id,
                new Alexa.Entities.InformationalFact
                {
                    Id = 9,
                    Fact = "The Eisenhower interstate system requires that one mile in every five must be straight. These straight sections are usable as airstrips in times of war or other emergencies.",
                    Votes = 0
                });

            context.InformationalFacts.AddOrUpdate(

                c => c.Id,
                new Alexa.Entities.InformationalFact
                {
                    Id = 10,
                    Fact = "When you die your hair still grows for a couple of months.",
                    Votes = 0
                });

            context.Pictures.AddOrUpdate(
                    c => c.Id,
                    new Alexa.Entities.Picture
                    {
                        Id = 1,
                        Description = "Party Shark",
                        LargeImageUrl = "https://alexa-gwv-test.azurewebsites.net/Images/rand1.jpeg",
                        SmallImageUrl = "https://alexa-gwv-test.azurewebsites.net/Images/rand1.jpeg"
                    }
                );
            

        }
    }
}
