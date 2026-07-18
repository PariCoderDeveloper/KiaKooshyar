using AutoMapper;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Requests.Identities.User.Commands;
using KiaKooshar.Domain.Enums;
using MediatR;

namespace KiaKooshar.Application.Handlers.Identities.User.Commands
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
            var user = await _unit.User.GetByIdAsync (request.Id);
            if ( user == null )
            {
                ResultDTO.NotFound ("User not found");
            }
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
            _mapper.Map<Domain.Entities.Identity.User> (user);
            await _unit.CommitAsync ();
            return ResultDTO<UserStatus>.Success (
                user.Status,
                "The status of user changes successfully"
                );
        }
    }
}
