using KiaKooshar.Application.Caching.Contracts;
using KiaKooshar.Application.Caching.Policies;
using KiaKooshar.Application.DTOs.Common;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands
{
    public class LogoutCommand : IRequest<ResultDTO>,
        ICacheInvalidationRequest
    {
        public long Id { get; set; }
        public string RefreshToken { get; set; } = null!;

        public string[] CacheKeysList => [
            CacheKeys.UserPermissions(Id)
            ];
    }
}
