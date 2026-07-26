using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.Construct.Security;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.Users.Queries;
using KiaKooshar.Application.Features.Identities.Users.Requests.Commands;
using KiaKooshar.Application.Specifications.Identities.Users;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Users.Handlers.Commands
{
    public class ChangePasswordHandler :
        IRequestHandler<ChangePasswordCommand, ResultDTO>
    {
        private readonly IUnitOfWork _unit;
        private readonly IPasswordHasher _passwordHasher;
        public ChangePasswordHandler (
            IUnitOfWork unit,
            IPasswordHasher passwordHasher
            )
        {
            _unit = unit;
            _passwordHasher = passwordHasher;
        }
        public async Task<ResultDTO> Handle (
            ChangePasswordCommand request,
            CancellationToken cancellationToken
            )
        {
            var specification = new UserByIdSpecification (request.Id);

            var user = await _unit.User.FirstOrDefaultAsync (
                specification,
                cancellationToken
                );
            if ( user != null )
                return ResultDTO<GetUserByIdDTO>.NotFound ("User not found");

            user.PasswordHash = _passwordHasher.HashPassword (request.Password);
            user.UpdatedAt = DateTime.UtcNow;
            await _unit.CommitAsync ();
            return ResultDTO.Success (
                "The password of user changed successfully"
              );
        }
    }
}
