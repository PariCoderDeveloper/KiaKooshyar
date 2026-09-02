using KiaKooshar.Application.DTOs.Common;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Admin.Requests.Command.UserManagment
{
    public class EnableUserCommand :
        IRequest<ResultDTO>
    {
        public long UserId { get; set; }
    }
}
