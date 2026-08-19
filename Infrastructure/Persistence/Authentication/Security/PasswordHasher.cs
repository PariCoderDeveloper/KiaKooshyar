using KiaKooshar.Application.Construct.Security;
using KiaKooshar.Application.Features.Interfaces.Security;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace KiaKooshar.Infrastructure.Persistence.Authentication.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        private const byte FormatMarker = 0x01;
        private const int HeaderLength = 13;

        private readonly PasswordHasherOptions _options;

        public PasswordHasher ( IOptions<PasswordHasherOptions> options )
        {
            _options = options.Value;
        }

        public string HashPassword ( string password )
        {
            if ( string.IsNullOrEmpty (password) )
                throw new ArgumentException ("Password cannot be null or empty.", nameof (password));

            var salt = new byte[_options.SaltSize];
            RandomNumberGenerator.Fill (salt);

            var subkey = KeyDerivation.Pbkdf2 (
                password,
                salt,
                _options.Prf,
                _options.IterationCount,
                _options.SubkeySize);

            var output = new byte[HeaderLength + salt.Length + subkey.Length];
            output[0] = FormatMarker;
            WriteUInt32BigEndian (output, 1, (uint) _options.Prf);
            WriteUInt32BigEndian (output, 5, (uint) _options.IterationCount);
            WriteUInt32BigEndian (output, 9, (uint) salt.Length);

            Buffer.BlockCopy (salt, 0, output, HeaderLength, salt.Length);
            Buffer.BlockCopy (subkey, 0, output, HeaderLength + salt.Length, subkey.Length);

            return Convert.ToBase64String (output);
        }

        public bool VerifyPassword ( string hashedPassword, string enteredPassword )
        {
            if ( string.IsNullOrEmpty (hashedPassword) || string.IsNullOrEmpty (enteredPassword) )
                return false;

            if ( !TryParse (hashedPassword, out var prf, out var iterations, out var salt, out var expectedSubkey) )
                return false;

            try
            {
                var actualSubkey = KeyDerivation.Pbkdf2 (
                    enteredPassword,
                    salt,
                    prf,
                    iterations,
                    expectedSubkey.Length);

                return CryptographicOperations.FixedTimeEquals (actualSubkey, expectedSubkey);
            }
            catch
            {
                return false;
            }
        }

        public bool NeedsRehash ( string hashedPassword )
        {
            if ( !TryParse (hashedPassword, out var prf, out var iterations, out var salt, out var subkey) )
                return true;

            return prf != _options.Prf
                || iterations < _options.IterationCount
                || salt.Length < _options.SaltSize
                || subkey.Length < _options.SubkeySize;
        }

        private static bool TryParse (
            string hashedPassword,
            out KeyDerivationPrf prf,
            out int iterations,
            out byte[] salt,
            out byte[] subkey )
        {
            prf = default;
            iterations = 0;
            salt = Array.Empty<byte> ();
            subkey = Array.Empty<byte> ();

            byte[] decoded;
            try
            {
                decoded = Convert.FromBase64String (hashedPassword);
            }
            catch
            {
                return false;
            }

            if ( decoded.Length < HeaderLength || decoded[0] != FormatMarker )
                return false;

            try
            {
                var prfValue = ReadUInt32BigEndian (decoded, 1);
                prf = prfValue switch
                {
                    0 => KeyDerivationPrf.HMACSHA1,
                    1 => KeyDerivationPrf.HMACSHA256,
                    2 => KeyDerivationPrf.HMACSHA512,
                    _ => throw new FormatException ("Unknown PRF marker.")
                };

                iterations = (int) ReadUInt32BigEndian (decoded, 5);
                var saltLength = (int) ReadUInt32BigEndian (decoded, 9);

                if ( saltLength < 0 || HeaderLength + saltLength > decoded.Length )
                    return false;

                salt = new byte[saltLength];
                Buffer.BlockCopy (decoded, HeaderLength, salt, 0, saltLength);

                var subkeyLength = decoded.Length - HeaderLength - saltLength;
                if ( subkeyLength <= 0 )
                    return false;

                subkey = new byte[subkeyLength];
                Buffer.BlockCopy (decoded, HeaderLength + saltLength, subkey, 0, subkeyLength);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static uint ReadUInt32BigEndian ( byte[] buffer, int offset )
            => (uint) buffer[offset] << 24
               | (uint) buffer[offset + 1] << 16
               | (uint) buffer[offset + 2] << 8
               | buffer[offset + 3];

        private static void WriteUInt32BigEndian ( byte[] buffer, int offset, uint value )
        {
            buffer[offset] = (byte) (value >> 24);
            buffer[offset + 1] = (byte) (value >> 16);
            buffer[offset + 2] = (byte) (value >> 8);
            buffer[offset + 3] = (byte) value;
        }
    }
}