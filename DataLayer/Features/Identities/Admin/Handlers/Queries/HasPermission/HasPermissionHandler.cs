using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Features.Identities.Admin.Requests.Queries;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Admin.Handlers.Queries.HasPermission
{
    public class HasPermissionHandler
        : IRequestHandler<HasPermissionQuery, ResultDTO>
    {
        private readonly IUnitOfWork _unit;
        public HasPermissionHandler (
            IUnitOfWork unit
            )
        {
            _unit = unit;
        }
        public async Task<ResultDTO> Handle (
            HasPermissionQuery request,
            CancellationToken cancellationToken = default
            )
        {
            var hasPermission = await _unit.Users
                .HasPermission (
                    request.UserId,
                    request.Permission,
                    cancellationToken
                    );

            if ( !hasPermission )
            {
                return ResultDTO.Failure (
                    "User doesn't have this permission."
                    );
            }

            return ResultDTO.Success (
                "User has this permission.");
        }
    }
}
