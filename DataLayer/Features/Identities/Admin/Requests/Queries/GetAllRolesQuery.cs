using KiaKooshar.Application.Common.Models;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.Admin.RolePermissionManagement;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Admin.Requests.Queries
{
    public class GetAllRolesQuery :
        IRequest<ResultDTO<PagedResult<RoleDTO>>>
    {
        public PaginationRequest PaginationRequest { get; set; }
            = null!;
    }
}
