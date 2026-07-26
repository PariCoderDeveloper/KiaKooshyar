using KiaKooshar.Application.Specifications.Base;

namespace KiaKooshar.Application.Specifications.Identities.Authentication
{
    public class GetByEmailSpecification :
        Specification<Domain.Entities.Identity.User>
    {
        public GetByEmailSpecification ( string email )
        {
            AddCriteria (x => x.Email == email);
        }
    }
}
