using AuthService.Data.Identity.Contexts;
using AuthService.Data.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Data
{
    public static class DbInitializer
    {
        public static void PrepDb(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();

                ApplyMigrations(dbContext);
            }
        }

        private static void ApplyMigrations(AppIdentityDbContext dbContext)
        {
            if (dbContext.Database.GetPendingMigrations().Count() > 0)
            {
                Console.WriteLine("--> Applying new migration/s.");
                try
                {
                    dbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not apply new migrations! Message: {ex.Message}");
                }
            }
        }

        private static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Bob",
                    Email = "adm@test.com",
                    UserName = "Bob135",
                };
                await userManager.CreateAsync(user, "mysecretpassword");
            }
        }
    }
}
