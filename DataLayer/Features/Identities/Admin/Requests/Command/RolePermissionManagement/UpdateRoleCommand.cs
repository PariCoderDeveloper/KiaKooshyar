using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.Admin.RolePermissionManagement;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Admin.Requests.Command.RolePermissionManagement
{
    public class UpdateRoleCommand :
        IRequest<ResultDTO>
    {
        public RoleDTO Role { get; set; } = null!;
    }
}
