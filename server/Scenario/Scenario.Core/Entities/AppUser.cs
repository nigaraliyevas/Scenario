
using Microsoft.AspNetCore.Identity;

namespace Scenario.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string? UserImg { get; set; }
        public List<Comment> Comments { get; set; }
        public List<PlotAppUser> PlotAppUsers { get; set; }//mant to many relation
        // İstifadəçinin bəyəndiyi ssenarilər
        public List<UserScenarioFavorite> LikedScenarios { get; set; }
        //todo bunu Plot cedveline elave et
        /*
        // Ssenarini bəyənən istifadəçilər
        public ICollection<UserLikedScenario> LikedByUsers { get; set; }
        */
    }
}
