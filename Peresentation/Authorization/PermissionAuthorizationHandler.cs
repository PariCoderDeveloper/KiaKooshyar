using Microsoft.AspNetCore.Authorization;

namespace KiaKooshar.Peresentation.Authorization
{
    public class PermissionAuthorizationHandler :
        AuthorizationHandler<PermissionRequirement>
    {
        public PermissionAuthorizationHandler (

            )
        {

        }
        protected override Task HandleRequirementAsync (
            AuthorizationHandlerContext context,
            PermissionRequirement requirement
            )
        {

            return Task.CompletedTask;
        }
    }
}
