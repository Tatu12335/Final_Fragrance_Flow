using Fragrance_flow_DL_VERSION_.Application.interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Fragrance_flow_DL_VERSION_.Infrastructure.Security
{
    public class Passwordhasher : IPasswordhasher
    {

        private const int saltSize = 64;
        private const int iterations = 350000;
        private HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        // hashes the password using pbkdf2 And a salt, iterates 35 0000 times 
        public string HashPassword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(saltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                saltSize
            );
            return Convert.ToHexString(hash);
        }
        // verifies given hash and the salt 
        public bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
                    password,
                    salt,
                    iterations,
                    hashAlgorithm,
                    saltSize);
            // Used fixed time comparison to prevent timing attacks
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }
    }
}
