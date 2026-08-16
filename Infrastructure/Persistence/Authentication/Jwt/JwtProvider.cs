using KiaKooshar.Application.DTOs.Identities.Authentication;
using KiaKooshar.Application.Features.Construct.JWT;
using KiaKooshar.Domain.Entities.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Json;

namespace KiaKooshar.Infrastructure.Persistence.Authentication.Jwt
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtSettings _settings;
        public JwtProvider (
            IOptions<JwtSettings> settings
            )
        {
            _settings = settings.Value;
        }
        public string GenerateAccessToken (
            AuthenticatedUserDTO authenticatedUser
            )
        {
            var authenticatedUserJson =
                JsonSerializer.Serialize (authenticatedUser);
            var claims = new List<Claim>
            {
                new Claim(
                    JwtRegisteredClaimNames.Sub,
                    authenticatedUserJson
                    ),
            };
            var key = new SymmetricSecurityKey (
                 System.Text.Encoding.UTF8.GetBytes (
                    _settings.Key
                )
            );
            var credentials =
                new SigningCredentials (
                key,
                SecurityAlgorithms.HmacSha256
                );
            var token = new JwtSecurityToken (
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes (
                    _settings.AccessTokenExpirationMinutes
                    ),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler ()
                .WriteToken (token);
        }
        public RefreshToken GenerateRefreshToken (
           RefreshTokenRequestDTO refreshTokenRequest
            )
        {
            return new RefreshToken
            {
                Token = Convert.ToBase64String (
                    RandomNumberGenerator.GetBytes (64)
                    ),
                ExpireDate = DateTime.UtcNow.AddDays (
                    _settings.RefreshTokenExpirationDays
                    ),
                AccessToken = refreshTokenRequest.AccessToken,
                UserId = refreshTokenRequest.UserId,
                Device = refreshTokenRequest.Device,
                IP = refreshTokenRequest.Ip,
            };
        }
        public ClaimsPrincipal? GetPrincipalFromExpiredToken ( string token )
        {
            var tokenValidationParameters =
                new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudience = _settings.Audience,
                    ValidIssuer = _settings.Issuer,
                    ValidIssuers = _settings.Issuer.Split (','),
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey (
                        System.Text.Encoding.UTF8.GetBytes (
                            _settings.Key
                            )
                        ),
                    ValidateLifetime = false
                };
            var tokenHandler = new JwtSecurityTokenHandler ();
            var principal = tokenHandler.ValidateToken (
                token,
                tokenValidationParameters,
                out SecurityToken securityToken
                );
            if ( securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals (
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase
                    )
                )
                throw new SecurityTokenException (
                    "Invalid token"
                    );
            return principal;
        }
    }
}
