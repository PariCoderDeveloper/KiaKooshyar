namespace KiaKooshar.Application.DTOs.Identities.Users.Queries
{
    public class GetAllUsersDTO
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? Avator { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
