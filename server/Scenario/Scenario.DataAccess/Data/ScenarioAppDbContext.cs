using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Scenario.Core.Entities;
using System.Reflection;

namespace Scenario.DataAccess.Data
{
    public class ScenarioAppDbContext : IdentityDbContext<AppUser>
    {
        public ScenarioAppDbContext(DbContextOptions<ScenarioAppDbContext> options) : base(options)
        {
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Plot> Plots { get; set; }
        public DbSet<PlotCategory> PlotCategories { get; set; }
        public DbSet<Scriptwriter> Scriptwriters { get; set; }
        public DbSet<PlotRating> PlotRatings { get; set; }
        public DbSet<PlotAppUser> PlotAppUsers { get; set; }
        public DbSet<ContactUs> Contacts { get; set; }
        public DbSet<Ad> Ads { get; set; }


        public DbSet<AboutTestimonial> AboutTestimonials { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<LikeDislike> LikeDislikes { get; set; }
        public DbSet<UserScenarioFavorite> UserScenarioFavorites { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

    }
}
