using KiaKooshar.Application.DTOs.Common;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Admin.Requests.Command.UserManagment
{
    public class ResetUserPasswordCommand :
        IRequest<ResultDTO>
    {
        public long userId { get; set; }
        public long adminUserId { get; set; }
        public string NewPassword { get; set; }
    }
}
