using KiaKooshar.Application.Features.Interfaces.CurrentUser;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace KiaKooshar.Infrastructure.Services
{
    internal class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService (
            IHttpContextAccessor httpContextAccessor
            )
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public long? UserId
        {
            get
            {
                var claim = _httpContextAccessor.HttpContext?.User?.FindFirst (
                    ClaimTypes.NameIdentifier
                    );
                return claim != null
                    && long.TryParse (claim.Value, out var id)
                    ? id
                    : null;
            }
        }

        public string? Username =>
            _httpContextAccessor.HttpContext?.User?.Identity?.Name;

        public string? IP =>
            _httpContextAccessor
                .HttpContext?
                .Connection
                .RemoteIpAddress?
                .ToString ();
    }
}
