using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.User;
using KiaKooshar.Application.DTOs.Identities.Users;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Users.Requests.Commands
{
    public class UpdateUserCommand : IRequest<ResultDTO<ResponseUpdateUserDTO>>
    {
        public RequestUpdateUserDTO UpdateUserDTO { get; set; } = null!;
    }
}
