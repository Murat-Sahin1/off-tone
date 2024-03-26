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
using off_tone.Application.Interfaces.Repositories.ReviewRepos;
using off_tone.Persistence.Repositories.ReviewRepos;
using off_tone.Persistence.Services.BlogPostServices;

namespace off_tone.Persistence.Extensions
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection RegisterPersistenceServices(this IServiceCollection services, IConfiguration config)
        {

            services.AddDbContext<BlogDbContext>(options =>
            {
                options.UseNpgsql(config.GetConnectionString("PostgresConnection"));
                options.EnableSensitiveDataLogging();
            });

            services.AddScoped<IBlogPostReadRepository, BlogPostReadRepository>();
            services.AddScoped<IBlogPostWriteRepository, BlogPostWriteRepository>();

            services.AddScoped<IBlogReadRepository, BlogReadRepository>();
            services.AddScoped<IBlogWriteRepository, BlogWriteRepository>();

            services.AddScoped<ITagReadRepository, TagReadRepository>();
            services.AddScoped<ITagWriteRepository, TagWriteRepository>();

            services.AddScoped<IReviewReadRepository, ReviewReadRepository>();
            services.AddScoped<IReviewWriteRepository, ReviewWriteRepository>();

            services.AddScoped<BlogPostFilterMenu>();

            return services;
        }
    }
}
