using KiaKooshar.Application.Authorization;
using KiaKooshar.Application.Caching.Contracts;
using KiaKooshar.Application.Caching.Policies;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.Users.Commands;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Users.Requests.Commands
{
    public class UpdateUserCommand : IRequest<ResultDTO<UpdateUserDTO>>,
        ICacheInvalidationRequest,
        IRequirePermission
    {
        public UpdateUserDTO UpdateUserDTO { get; set; } = null!;

        public string[] CacheKeysList => [
            CacheKeys.UserPermissions(UpdateUserDTO.Id),
            ];
        public string[]? CacheGroups => throw new NotImplementedException ();

        public string Permission => throw new NotImplementedException ();
    }
}
