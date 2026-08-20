using KiaKooshar.Application.DTOs.Common;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands
{
    public class LogoutByRefreshTokenCommand : IRequest<ResultDTO>
    {
        public string? RefreshToken { get; set; }
    }
}
