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

            await context.Permissions.AddRangeAsync (
                newPermissions,
                cancellationToken);

            await context.SaveChangesAsync (cancellationToken);
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

            await context.Roles.AddRangeAsync (
                newRoles,
                cancellationToken
                );

            await context.SaveChangesAsync (cancellationToken);
        }
    }
}
