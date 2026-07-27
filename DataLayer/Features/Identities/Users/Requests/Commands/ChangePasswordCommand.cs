using KiaKooshar.Application.Authorization;
using KiaKooshar.Application.DTOs.Common;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Users.Requests.Commands
{
    public class ChangePasswordCommand : IRequest<ResultDTO>,
        IRequirePermission
    {
        public long Id { get; set; }
        public string Password { get; set; } = null!;

        public string Permission => throw new NotImplementedException ();
    }
}
