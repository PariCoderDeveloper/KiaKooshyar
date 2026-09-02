using KiaKooshar.Infrastructure.Persistence.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KiaKooshar.Infrastructure.Persistence
{
    public class DatabaseMigration
    {
        public static async Task ApplyAsync (
            IServiceProvider service,
            CancellationToken cancellationToken = default
            )
        {
            using var scope = service.CreateScope ();
            var context = scope.ServiceProvider
                .GetRequiredService<DatabaseContext> ();
            await context.Database.MigrateAsync (cancellationToken);
            await DatabaseSeeder.SeedAsync
                (context, cancellationToken);
        }
    }
}
