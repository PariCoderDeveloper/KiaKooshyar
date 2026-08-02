using KiaKooshar.Application.Specifications.Base;
using KiaKooshar.Domain.Entities.Identity;

namespace KiaKooshar.Application.Specifications
{
    public class GetPermissionSpecification :
        Specification<Permission>
    {
        public GetPermissionSpecification ( string key )
        {
            AddCriteria (x => x.DiplayName == key);
        }
    }
}
