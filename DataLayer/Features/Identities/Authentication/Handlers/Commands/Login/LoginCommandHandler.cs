using AutoMapper;
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
    public class LoginCommandHandler :
        IRequestHandler<LoginCommand, ResultDTO<LoginResponseDTO>>
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IUnitOfWork _unit;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        public LoginCommandHandler (
            IJwtProvider jwtProvider,
            IUnitOfWork unit,
            IPasswordHasher passwordHasher,
            IMapper mapper
            )
        {
            _jwtProvider = jwtProvider;
            _unit = unit;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
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
            var userRoles = await _unit.UserRoles.ListAsync (
                rolesSpecification,
                cancellationToken
                );
            var roles = _mapper.Map<List<RoleDTO>> (userRoles);
            var roleNames = roles
                .Select (x => x.Name)
                .ToList ();
            return ResultDTO<LoginResponseDTO>.Success (
                new LoginResponseDTO
                {
                    AccessToken = _jwtProvider.GenerateAccessToken (
                        new AuthenticatedUserDTO
                        {
                            Roles = roleNames,
                            User = user,
                        }
                        ),
                    AccessTokenExpiration = DateTime.UtcNow.AddMinutes (15),
                    User = new UserInfoDTO
                    {
                        Id = user.Id,
                        Username = user.UserName,
                        Roles = roles
                    }
                },
                "Login successful"
                );
        }
    }
}
