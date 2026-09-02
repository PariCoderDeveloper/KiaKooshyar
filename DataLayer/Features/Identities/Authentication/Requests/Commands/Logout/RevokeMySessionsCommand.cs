using KiaKooshar.Application.DTOs.Common;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands.Authentication.Logout
{
    public class RevokeMySessionsCommand
        : IRequest<ResultDTO>
    {
        public long sessionId { get; set; }
        public long userId { get; set; }
    }
}
