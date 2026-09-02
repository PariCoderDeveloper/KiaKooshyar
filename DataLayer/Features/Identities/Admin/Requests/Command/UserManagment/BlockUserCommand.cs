using KiaKooshar.Application.DTOs.Common;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Admin.Requests.Command.UserManagment
{
    public class BlockUserCommand :
        IRequest<ResultDTO>
    {
        public long Id { get; set; }
        public long UserId { get; set; }
    }
}
