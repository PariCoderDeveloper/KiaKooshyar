using KiaKooshar.Application.DTOs.Common;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands
{
    public class LogoutCommand : IRequest<ResultDTO>
    {
        public long Id { get; set; }
        public string RefreshToken { get; set; } = null!;
    }
}
