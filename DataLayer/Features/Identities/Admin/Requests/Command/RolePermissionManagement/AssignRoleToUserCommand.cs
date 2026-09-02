using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.Admin.UserManagement;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Admin.Requests.Command.RolePermissionManagement
{
    public class AssignRoleToUserCommand :
        IRequest<ResultDTO>
    {
        public long roleId { get; set; }
        public UserChangedBase UserChangedBase { get; set; } = null!;
    }
}
