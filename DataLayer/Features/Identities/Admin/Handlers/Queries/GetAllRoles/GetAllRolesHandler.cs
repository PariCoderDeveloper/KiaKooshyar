using AutoMapper;
using KiaKooshar.Application.Common.Models;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.Admin.RolePermissionManagement;
using KiaKooshar.Application.Features.Identities.Admin.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KiaKooshar.Application.Features.Identities.Admin.Handlers.Queries.GetAllRoles
{
    public class GetAllRolesHandler :
        IRequestHandler<GetAllRolesQuery,
            ResultDTO<PagedResult<RoleDTO>>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public GetAllRolesHandler (
            IUnitOfWork unit,
            IMapper mapper
            )
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<ResultDTO<PagedResult<RoleDTO>>> Handle (
            GetAllRolesQuery request,
            CancellationToken cancellationToken
            )
        {
            var roles = _unit.Roles.GetAllAsync
                (cancellationToken);
            if ( !await roles.AnyAsync (cancellationToken) )
                return ResultDTO<PagedResult<RoleDTO>>.NotFound (
                    "Role doesnt found"
                    );
            var pagedResult = await roles.ToPagedResultAsync
                 (request.PaginationRequest);

            var mapperResult = _mapper.Map<PagedResult<RoleDTO>> (
                pagedResult
                );
            return ResultDTO<PagedResult<RoleDTO>>.Success (
                mapperResult,
                ""
                );
        }
    }
}
