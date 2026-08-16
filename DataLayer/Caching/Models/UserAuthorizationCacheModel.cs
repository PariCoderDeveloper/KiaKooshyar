namespace KiaKooshar.Application.Caching.Models
{
    public class UserAuthorizationCacheModel
    {
        public long UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<string> Roles { get; set; } = [];
        public List<string> Permissions { get; set; } = [];
    }
}
