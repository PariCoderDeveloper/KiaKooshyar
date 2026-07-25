using KiaKooshar.Domain.Entities.Identity;
using System.Security.Claims;

namespace KiaKooshar.Application.Features.Construct.JWT
{
    public interface IJwtProvider
    {
        string GenerateAccessToken ( User user );
        RefreshToken GenerateRefreshToken ( User user );
        ClaimsPrincipal? GetPrincipalFromExpiredToken ( string token );
    }
}
