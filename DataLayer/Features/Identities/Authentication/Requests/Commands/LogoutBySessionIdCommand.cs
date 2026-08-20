using KiaKooshar.Application.DTOs.Common;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands
{
    public class LogoutBySessionIdCommand :
        IRequest<ResultDTO>
    {
        public long sessionId { get; set; }
    }
}
