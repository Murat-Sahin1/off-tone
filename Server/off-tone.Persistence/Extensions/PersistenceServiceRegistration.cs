using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using off_tone.Persistence.Contexts;
using off_tone.Application.Interfaces.Repositories.BlogPostRepos;
using off_tone.Persistence.Repositories.BlogPostRepos;
using off_tone.Application.Interfaces.Repositories.BlogRepos;
using off_tone.Persistence.Repositories.BlogRepos;
using off_tone.Application.Interfaces.Repositories.TagRepos;
using off_tone.Persistence.Repositories.TagRepos;

namespace off_tone.Persistence.Extensions
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection RegisterPersistenceServices(this IServiceCollection services, IConfiguration config)
        {         

            services.AddDbContext<BlogDbContext>(options => options.UseNpgsql(config.GetConnectionString("PostgresConnection")));

            services.AddScoped<IBlogPostReadRepository, BlogPostReadRepository>();
            services.AddScoped<IBlogPostWriteRepository, BlogPostWriteRepository>();

            services.AddScoped<IBlogReadRepository, BlogReadRepository>();
            services.AddScoped<IBlogWriteRepository, BlogWriteRepository>();

            services.AddScoped<ITagReadRepository, TagReadRepository>();
            services.AddScoped<ITagWriteRepository, TagWriteRepository>();

            return services;
        }
    }
}
