using AuthService.Data.Identity.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Extensions.Identity
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection RegisterIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppIdentityDbContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("Postgres")));

            return services;
        }
    }
}
