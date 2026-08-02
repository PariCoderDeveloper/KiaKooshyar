namespace KiaKooshar.Application.Features.Interfaces.HttpContext
{
    public interface IRequestContext
    {
        string? IpAddress { get; }
        string? Device { get; }
        string? UserAgent { get; }
        long? UserId { get; }
    }
}
