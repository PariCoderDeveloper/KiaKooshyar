using KiaKooshar.Application.Specifications.Base;
using KiaKooshar.Domain.Entities.Identity;

namespace KiaKooshar.Application.Specifications.Identities.Authentication
{
    public class LogoutSpecification :
        Specification<RefreshToken>
    {
        public LogoutSpecification ( string refreshToken )
        {
            AddCriteria (x => x.Token == refreshToken);
        }
    }
}
