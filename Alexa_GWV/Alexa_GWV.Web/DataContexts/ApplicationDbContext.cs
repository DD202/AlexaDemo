using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Alexa.Entities;

//namespace Alexa_GWV.Web.DataContexts
namespace Alexa_GWV.Web.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Member> Members { get; set; }

        public DbSet<Request> Requests { get; set; }

        public DbSet<UserPreference> UserPreferences { get; set; }

        public DbSet<InformationalFact> InformationalFacts { get; set; }

        public DbSet<Picture> Pictures { get; set; }
    }
}
