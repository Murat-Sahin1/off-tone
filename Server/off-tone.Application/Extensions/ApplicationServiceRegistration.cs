using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace off_tone.Application.Extensions
{
    public static class ApplicationServiceRegistration
    {
        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
