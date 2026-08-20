using KiaKooshar.Application.Features.Interfaces.HttpContext;
using Microsoft.AspNetCore.Http;
using UAParser;

namespace KiaKooshar.Infrastructure.Services;

public class HttpRequestContext : IRequestContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private static readonly Parser _uaParser = Parser.GetDefault ();
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
    public string? Device
    {
        get
        {
            var userAgent = _httpContextAccessor.HttpContext?.Request.Headers.UserAgent.ToString ();
            if ( string.IsNullOrWhiteSpace (userAgent) )
                return null;

            var clientInfo = _uaParser.Parse (userAgent);
            return clientInfo.Device.Family;
        }
    }
    long IRequestContext.UserId
    {
        get
        {
            var id = _httpContextAccessor.HttpContext?
                .User
                .FindFirst ("sub")
                ?.Value;
            long.TryParse (id, out var userId);
            return userId;
        }
    }

    public string? Browser
    {
        get
        {
            var userAgent = _httpContextAccessor.HttpContext?.Request.Headers.UserAgent.ToString ();
            if ( string.IsNullOrWhiteSpace (userAgent) )
                return null;

            var clientInfo = _uaParser.Parse (userAgent);
            return $"{clientInfo.UA.Family} {clientInfo.UA.Major}";
        }
    }

    public string? OS
    {
        get
        {
            var userAgent = _httpContextAccessor.HttpContext?.Request.Headers.UserAgent.ToString ();
            if ( string.IsNullOrWhiteSpace (userAgent) )
                return null;

            var clientInfo = _uaParser.Parse (userAgent);
            return $"{clientInfo.OS.Family} {clientInfo.OS.Major}";
        }
    }
}