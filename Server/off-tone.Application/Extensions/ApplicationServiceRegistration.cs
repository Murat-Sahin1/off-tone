using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using off_tone.Application.Validators.Blogs;
using System.Reflection;

namespace off_tone.Application.Extensions
{
    public static class ApplicationServiceRegistration
    {
        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssemblyContaining<CreateBlogValidator>();
        }
    }
}
