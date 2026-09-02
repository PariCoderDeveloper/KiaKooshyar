using KiaKooshar.Application.Common.Models;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.Admin.RolePermissionManagement;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Admin.Requests.Queries
{
    public class GetAllPermissionsQuery :
        IRequest<ResultDTO<PagedResult<GetPermissionDTO>>>
    {
        public PaginationRequest paginationRequest { get; set; } = null!;
    }
}
