using Microsoft.AspNetCore.Authorization;

namespace KiaKooshar.Peresentation.Authorization
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; } = null!;
        public PermissionRequirement ( string permission )
        {
            Permission = permission;
        }
    }
}
