using Microsoft.AspNetCore.Identity;

namespace AuthService.Data.Identity.Entities
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}