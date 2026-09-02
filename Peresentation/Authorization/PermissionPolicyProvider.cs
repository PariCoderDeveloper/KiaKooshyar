using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace KiaKooshar.Peresentation.Authorization
{
    public class PermissionPolicyProvider
        : DefaultAuthorizationPolicyProvider
    {

        public PermissionPolicyProvider (
            IOptions<AuthorizationOptions> options
            ) : base (options)
        {
        }
        public override Task<AuthorizationPolicy?> GetPolicyAsync (
            string policyName
            )
        {
            var values = policyName
                .Split ('|', 2);

            var role = values[1];
            var permission = values[0];

            var policy = new AuthorizationPolicyBuilder ()
                .AddRequirements (
                    new PermissionRoleRequirement (role, permission))
                .Build ();

            return Task.FromResult<AuthorizationPolicy?> (policy);
        }
    }
}
