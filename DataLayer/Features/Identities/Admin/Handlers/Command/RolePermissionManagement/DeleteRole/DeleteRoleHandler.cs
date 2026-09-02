using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Features.Identities.Admin.Requests.Command.RolePermissionManagement;
using KiaKooshar.Domain.Entities.Identity;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Admin.Handlers.Command.RolePermissionManagement.DeleteRole
{
    public class DeleteRoleHandler :
        IRequestHandler<DeleteRoleCommand, ResultDTO>
    {
        private readonly IUnitOfWork _unit;
        public DeleteRoleHandler (
            IUnitOfWork unit
            )
        {
            _unit = unit;
        }
        public async Task<ResultDTO> Handle (
            DeleteRoleCommand request,
            CancellationToken cancellationToken
            )
        {
            var role = await _unit.Roles
                 .GetByIdAsync (
                    request.RoleId,
                    cancellationToken
                    );
            if ( role is null )
                return ResultDTO.NotFound ("Role doesnt found");
            _unit.Roles.Delete<Role> (role);
            var result = await _unit.CommitAsync (
                cancellationToken
             );
            if ( result < 0 )
                return ResultDTO.Failure (
                    "There is an error in deleting role"
                    );
            return ResultDTO.Success (
                "Role successfully deleted"
                );
        }
    }
}
