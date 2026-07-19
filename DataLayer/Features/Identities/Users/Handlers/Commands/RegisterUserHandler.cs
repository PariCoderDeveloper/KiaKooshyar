using AutoMapper;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.Construct.Security;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Features.Identities.Users.Requests.Commands;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Users.Handlers.Commands
{
    public class RegisterUserHandler
        : IRequestHandler<RegisterUserCommand, ResultDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unit;
        private readonly IPasswordHasher _passwordHasher;
        public RegisterUserHandler (
            IUnitOfWork unit,
            IPasswordHasher passwordHasher,
            IMapper mapper
            )
        {
            _mapper = mapper;
            _unit = unit;
            _passwordHasher = passwordHasher;
        }
        public async Task<ResultDTO> Handle
            (
            RegisterUserCommand request,
            CancellationToken cancellationToken
            )
        {

            var user = _mapper.Map<Domain.Entities.Identity.User> (request.RegisterUserDTO);

            user.PasswordHash = _passwordHasher.HashPassword (request.RegisterUserDTO.PasswordHash);

            _unit.User.AddAsync (user);
            await _unit.CommitAsync ();

            return ResultDTO.Success ("User added successfully");
        }
    }
}
