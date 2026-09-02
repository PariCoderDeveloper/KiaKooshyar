using AutoMapper;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Features.Identities.Admin.Requests.Command.RolePermissionManagement;
using KiaKooshar.Domain.Entities.Identity;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Admin.Handlers.Command.RolePermissionManagement.CreateRole
{
    public class CreateRoleHandler :
        IRequestHandler<CreateRoleCommand, ResultDTO>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public CreateRoleHandler (
            IUnitOfWork unit,
            IMapper mapper
            )
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<ResultDTO> Handle (
            CreateRoleCommand request,
            CancellationToken cancellationToken
            )
        {
            var role = _mapper.Map<Role> (request.Role);
            await _unit.Roles.AddAsync
                (role, cancellationToken);
            var result = await _unit.CommitAsync
                (cancellationToken);
            if ( result > 0 )
                return ResultDTO.Failure (
                    "There is an error in creating role"
                    );
            return ResultDTO.Success (
                "Role successfully created"
                );
        }
    }
}
