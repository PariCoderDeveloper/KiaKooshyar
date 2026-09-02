using KiaKooshar.Infrastructure.Persistence.Seed.Permission;
using KiaKooshar.Infrastructure.Persistence.Seed.Role;
using Microsoft.EntityFrameworkCore;

namespace KiaKooshar.Infrastructure.Persistence.Seed
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync (
            DatabaseContext context,
            CancellationToken cancellationToken = default )
        {
            await SeedPermissionsAsync (context, cancellationToken);
            await SeedRoleAsync (context, cancellationToken);
        }

        private static async Task SeedPermissionsAsync (
            DatabaseContext context,
            CancellationToken cancellationToken = default
            )
        {
            var permissions = PermissionSeedData.GetPermissions ();

            var existingCodes = await context.Permissions
                .Select (x => x.Code)
                .ToListAsync (cancellationToken);

            var newPermissions = permissions
                .Where (x => !existingCodes.Contains (x.Code))
                .ToList ();

            if ( newPermissions.Count == 0 )
                return;
            await ExecuteWithIdentityInsertAsync (
                context,
                "Permissions",
                async () =>
                {
                    await context.Permissions.AddRangeAsync (
                        newPermissions,
                        cancellationToken);

                    await context.SaveChangesAsync (cancellationToken);
                },
                cancellationToken
                );
        }
        private static async Task SeedRoleAsync (
            DatabaseContext context,
            CancellationToken cancellationToken = default
            )
        {
            var roles = RoleSeedData.GetRoles ();

            var existingCodes = await context.Roles
                .Select (x => x.Code)
                .ToListAsync (cancellationToken);

            var newRoles = roles
                .Where (x => !existingCodes.Contains (x.Code))
                .ToList ();

            if ( newRoles.Count == 0 )
                return;

            await ExecuteWithIdentityInsertAsync (
                context,
                "Roles",
                async () =>
                {
                    await context.Roles.AddRangeAsync (
                       newRoles,
                       cancellationToken
                       );
                    await context.SaveChangesAsync (cancellationToken);
                },
                cancellationToken);
        }
        private static async Task ExecuteWithIdentityInsertAsync (
            DatabaseContext context,
            string tableName,
            Func<Task> action,
            CancellationToken cancellationToken = default
            )
        {
            var strategy = context.Database.CreateExecutionStrategy ();

            await strategy.ExecuteAsync (async () =>
            {
                await context.Database.OpenConnectionAsync (cancellationToken);

                try
                {
                    await context.Database.ExecuteSqlRawAsync (
                        $"SET IDENTITY_INSERT dbo.{tableName} ON",
                        cancellationToken
                        );

                    await action ();

                    await context.Database.ExecuteSqlRawAsync (
                        $"SET IDENTITY_INSERT dbo.{tableName} OFF",
                        cancellationToken
                        );
                }
                finally
                {
                    await context.Database.CloseConnectionAsync ();
                }
            });
        }
    }
}
