using KiaKooshar.Application.DTOs.Common;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands.Authentication.RevokeToken
{
    public class RevokeTokenCommand :
        IRequest<ResultDTO>
    {
        public string? RefreshToken { get; set; }
    }
}
