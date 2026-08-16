using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.Authentication;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands
{
    public class RefreshTokenCommand :
        IRequest<ResultDTO<ResponseRefreshTokenDTO>>
    {
        public string RefreshToken { get; set; } = null!;
    }
}
