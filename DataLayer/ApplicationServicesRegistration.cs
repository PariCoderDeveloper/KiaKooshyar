using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KiaKooshar.Application
{
    public static class ApplicationServicesRegistration
    {
        public static void ConfigureApplicationServices
            (
                this IServiceCollection services,
                IConfiguration configuration
            )
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
