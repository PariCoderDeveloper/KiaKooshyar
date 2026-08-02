using KiaKooshar.Application.Specifications.Base;
using KiaKooshar.Domain.Constants;
using KiaKooshar.Domain.Entities.Identity;

namespace KiaKooshar.Application.Specifications.Identities.Authentication
{
    public class DefaultRoleSpecification
        : Specification<Role>
    {
        public DefaultRoleSpecification ()
        {
            AddCriteria (x => x.Name == Roles.User);
        }
    }
}
