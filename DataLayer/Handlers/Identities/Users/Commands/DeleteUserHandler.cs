using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Requests.Identities.User.Commands;
using MediatR;

namespace KiaKooshar.Application.Handlers.Identities.Users.Commands
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
            var user = await _unit.User.GetByIdAsync (request.Id);
            if ( user == null )
            {
                return ResultDTO.NotFound ("User doesnt found");
            }
            _unit.User.Delete<Domain.Entities.Identity.User> (user);
            await _unit.CommitAsync ();
            return ResultDTO.Success ("");
        }
    }
}

