using System.Security.Cryptography;
using System.Text;

namespace laTienda.Helpers
{
    public static class PasswordHelper
    {
        public static void CrearPasswordHash(
            string password,
            out byte[] passwordHash,
            out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();

            passwordSalt = hmac.Key;

            passwordHash = hmac.ComputeHash(
                Encoding.UTF8.GetBytes(password));
        }

        public static bool VerificarPassword(
            string password,
            byte[] passwordHash,
            byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);

            var hashCalculado = hmac.ComputeHash(
                Encoding.UTF8.GetBytes(password));

            return hashCalculado.SequenceEqual(passwordHash);
        }
    }
}