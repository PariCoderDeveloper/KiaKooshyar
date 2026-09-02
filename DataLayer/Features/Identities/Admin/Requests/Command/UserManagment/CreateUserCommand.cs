using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.Users.Commands;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Admin.Requests.Command.UserManagment
{
    public class CreateUserCommand :
        IRequest<ResultDTO>
    {
        public List<long> Permissions { get; set; } = new ();
        public List<long> Roles { get; set; } = new ();
        public RegisterUserDTO UserInformation { get; set; } = null!;
    }
}
