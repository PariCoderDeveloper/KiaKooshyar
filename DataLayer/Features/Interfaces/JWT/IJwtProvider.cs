using KiaKooshar.Application.DTOs.Identities.Authentication;
using KiaKooshar.Domain.Entities.Identity;
using System.Security.Claims;

namespace KiaKooshar.Application.Features.Construct.JWT
{
    public interface IJwtProvider
    {
        string GenerateAccessToken (
              AuthenticatedUserDTO authenticatedUser
            );
        RefreshToken GenerateRefreshToken (
            RefreshTokenRequestDTO refreshTokenRequest
            );
        ClaimsPrincipal? GetPrincipalFromExpiredToken ( string token );
    }
}
