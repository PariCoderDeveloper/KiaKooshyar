using Microsoft.AspNetCore.Authorization;

namespace KiaKooshar.Peresentation.Attributes
{
    public sealed class PermissionAttribute : AuthorizeAttribute
    {
        public PermissionAttribute ( string permission )
        {
            Policy = permission;
        }
    }
}
