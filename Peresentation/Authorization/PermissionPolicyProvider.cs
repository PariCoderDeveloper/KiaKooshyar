using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace KiaKooshar.Peresentation.Authorization
{
    public class PermissionPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        public PermissionPolicyProvider (
            IOptions<AuthorizationOptions> options
            ) : base (options)
        {
        }
        public override async Task<AuthorizationPolicy?> GetPolicyAsync (
            string policyName
            )
        {
            var policy = await base.GetPolicyAsync (policyName);
            if ( policy != null )
                return policy;
            return new AuthorizationPolicyBuilder ()
                .AddRequirements (
                 new PermissionRequirement (policyName)
                ).Build ();
        }
    }
}
