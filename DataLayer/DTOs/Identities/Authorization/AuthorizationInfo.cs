namespace KiaKooshar.Application.DTOs.Identities.Authorization
{
    public sealed class AuthorizationInfo
    {
        public required IReadOnlySet<string> Roles { get; init; }
        public required IReadOnlySet<string> Permissions { get; init; }
    }
}
