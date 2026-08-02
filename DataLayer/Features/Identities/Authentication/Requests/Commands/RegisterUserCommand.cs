using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Commons;
using KiaKooshar.Application.DTOs.Identities.Users.Commands;
using KiaKooshar.Domain.Entities.Identity;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands
{
    public class RegisterUserCommand : IRequest<ResultDTO<ReturnUserDTO>>
    {
        public RegisterUserDTO RegisterUserDTO { get; set; } = null!;
        public ICollection<UserRole> Roles { get; set; } = null!;
    }
}
