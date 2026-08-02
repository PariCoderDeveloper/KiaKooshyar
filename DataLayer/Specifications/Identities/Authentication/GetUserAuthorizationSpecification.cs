using KiaKooshar.Application.Specifications.Base;
using KiaKooshar.Domain.Entities.Identity;

namespace KiaKooshar.Application.Specifications.Identities.Authentication
{
    public class GetUserAuthorizationSpecification :
        Specification<User>
    {
        public GetUserAuthorizationSpecification ( long userId )
        {
            AddCriteria (x => x.Id == userId);
            AddIncludeString (
                        "UserRole.Role.RolePermission.Permission"
                    );
        }
    }
}
