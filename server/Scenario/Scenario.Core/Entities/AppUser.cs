
using Microsoft.AspNetCore.Identity;

namespace Scenario.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string? UserImg { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
