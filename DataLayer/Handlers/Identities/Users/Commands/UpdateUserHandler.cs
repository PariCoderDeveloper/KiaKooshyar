using AutoMapper;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.User;
using KiaKooshar.Application.Requests.Identities.User.Commands;
using MediatR;

namespace KiaKooshar.Application.Handlers.Identities.Users.Commands
{
    public class UpdateUserHandler :
        IRequestHandler<UpdateUserCommand, ResultDTO<UpdateUserDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unit;
        public UpdateUserHandler (
            IMapper mapper,
            IUnitOfWork unit
            )
        {
            _mapper = mapper;
            _unit = unit;
        }
        public async Task<ResultDTO<UpdateUserDTO>> Handle (
            UpdateUserCommand request,
            CancellationToken cancellationToken
            )
        {
            var user = await _unit.User.GetByIdAsync (request.Id);
            if ( user == null )
            {
                return ResultDTO<UpdateUserDTO>.NotFound ("User not found");
            }

            _mapper.Map (request, user);
            await _unit.CommitAsync ();

            var result = _mapper.Map<UpdateUserDTO> (user);
            return ResultDTO<UpdateUserDTO>.Success (
                result,
                "User updated successfully"
            );
        }
    }
}
