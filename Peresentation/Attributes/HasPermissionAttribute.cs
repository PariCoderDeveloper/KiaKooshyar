using Microsoft.AspNetCore.Authorization;

namespace KiaKooshar.Peresentation.Attributes
{
    public sealed class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute ( string permission, string role ) :
            base (policy: $"{permission}|{role}")
        {
        }
    }
}
