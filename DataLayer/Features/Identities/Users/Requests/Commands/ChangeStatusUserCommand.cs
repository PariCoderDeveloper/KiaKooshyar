using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Domain.Enums;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Users.Requests.Commands
{
    public class ChangeStatusUserCommand : IRequest<ResultDTO<UserStatus>>
    {
        public long Id { get; set; }
        public UserStatus Status { get; set; }
    }
}
