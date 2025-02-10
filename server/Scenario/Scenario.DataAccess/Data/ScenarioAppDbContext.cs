using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Scenario.Core.Entities;
using System.Reflection;

namespace Scenario.DataAccess.Data
{
    public class ScenarioAppDbContext : IdentityDbContext<AppUser>
    {
        public ScenarioAppDbContext(DbContextOptions options) : base(options)
        {
        }

        //public DbSet<Actor> Actors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

    }
}
