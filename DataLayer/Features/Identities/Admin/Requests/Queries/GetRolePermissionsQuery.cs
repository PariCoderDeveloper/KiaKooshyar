using KiaKooshar.Application.Common.Models;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.Admin.RolePermissionManagement;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Admin.Requests.Queries
{
    public class GetRolePermissionsQuery :
        IRequest<ResultDTO<PagedResult<GetPermissionDTO>>>
    {
        public long roleId { get; set; }
        public string filter { get; set; } = null!;
        public PaginationRequest PaginationRequest { get; set; }
             = null!;
    }
}
