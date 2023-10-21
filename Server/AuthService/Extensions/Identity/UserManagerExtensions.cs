using AuthService.Infrastructure.Data.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AuthService.Extensions.Identity
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> FindUserFromClaimsPrincipalByEmail(this UserManager<AppUser> userManager,
            ClaimsPrincipal user)
        {
            return await userManager.FindByEmailAsync(user.FindFirstValue(ClaimTypes.Email));
        }
    }
}
