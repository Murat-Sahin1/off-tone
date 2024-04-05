using BlogService.Persistence.Data;
using BlogService.Persistence.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlogService.Persistence.Extensions;

public static class PersistenceLayerExtensions
{
    public static SD.CurrentDB RegisterDbContext(this IServiceCollection serviceCollection, IWebHostEnvironment webHostEnvironment)
    {
        if (webHostEnvironment.IsProduction())
        {
            serviceCollection.AddDbContext<AppDbContext>(
                /* opt => opt.UsePostgres */
            );
            return SD.CurrentDB.PostgreSQL;
        }

        serviceCollection.AddDbContext<AppDbContext>(
            opt => opt.UseInMemoryDatabase("InMem")
        );
        return SD.CurrentDB.InMemory;
    }
}

