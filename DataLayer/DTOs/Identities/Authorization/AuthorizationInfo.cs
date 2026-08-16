namespace KiaKooshar.Application.DTOs.Identities.Authorization
{
    public sealed class AuthorizationInfo
    {
        public IReadOnlySet<string>? Roles { get; init; }
        public IReadOnlySet<string>? Permissions { get; init; }
    }
}
