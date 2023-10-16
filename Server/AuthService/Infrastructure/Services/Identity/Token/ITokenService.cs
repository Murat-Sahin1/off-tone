using AuthService.Infrastructure.Data.Identity.Entities;

namespace AuthService.Infrastructure.Services.Identity.Token
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
