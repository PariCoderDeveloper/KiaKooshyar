using KiaKooshar.Application.Specifications.Base;
using KiaKooshar.Domain.Entities.Identity;

namespace KiaKooshar.Application.Specifications.Identities.Authentication
{
    public class GetUserRolesSpecification :
        Specification<UserRole>
    {
        public GetUserRolesSpecification ( long userId )
        {
            AddCriteria (x => x.UserId == userId);
            AddInclude (x => x.Role);
        }
    }
}
