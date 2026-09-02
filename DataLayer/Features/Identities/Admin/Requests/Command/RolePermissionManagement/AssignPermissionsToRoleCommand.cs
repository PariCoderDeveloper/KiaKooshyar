using KiaKooshar.Application.DTOs.Common;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Admin.Requests.Command.RolePermissionManagement
{
    public class AssignPermissionsToRoleCommand :
        IRequest<ResultDTO>
    {
        public long roleId { get; set; }
        public List<long> permissionIds { get; set; }
            = new List<long> ();
    }
}
