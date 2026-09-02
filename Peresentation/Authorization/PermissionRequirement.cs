using Microsoft.AspNetCore.Authorization;

namespace KiaKooshar.Peresentation.Authorization
{
    public class PermissionRoleRequirement
        : IAuthorizationRequirement
    {
        public string? Permission { get; } = null!;
        public string? RequiredRole { get; set; }
        public PermissionRoleRequirement (
            string? permission,
            string? requiredRole
            )
        {
            Permission = permission;
            RequiredRole = requiredRole;
        }
    }
}
