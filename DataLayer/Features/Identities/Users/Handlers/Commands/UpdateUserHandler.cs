using AutoMapper;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.Users.Commands;
using KiaKooshar.Application.Features.Identities.Users.Requests.Commands;
using KiaKooshar.Application.Specifications.Identities.Users;
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
            var specification = new UserByIdSpecification (request.UpdateUserDTO.Id);

            var user = await _unit.User.FirstOrDefaultAsync (
                specification,
                cancellationToken
                );
            if ( user is null )
                return ResultDTO<UpdateUserDTO>.NotFound ("User not found");
            user.UpdatedAt = DateTime.UtcNow;
            _mapper.Map (request.UpdateUserDTO, user);
            await _unit.CommitAsync ();
            return ResultDTO<UpdateUserDTO>.Success (
                null,
                "User updated successfully"
                );
        }
    }
}
