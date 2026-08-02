using KiaKooshar.Application.Features.Interfaces.HttpContext;
using Microsoft.AspNetCore.Http;

namespace KiaKooshar.Infrastructure.Services;

public class HttpRequestContext : IRequestContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public HttpRequestContext (
        IHttpContextAccessor httpContextAccessor )
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public string? IpAddress =>
        _httpContextAccessor.HttpContext?
        .Connection
        .RemoteIpAddress?
        .ToString ();
    public string? UserAgent =>
        _httpContextAccessor.HttpContext?
        .Request
        .Headers["User-Agent"]
        .ToString ();
    public string? Device => UserAgent;
    public long? UserId
    {
        get
        {
            var id = _httpContextAccessor.HttpContext?
                .User
                .FindFirst ("sub")
                ?.Value;

            return long.TryParse (id, out var userId)
                ? userId
                : null;
        }
    }
}