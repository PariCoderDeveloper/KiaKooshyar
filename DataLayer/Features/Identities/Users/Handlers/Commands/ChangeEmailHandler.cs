using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.Users.Queries;
using KiaKooshar.Application.Features.Identities.Users.Requests.Commands;
using KiaKooshar.Application.Specifications.Identities.Users;
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
            var specification = new UserByIdSpecification (request.Id);

            var user = await _unit.User.FirstOrDefaultAsync (
                specification,
                cancellationToken
                );
            if ( user != null )
                return ResultDTO<GetUserByIdDTO>.NotFound ("User not found");

            user.Email = request.Email;
            user.UpdatedAt = DateTime.UtcNow;

            await _unit.CommitAsync ();

            return ResultDTO.Success (
                "The email of user changed successfully"
              );
        }
    }
}
