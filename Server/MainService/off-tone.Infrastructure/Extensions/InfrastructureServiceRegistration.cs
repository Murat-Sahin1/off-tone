using Microsoft.Extensions.DependencyInjection;
using off_tone.Infrastructure.AsyncDataServices;
using off_tone.Infrastructure.AsyncDataServices.Interfaces;
using System.Reflection;

namespace off_tone.Infrastructure.Extensions
{
    public static class InfrastructureServiceRegistration
    {
        public static void RegisterInfrastructureService(this IServiceCollection services)
        {
            services.AddSingleton<IMessageBusClient, MessageBusClient>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
