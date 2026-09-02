using AutoMapper;
using KiaKooshar.Application.Common.Models;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.Admin.RolePermissionManagement;
using KiaKooshar.Application.Features.Identities.Admin.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KiaKooshar.Application.Features.Identities.Admin.Handlers.Queries.GetAllPermissions
{
    public class GetAllPermissionsHandler :
        IRequestHandler<GetAllPermissionsQuery,
            ResultDTO<PagedResult<GetPermissionDTO>>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public GetAllPermissionsHandler (
            IUnitOfWork unit,
            IMapper mapper
            )
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<ResultDTO<PagedResult<GetPermissionDTO>>> Handle (
            GetAllPermissionsQuery request,
            CancellationToken cancellationToken
            )
        {
            var permissions = _unit.Permissions.GetAllAsync (
                 cancellationToken
                 );
            if ( !await permissions.AnyAsync (cancellationToken) )
                return ResultDTO<PagedResult<GetPermissionDTO>>.NotFound (
                    "No permission found"
                    );
            var pagedResult = await permissions.ToPagedResultAsync (
                request.paginationRequest
                );
            var resultPermissins = _mapper.Map
                <PagedResult<GetPermissionDTO>>
                (pagedResult);
            return ResultDTO<PagedResult<GetPermissionDTO>>
                .Success (
                    resultPermissins,
                    ""
                    );
        }
    }
}
