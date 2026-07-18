using KiaKooshar.Application.DTOs.Common;
using MediatR;

namespace KiaKooshar.Application.Requests.Identities.User.Commands
{
    public class DeleteUserCommand : IRequest<ResultDTO>
    {
        public long Id { get; set; }
    }
}
