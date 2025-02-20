
using Microsoft.AspNetCore.Identity;

namespace Scenario.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? UserImg { get; set; }
        public List<Comment> Comments { get; set; }
        // İstifadəçinin bəyəndiyi ssenarilər
        public ICollection<UserScenarioFavorite> LikedScenarios { get; set; }
        //todo bunu Plot cedveline elave et
        /*
        // Ssenarini bəyənən istifadəçilər
        public ICollection<UserLikedScenario> LikedByUsers { get; set; }
        */
    }
}
