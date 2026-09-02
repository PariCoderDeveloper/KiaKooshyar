using KiaKooshar.Application.DTOs.Common;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Admin.Requests.Command.RolePermissionManagement
{
    public class DeleteRoleCommand :
        IRequest<ResultDTO>
    {
        public long RoleId { get; set; }
    }
}
