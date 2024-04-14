using BlogService.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogService.Persistence.Data;

public static class DbInitializer
{
    public static async Task<bool> PrepDb(IApplicationBuilder app, bool isProd)
    {
        using (var serviceProvider = app.ApplicationServices.CreateScope())
        {
            var dbContext = serviceProvider.ServiceProvider.GetRequiredService<AppDbContext>();
            var migration = MigrateDatabase(dbContext, isProd);
            var seeding = await SeedData(dbContext);

            if (migration && seeding)
            {
                Console.WriteLine("--> Data preparation is done.");
                return true;
            }
            
            Console.WriteLine("--> Error while preparing the database.");
            return false;
        }
    }

    private static bool MigrateDatabase(AppDbContext dbContext, bool isProd)
    {
        if (!isProd) return true;
        if (!dbContext.Database.GetPendingMigrations().Any()) return true;
        Console.WriteLine("--> Has pending migrations...");
            
        try
        {
            Console.WriteLine("--> Migrating migrations...");
            dbContext.Database.Migrate();
        }
        catch (Exception ex)
        {
            Console.WriteLine("--> An error occured while migration: " + ex.Message);
            return false;
        }
        Console.WriteLine("--> Migration is a success.");
        return true;
    }

    private static async Task<bool> SeedData(AppDbContext dbContext)
    {
        if (dbContext.Blogs.Any())
        {
            Console.WriteLine("--> Blogs table is already seeded.");
            return true;
        }
        
        Console.WriteLine("--> Seeding is started.");
        bool isSuccess = false;
        try
        {
            for (int i = 1; i <= 100; i++)
            {
                var blog = new Blog
                {
                    BlogName = "Blog " + i,
                    BlogDescription = "Blog description" + i,
                    SubName = "Sub name " + i,
                    About = "About " + i
                };
                await dbContext.AddAsync(blog);
            }
            var result = await dbContext.SaveChangesAsync();
            if (result > 0) isSuccess = true;
        }
        catch (Exception ex)
        {
           Console.WriteLine("--> Exception while seeding data: " + ex); 
           return isSuccess;
        }
        return isSuccess;
    }
}