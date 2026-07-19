using KiaKooshar.Application.DTOs.Common;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Users.Requests.Commands
{
    public class ChangeEmailCommand : IRequest<ResultDTO>
    {
        public long Id { get; set; }
        public string Email { get; set; } = null!;
    }
}
