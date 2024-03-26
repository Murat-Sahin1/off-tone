using Microsoft.AspNetCore.Identity;

namespace AuthService.Infrastructure.Data.Identity.Entities
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}