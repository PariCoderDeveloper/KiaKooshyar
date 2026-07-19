using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.Users.Request.Commands;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Users.Requests.Commands
{
    public class RegisterUserCommand : IRequest<ResultDTO>
    {
        public RegisterUserDTO RegisterUserDTO { get; set; } = null!;


    }
}
