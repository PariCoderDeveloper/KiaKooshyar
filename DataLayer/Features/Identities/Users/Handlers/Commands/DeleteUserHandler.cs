using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.Users.Queries;
using KiaKooshar.Application.Features.Identities.Users.Requests.Commands;
using KiaKooshar.Application.Specifications.Identities.Users;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Users.Handlers.Commands
{

    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, ResultDTO>
    {
        private readonly IUnitOfWork _unit;
        public DeleteUserHandler (
            IUnitOfWork unit
            )
        {
            _unit = unit;
        }
        public async Task<ResultDTO> Handle (
            DeleteUserCommand request,
            CancellationToken cancellationToken
            )
        {
            var specification = new UserByIdSpecification (request.Id);

            var user = await _unit.User.FirstOrDefaultAsync (
                specification,
                cancellationToken
                );
            if ( user is null )
                return ResultDTO<GetUserByIdDTO>.NotFound ("User not found");

            _unit.User.Delete (user);
            user.UpdatedAt = DateTime.UtcNow;

            await _unit.CommitAsync ();
            return ResultDTO.Success ("User deleted successfully");
        }
    }
}

