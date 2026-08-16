namespace KiaKooshar.Application.DTOs.Identities.Authentication
{
    public class AuthenticatedUserDTO
    {
        public long Id { get; set; }
        public string UserName { get; set; } = null!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public ICollection<string> Roles { get; set; } = new List<string> ();
        public ICollection<string> Permissions { get; set; } = new List<string> ();
    }
}
