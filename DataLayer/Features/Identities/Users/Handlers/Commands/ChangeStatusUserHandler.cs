using AutoMapper;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Features.Identities.Users.Requests.Commands;
using KiaKooshar.Application.Specifications.Identities.Users;
using KiaKooshar.Domain.Enums;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Users.Handlers.Commands
{
    public class ChangeStatusUserHandler :
        IRequestHandler<ChangeStatusUserCommand, ResultDTO<UserStatus>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unit;
        public ChangeStatusUserHandler (
            IMapper mapper,
            IUnitOfWork unit
            )
        {
            _mapper = mapper;
            _unit = unit;
        }
        public async Task<ResultDTO<UserStatus>> Handle (
            ChangeStatusUserCommand request,
            CancellationToken cancellationToken
            )
        {
            var specification = new UserByIdSpecification (request.Id);

            var user = await _unit.User.FirstOrDefaultAsync (
                specification,
                cancellationToken
                );
            if ( user is null )
                return ResultDTO<UserStatus>.NotFound ("User not found");
            switch ( request.Status )
            {
                case UserStatus.Pending:
                    user.Status = UserStatus.Pending;
                    break;
                case UserStatus.Active:
                    user.Status = UserStatus.Active;
                    break;
                case UserStatus.Inactive:
                    user.Status = UserStatus.Inactive;
                    break;
                case UserStatus.Suspended:
                    user.Status = UserStatus.Suspended;
                    break;
                case UserStatus.Locked:
                    user.Status = UserStatus.Locked;
                    break;
                default:
                    return ResultDTO<UserStatus>.ValidationError ("Validating Errors Failed");
            }
            user.UpdatedAt = DateTime.UtcNow;

            await _unit.CommitAsync ();
            return ResultDTO<UserStatus>.Success (
                user.Status,
                "The status of user changes successfully"
              );
        }
    }
}
