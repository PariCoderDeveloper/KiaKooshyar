using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Features.Identities.Users.Requests.Commands;
using KiaKooshar.Application.Features.Interfaces.Repositories;
using KiaKooshar.Domain.Enums;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Users.Handlers.Commands
{
    public class ChangeStatusUserHandler :
        IRequestHandler<ChangeStatusUserCommand, ResultDTO<UserStatus>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IUserRepository _userRepository;
        public ChangeStatusUserHandler (
            IUnitOfWork unit,
            IUserRepository userRepository
            )
        {
            _unit = unit;
            _userRepository = userRepository;
        }
        public async Task<ResultDTO<UserStatus>> Handle (
            ChangeStatusUserCommand request,
            CancellationToken cancellationToken
            )
        {
            var user = await _userRepository.GetByIdAsync (request.Id);
            if ( user is null )
                return ResultDTO<UserStatus>.NotFound ("User not found");
            user.Status = request.Status;
            user.UpdatedAt = DateTime.UtcNow;
            await _unit.CommitAsync ();
            return ResultDTO<UserStatus>.Success (
                user.Status,
                "The status of user changes successfully"
            );
        }
    }
}
