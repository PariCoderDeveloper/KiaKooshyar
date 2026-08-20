using AutoMapper;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.Authentication;
using KiaKooshar.Application.Features.Construct.JWT;
using KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands.Authentication.RefreshToken;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Authentication.Handlers.Commands.RefreshToken
{
    public class RefreshTokenHandler :
        IRequestHandler<RefreshTokenCommand, ResultDTO<ResponseRefreshTokenDTO>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IJwtProvider _jwtProvider;
        private readonly IMapper _mapper;
        public RefreshTokenHandler (
            IUnitOfWork unit,
            IJwtProvider jwtProvider,
            IMapper mapper
            )
        {
            _mapper = mapper;
            _unit = unit;
            _jwtProvider = jwtProvider;
        }
        public async Task<ResultDTO<ResponseRefreshTokenDTO>> Handle (
            RefreshTokenCommand request,
            CancellationToken cancellationToken
            )
        {
            var refreshToken = await _unit.RefreshToken.FindByTokenAsync
                (
                    request.RefreshToken
                );
            if ( refreshToken is null )
                return ResultDTO<ResponseRefreshTokenDTO>.Unauthorized
                    (
                        "Invalid refresh token"
                    );
            if ( refreshToken.IsRevoked )
                return ResultDTO<ResponseRefreshTokenDTO>.Unauthorized
                    (
                        "Token is revoked"
                    );
            if ( refreshToken.ExpireDate <= DateTime.UtcNow )
                return ResultDTO<ResponseRefreshTokenDTO>.Unauthorized
                    (
                        "Refresh token expired"
                    );
            refreshToken.UpdatedAt = DateTime.UtcNow;
            var user = await _unit.Users.GetByIdAsync
                (
                    refreshToken.UserId
                );
            if ( user is null )
                return ResultDTO<ResponseRefreshTokenDTO>.NotFound ("User does not found");
            var accessToken = _jwtProvider.GenerateAccessToken
                (
                   user.Id
                );
            refreshToken.AccessToken = accessToken;
            await _unit.CommitAsync ();
            return ResultDTO<ResponseRefreshTokenDTO>.Success (
                new ResponseRefreshTokenDTO
                {
                    AccessToken = accessToken,
                    AccessTokenExpiration = DateTime.UtcNow.AddMinutes (15)
                }
            );
        }
    }
}
