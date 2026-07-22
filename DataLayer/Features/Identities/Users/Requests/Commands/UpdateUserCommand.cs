using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.Users.Commands;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Users.Requests.Commands
{
    public class UpdateUserCommand : IRequest<ResultDTO<UpdateUserDTO>>
    {
        public UpdateUserDTO UpdateUserDTO { get; set; } = null!;
    }
}
