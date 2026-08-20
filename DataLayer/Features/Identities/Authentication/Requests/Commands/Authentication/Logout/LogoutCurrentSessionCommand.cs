using KiaKooshar.Application.DTOs.Common;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands.Authentication.Logout
{
    public class LogoutCurrentSessionCommand : IRequest<ResultDTO>
    {
        public string? RefreshToken { get; set; }
    }
}
