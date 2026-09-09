using KiaKooshar.Domain.Enums;

namespace KiaKooshar.Application.DTOs.Identities.Users.Queries
{
    public class GetAllUsersDTO
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public UserStatus UserStatus { get; set; }
        public List<string> Roles { get; set; } = new List<string> ();
        public List<string> Permissions { get; set; } = new List<string> ();
        public List<string> RolePermissions { get; set; } = new List<string> ();
    }
}
