using KiaKooshar.Application.DTOs.Common;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Users.Requests.Commands
{
    public class DeleteUserCommand : IRequest<ResultDTO>
    {
        public long Id { get; set; }
    }
}