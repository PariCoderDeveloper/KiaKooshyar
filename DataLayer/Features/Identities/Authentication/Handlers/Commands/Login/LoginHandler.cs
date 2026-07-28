using AutoMapper;
using KiaKooshar.Application.Caching.Contracts;
using KiaKooshar.Application.Caching.Policies;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.Construct.Security;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.Authentication;
using KiaKooshar.Application.DTOs.Identities.Users.Queries;
using KiaKooshar.Application.Features.Construct.JWT;
using KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands;
using KiaKooshar.Application.Specifications.Identities.Authentication;
using MediatR;

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
        public LoginHandler (
            IJwtProvider jwtProvider,
            IUnitOfWork unit,
            IPasswordHasher passwordHasher,
            IMapper mapper,
            ICacheService cache
            )
        {
            _jwtProvider = jwtProvider;
            _unit = unit;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _cache = cache;
        }
        public async Task<ResultDTO<LoginResponseDTO>> Handle (
            LoginCommand request,
            CancellationToken cancellationToken
            )
        {
            var emailSpecification = new GetByEmailSpecification (request.Email);
            var user = await _unit.User.FirstOrDefaultAsync (
                emailSpecification,
                cancellationToken
                );
            if ( user == null )
                return ResultDTO<LoginResponseDTO>.NotFound (
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
            var rolesSpecification = new GetUserRolesSpecification (user.Id);
            var userPermissions = await _unit.UserRoles.ListAsync (
                rolesSpecification,
                cancellationToken
                );
            var permissions = _mapper.Map<List<PermissionDTO>> (userPermissions);
            var permissionsNames = permissions
                .Select (x => x.Name)
                .ToList ();
            await _cache.SetAsync (
                CacheKeys.UserPermissions (user.Id),
                permissionsNames,
                CachePolicy.Long
            );
            return ResultDTO<LoginResponseDTO>.Success (
                new LoginResponseDTO
                {
                    AccessToken = _jwtProvider.GenerateAccessToken (
                        new AuthenticatedUserDTO
                        {
                            User = user,
                        }
                        ),
                    AccessTokenExpiration = DateTime.UtcNow.AddMinutes (15),
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
