using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Features.Identities.Users.Requests.Commands;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Users.Handlers.Commands
{
    public class ChangeEmailHandler :
        IRequestHandler<ChangeEmailCommand, ResultDTO>
    {
        private readonly IUnitOfWork _unit;
        public ChangeEmailHandler (
            IUnitOfWork unit
            )
        {
            _unit = unit;
        }
        public async Task<ResultDTO> Handle ( ChangeEmailCommand request, CancellationToken cancellationToken )
        {
            var user = await _unit.User.FirstOrDefaultAsync (request.Id);
            if ( user is null )
                return ResultDTO.NotFound ("User not found");

            user.Email = request.Email;
            user.UpdatedAt = DateTime.UtcNow;

            await _unit.CommitAsync ();

            return ResultDTO.Success (
                "The email of user changed successfully"
              );
        }
    }
}
