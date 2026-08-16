using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KiaKooshar.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories (
            this IServiceCollection services
            )
        {
            var implementationAssembly = Assembly.GetExecutingAssembly ();
            var refrencedAssembelies =
                implementationAssembly
                .GetReferencedAssemblies ()
                .Select (Assembly.Load);
            var interfaceTypes = refrencedAssembelies
                .SelectMany (a => a.GetTypes ())
                .Where (t => t.IsInterface && t.Name.EndsWith ("Repository"))
                .ToList ();
            var implementationType = implementationAssembly
                .GetTypes ()
                .Where (t => t.IsClass && !t.IsAbstract)
                .ToList ();
            foreach ( var iface in interfaceTypes )
            {
                var implementation = implementationType
                    .FirstOrDefault (c => iface.IsAssignableFrom (c));
                if ( implementation != null )
                    services.AddScoped (iface, implementation);
            }
            return services;
        }
    }
}
