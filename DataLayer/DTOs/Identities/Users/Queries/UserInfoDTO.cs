namespace KiaKooshar.Application.DTOs.Identities.Users.Queries
{
    public class UserInfoDTO
    {
        public long Id { get; set; }
        public string Username { get; set; } = null!;
        public List<string> Roles { get; set; } = new List<string> ();
        public List<string> Permissions { get; set; } = new List<string> ();
    }
}
