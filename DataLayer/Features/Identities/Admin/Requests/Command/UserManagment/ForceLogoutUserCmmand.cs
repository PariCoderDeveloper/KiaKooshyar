using KiaKooshar.Application.DTOs.Common;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Admin.Requests.Command
{
    public class ForceLogoutUserCmmand :
        IRequest<ResultDTO>
    {
        public long Id { get; set; }
    }
}
