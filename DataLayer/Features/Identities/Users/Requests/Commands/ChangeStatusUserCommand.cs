using KiaKooshar.Application.Authorization;
using KiaKooshar.Application.Caching.Contracts;
using KiaKooshar.Application.Caching.Policies;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Domain.Enums;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Users.Requests.Commands
{
    public class ChangeStatusUserCommand : IRequest<ResultDTO<UserStatus>>,
        ICacheInvalidationRequest,
        IRequirePermission
    {
        public long Id { get; set; }
        public UserStatus Status { get; set; }
        public string[] CacheKeysList => [
            CacheKeys.UserPermissions(Id),
            ];
        public string[]? CacheGroups => throw new NotImplementedException ();

        public string Permission => throw new NotImplementedException ();
    }
}
