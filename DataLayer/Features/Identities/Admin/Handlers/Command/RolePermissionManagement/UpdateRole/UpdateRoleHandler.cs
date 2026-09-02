using AutoMapper;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Features.Identities.Admin.Requests.Command.RolePermissionManagement;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Admin.Handlers.Command.RolePermissionManagement.UpdateRole
{
    public class UpdateRoleHandler :
        IRequestHandler<UpdateRoleCommand, ResultDTO>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public UpdateRoleHandler (
            IUnitOfWork unit,
            IMapper mapper
            )
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<ResultDTO> Handle (
            UpdateRoleCommand request,
            CancellationToken cancellationToken
            )
        {
            var role = await _unit.Roles.GetByIdAsync (
                request.Role.id,
                cancellationToken
                );
            if ( role is null )
                return ResultDTO.NotFound ("Role doesnt found");
            role.UpdatedAt = DateTime.UtcNow;
            _mapper.Map (role, request.Role);
            return ResultDTO.Success (
                "Role updated successfully"
                );
        }
    }
}