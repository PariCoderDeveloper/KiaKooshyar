using AutoMapper;
using KiaKooshar.Application.Caching.Contracts;
using KiaKooshar.Application.Caching.Models;
using KiaKooshar.Application.Caching.Policies;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.Construct.Security;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.Authentication;
using KiaKooshar.Application.DTOs.Identities.Users.Queries;
using KiaKooshar.Application.Features.Construct.JWT;
using KiaKooshar.Application.Features.Construct.Logging;
using KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands.Authentication.Login;
using KiaKooshar.Application.Features.Interfaces.HttpContext;
using KiaKooshar.Application.Features.Interfaces.Repositories;
using KiaKooshar.Application.Logging;
using MediatR;
using System.Text.Json;

namespace KiaKooshar.Application.Features.Identities.Authentication.Handlers.Commands.Login
{
    public class LoginHandler :
        IRequestHandler<LoginCommand, ResultDTO<LoginResponseDTO>>
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IUnitOfWork _unit;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly IBaseLogger _logger;
        private readonly IRequestContext _requestContext;
        private readonly IUserSessionRepository _userSession;
        public LoginHandler (
            IJwtProvider jwtProvider,
            IUnitOfWork unit,
            IPasswordHasher passwordHasher,
            IMapper mapper,
            ICacheService cache,
            IBaseLogger logger,
            IRequestContext requestContext,
            IUserSessionRepository userSession
            )
        {
            _jwtProvider = jwtProvider;
            _unit = unit;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _cache = cache;
            _logger = logger;
            _requestContext = requestContext;
            _userSession = userSession;
        }
        public async Task<ResultDTO<LoginResponseDTO>> Handle (
            LoginCommand request,
            CancellationToken cancellationToken
            )
        {
            var user = await _unit.Users.GetUserByEmail (
                request.Email,
                cancellationToken
                );
            if ( user is null )
                return ResultDTO<LoginResponseDTO>.NotFound
                    (
                        "Invalid email or password"
                    );
            var isCorrect = _passwordHasher.VerifyPassword (
                    user.PasswordHash,
                    request.Password
            );
            if ( !isCorrect )
                return ResultDTO<LoginResponseDTO>.Unauthorized (
                    "Invalid email or password"
                    );
            var userPermission = await _unit.Users.GetUserPermissions
                (
                    user.Id
                );
            var roleNames = await _unit.Users.GetUserRoles
                (
                    user.Id
                );
            var cacheModel = new UserAuthorizationCacheModel
            {
                UserId = user.Id,
                Email = user.Email,
                Username = user.UserName,
                Roles = roleNames,
                Permissions = userPermission
            };
            await _cache.SetAsync (
                CacheKeys.User (user.Id),
                JsonSerializer.Serialize (cacheModel),
                CachePolicy.Long
            );
            AuthLogExtensions.LogUserLogin (
                _logger,
                user,
                true,
                _requestContext.Device,
                _requestContext.IpAddress
                );
            var accessToken = _jwtProvider.GenerateAccessToken (
               user.Id
              );
            var refreshToken = _jwtProvider.GenerateRefreshToken (
                new RefreshTokenRequestDTO
                {
                    Device = _requestContext.Device,
                    Ip = _requestContext.IpAddress,
                    UserId = user.Id,
                    AccessToken = accessToken
                }
              );
            await _unit.RefreshToken.AddAsync (refreshToken);
            var userSession = new Domain.Entities.Identity.UserSession
            {
                Device = _requestContext.Device,
                Browser = _requestContext.Browser,
                IP = _requestContext.IpAddress,
                OS = _requestContext.OS,
                LoginTime = DateTime.UtcNow,
                LastActivity = DateTime.UtcNow,
                RefreshToken = refreshToken,
                User = user
            };
            _userSession.AddAsync (userSession);
            await _unit.CommitAsync (cancellationToken);
            return ResultDTO<LoginResponseDTO>.Success (
                new LoginResponseDTO
                {
                    AccessToken = refreshToken.AccessToken,
                    RefreshToken = refreshToken.Token,
                    AccessTokenExpiration = DateTime.UtcNow.AddMinutes (15),
                    RefreshTokenExpiration = DateTime.UtcNow.AddDays (7),
                    User = new UserInfoDTO
                    {
                        Id = user.Id,
                        Username = user.UserName,
                    }
                },
                "Login successful"
                );
        }
    }
}
