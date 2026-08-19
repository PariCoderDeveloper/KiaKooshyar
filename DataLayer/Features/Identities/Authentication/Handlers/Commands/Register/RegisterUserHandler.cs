using AutoMapper;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.Construct.Security;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Commons;
using KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands;
using KiaKooshar.Application.Features.Interfaces.Repositories;
using KiaKooshar.Domain.Entities.Identity;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Authentication.Handlers.Commands.Register
{
    public class RegisterUserHandler
        : IRequestHandler<RegisterUserCommand, ResultDTO<ReturnUserDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unit;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        public RegisterUserHandler (
            IPasswordHasher passwordHasher,
            IMapper mapper,
            IUserRepository userRepository,
            IUnitOfWork unit,
            IUserRoleRepository userRoleRepository,
            IRoleRepository roleRepository
            )
        {
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _unit = unit;
            _roleRepository = roleRepository;
        }
        public async Task<ResultDTO<ReturnUserDTO>> Handle
            (
            RegisterUserCommand request,
            CancellationToken cancellationToken
            )
        {
            var user = _mapper.Map<Domain.Entities.Identity.User> (
                request.RegisterUserDTO
                );
            user.PasswordHash = _passwordHasher.HashPassword (
                request.RegisterUserDTO.Password);
            user.IsEmailConfirmed = false;
            user.Status = Domain.Enums.UserStatus.Active;
            await _userRepository.AddAsync (
                user,
                cancellationToken
                );
            var role = await _roleRepository.GetByIdAsync (
                4, cancellationToken);
            UserRole userRole = new UserRole
            {
                User = user,
                Role = role!,
            };
            await _userRoleRepository.AddAsync (
                 userRole,
                 cancellationToken
                 );
            await _unit.CommitAsync (cancellationToken);
            return ResultDTO<ReturnUserDTO>.Success (
                new ReturnUserDTO
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Gender = user.Gender,
                    Status = user.Status,
                },
                "Registration successful. You have been assigned the default 'User' role."
            );
        }
    }
}
