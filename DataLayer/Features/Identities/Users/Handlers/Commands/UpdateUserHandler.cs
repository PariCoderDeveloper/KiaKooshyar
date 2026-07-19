using AutoMapper;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.Users;
using KiaKooshar.Application.Features.Identities.Users.Requests.Commands;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Users.Handlers.Commands
{
    public class UpdateUserHandler :
        IRequestHandler<UpdateUserCommand, ResultDTO<ResponseUpdateUserDTO>>
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
        public async Task<ResultDTO<ResponseUpdateUserDTO>> Handle (
            UpdateUserCommand request,
            CancellationToken cancellationToken
            )
        {
            var user = await _unit.User.GetByIdAsync (request.UpdateUserDTO.Id);
            if ( user == null )
            {
                return ResultDTO<ResponseUpdateUserDTO>.NotFound ("User not found");
            }

            _mapper.Map (request, user);
            user.UpdatedAt = DateTime.UtcNow;

            await _unit.CommitAsync ();

            var result = _mapper.Map<ResponseUpdateUserDTO> (user);

            return ResultDTO<ResponseUpdateUserDTO>.Success (
                result,
                "User updated successfully"
            );
        }
    }
}
