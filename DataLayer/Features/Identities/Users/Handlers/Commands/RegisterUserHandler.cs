using AutoMapper;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands.Authentication.Register;
using KiaKooshar.Domain.Entities.Identity;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Users.Handlers.Commands
{
    public class RegisterUserHandler :
        IRequestHandler<RegisterUserCommand, ResultDTO>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public RegisterUserHandler (
            IUnitOfWork unit,
            IMapper mapper
            )
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<ResultDTO> Handle (
            RegisterUserCommand request,
            CancellationToken cancellationToken
            )
        {
            var user = _mapper.Map<User> (request.RegisterUserDTO);
            var addedResult = await _unit.Users.AddAsync
                (user, cancellationToken);
            if ( addedResult is null )
                return ResultDTO.NotFound ("User couldn't add");
            return ResultDTO.Success ("User added successfully");
        }
    }
}
