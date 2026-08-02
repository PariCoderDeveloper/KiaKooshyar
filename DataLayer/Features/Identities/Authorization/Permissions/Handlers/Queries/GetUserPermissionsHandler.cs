using KiaKooshar.Application.Caching.Contracts;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Features.Identities.Authorization.Permissions.Requests.Queries;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Authorization.Permissions.Handlers.Queries
{
    internal class GetUserPermissionsHandler :
        IRequestHandler<GetUserPermissionsCommand, ResultDTO>
    {
        private readonly ICacheService _cache;
        private readonly IUnitOfWork _unit;
        public GetUserPermissionsHandler (
            ICacheService cache,
            IUnitOfWork unit
            )
        {
            _cache = cache;
            _unit = unit;
        }
        public Task<ResultDTO> Handle (
            GetUserPermissionsCommand request,
            CancellationToken cancellationToken
            )
        {
            throw new NotImplementedException ();
        }
    }
}
