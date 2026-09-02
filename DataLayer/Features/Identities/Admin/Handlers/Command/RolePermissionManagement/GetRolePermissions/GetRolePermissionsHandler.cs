using AutoMapper;
using KiaKooshar.Application.Common.Models;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.Admin.RolePermissionManagement;
using KiaKooshar.Application.Features.Identities.Admin.Requests.Queries;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Admin.Handlers.Command.RolePermissionManagement.GetRolePermissions
{
    public class GetRolePermissionsHandler :
        IRequestHandler<GetRolePermissionsQuery,
            ResultDTO<PagedResult<GetPermissionDTO>>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public GetRolePermissionsHandler (
            IUnitOfWork unit,
            IMapper mapper
            )
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<ResultDTO<PagedResult<GetPermissionDTO>>>
            Handle (
            GetRolePermissionsQuery request,
            CancellationToken cancellationToken
            )
        {
            var permissions = _unit.RolePermission.
                GetPermissionsForRoleAsync (
                    request.roleId,
                    cancellationToken
                 );
            if ( permissions is null )
                return ResultDTO<PagedResult<GetPermissionDTO>>
                     .Failure (
                        "There is no permission for this role"
                    );
            var pagedPermission = await permissions.ToPagedResultAsync
                 (
                     request.PaginationRequest,
                     cancellationToken
                 );
            var permissionDto = _mapper.Map<PagedResult<GetPermissionDTO>> (
                pagedPermission
                );
            return ResultDTO<PagedResult<GetPermissionDTO>>
                .Success (
                permissionDto,
                ""
                );
        }
    }
}
