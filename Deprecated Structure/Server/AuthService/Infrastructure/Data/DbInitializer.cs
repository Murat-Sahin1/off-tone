using Microsoft.Extensions.Identity.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AuthService.Infrastructure.Data.Identity.Contexts;
using AuthService.Infrastructure.Data.Identity.Entities;

namespace AuthService.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task PrepDb(AppIdentityDbContext dbContext)
        {
            await ApplyMigrations(dbContext);
        }

        private static async Task ApplyMigrations(AppIdentityDbContext dbContext)
        {
            if (dbContext.Database.GetPendingMigrations().Count() > 0)
            {
                Console.WriteLine("--> Applying new migration/s.");
                try
                {
                    await dbContext.Database.MigrateAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not apply new migrations! Message: {ex.Message}");
                }
            }
        }

        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                Console.WriteLine("--> Seeding users.");

                var user = new AppUser
                {
                    DisplayName = "Bob",
                    Email = "adm@test.com",
                    UserName = "Bob135",
                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }

        }
    }
}

