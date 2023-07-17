using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using off_tone.Persistence.Contexts;

namespace off_tone.Persistence.Extensions
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection RegisterPersistenceServices(this IServiceCollection services, IConfiguration config)
        {
            var connection = config.GetConnectionString("DefaultConnection");

            services.AddDbContext<BlogDbContext>(options => options.UseMySQL(config.GetConnectionString("DefaultConnection")));
            return services;
        }
    }
}
