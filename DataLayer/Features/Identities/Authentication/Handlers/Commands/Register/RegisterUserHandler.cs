using AutoMapper;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.Construct.Security;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Commons;
using KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Authentication.Handlers.Commands.Register
{
    public class RegisterUserHandler
        : IRequestHandler<RegisterUserCommand, ResultDTO<ReturnUserDTO>>
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
        public async Task<ResultDTO<ReturnUserDTO>> Handle
            (
            RegisterUserCommand request,
            CancellationToken cancellationToken
            )
        {

            var user = _mapper.Map<Domain.Entities.Identity.User> (request.RegisterUserDTO);

            user.PasswordHash = _passwordHasher.HashPassword (request.RegisterUserDTO.PasswordHash);

            _unit.User.AddAsync (user);
            var result = await _unit.CommitAsync ();

            return ResultDTO<ReturnUserDTO>.Success (

                new ReturnUserDTO
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Gender = user.Gender,
                    Status = user.Status,
                });
        }
    }
}
