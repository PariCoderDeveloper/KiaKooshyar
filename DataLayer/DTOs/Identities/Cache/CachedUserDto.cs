namespace KiaKooshar.Application.DTOs.Identities.Cache
{
    public class CachedUserDTO
    {
        public long Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public List<string> Roles { get; set; }
            = new List<string> ();
        public List<string> RolePermissions { get; set; }
            = new List<string> ();
        public List<string> Permissions { get; set; }
            = new List<string> ();
    }
}
