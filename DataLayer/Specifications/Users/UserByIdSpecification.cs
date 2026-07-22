using KiaKooshar.Application.Specifications.Base;

namespace KiaKooshar.Application.Specifications.Users
{
    public class UserByIdSpecification : Specification<Domain.Entities.Identity.User>
    {
        public UserByIdSpecification ( long id )
        {
            AddCriteria (x => x.Id == id);
        }
    }
}
