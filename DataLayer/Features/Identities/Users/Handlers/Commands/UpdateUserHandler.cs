using AutoMapper;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.Users.Commands;
using KiaKooshar.Application.Features.Identities.Users.Requests.Commands;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Users.Handlers.Commands
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
            var user = await _unit.Users.GetByIdAsync (
                request.UpdateUserDTO.Id,
                cancellationToken
                );
            if ( user is null )
                return ResultDTO<UpdateUserDTO>.NotFound ("User not found");
            _mapper.Map (request.UpdateUserDTO, user);
            user.UpdatedAt = DateTime.UtcNow;
            await _unit.CommitAsync (cancellationToken);
            var resultDto = _mapper.Map<UpdateUserDTO> (user);
            return ResultDTO<UpdateUserDTO>.Success (
                resultDto,
                "User updated successfully"
                );
        }
    }
}