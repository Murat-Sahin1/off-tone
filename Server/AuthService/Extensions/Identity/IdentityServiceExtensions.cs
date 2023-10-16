using AuthService.Infrastructure.Data.Identity.Contexts;
using AuthService.Infrastructure.Data.Identity.Entities;
using AuthService.Infrastructure.Services.Identity.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthService.Extensions.Identity
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection RegisterIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppIdentityDbContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("Postgres")));

            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {

            }).AddEntityFrameworkStores<AppIdentityDbContext>()
              .AddDefaultTokenProviders()
              .AddSignInManager<SignInManager<AppUser>>();

            services.AddScoped<ITokenService, TokenService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"])),
                        ValidIssuer = configuration["Token:Issuer"],
                        ValidateIssuer = true,
                    };
                });

            services.AddAuthorizationCore();

            return services;
        }
    }
}
