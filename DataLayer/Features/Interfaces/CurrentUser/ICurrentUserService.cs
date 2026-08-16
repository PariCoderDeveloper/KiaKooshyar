namespace KiaKooshar.Application.Features.Interfaces.CurrentUser
{
    public interface ICurrentUserService
    {
        public long? UserId { get; }
        public string? Username { get; }
        public string? IP { get; }
    }
}
