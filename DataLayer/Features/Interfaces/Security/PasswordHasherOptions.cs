using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace KiaKooshar.Application.Features.Interfaces.Security
{
    public class PasswordHasherOptions
    {
        public int IterationCount { get; set; } = 600_000;
        public int SaltSize { get; set; } = 128 / 8;
        public int SubkeySize { get; set; } = 256 / 8;
        public KeyDerivationPrf Prf { get; set; } = KeyDerivationPrf.HMACSHA256;

    }
}
