using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.Authentication;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands
{
    public class LoginCommand : IRequest<ResultDTO<LoginResponseDTO>>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
