using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Commons;
using KiaKooshar.Application.DTOs.Identities.Users.Commands;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands.Authentication.Register
{
    public class RegisterUserCommand :
        IRequest<ResultDTO<ReturnUserDTO>>
    {
        public RegisterUserDTO RegisterUserDTO { get; set; } = null!;
    }
}
