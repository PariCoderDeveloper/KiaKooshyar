using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.Admin.UserManagement;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Admin.Requests.Command.RolePermissionManagement
{
    public class RemoveRoleFromUserCommand :
        IRequest<ResultDTO>
    {
        public UserChangedBase UserChangedBase { get; set; } = null!;
        public long roleId { get; set; }
    }
}
