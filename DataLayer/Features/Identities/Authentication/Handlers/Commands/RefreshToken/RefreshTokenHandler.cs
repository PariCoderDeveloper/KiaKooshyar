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
                    request.RefreshToken,
                    cancellationToken
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

            var userSession = await _unit.UserSessions
                .GetUserSessionByRefreshTokenId (
                    refreshToken.Id,
                    cancellationToken
                    );
            if ( userSession is null )
                return ResultDTO<ResponseRefreshTokenDTO>.Unauthorized
                   (
                       "Invalid user session"
                   );
            if ( !userSession.IsActive )
                return ResultDTO<ResponseRefreshTokenDTO>.Unauthorized
                   (
                       "Invalid user session"
                   );
            if ( userSession.LogoutTime is not null )
                return ResultDTO<ResponseRefreshTokenDTO>.Unauthorized
                   (
                       "Invalid user session"
                   );
            if ( userSession.IsDeleted )
                return ResultDTO<ResponseRefreshTokenDTO>.Unauthorized
                   (
                       "Invalid user session"
                   );

            refreshToken.UpdatedAt = DateTime.UtcNow;
            var accessToken = _jwtProvider.GenerateAccessToken
                (
                   refreshToken.UserId
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
