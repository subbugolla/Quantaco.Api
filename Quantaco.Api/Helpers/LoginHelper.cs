using Crypto = BCrypt.Net.BCrypt;

namespace Quantaco.Api.Helpers
{
    public static class LoginHelper
    {
        /// <summary>
        /// Hash Password before storing
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string HashPassword(string password)
        {
            //Hash passwords before storing
            return Crypto.HashPassword(password, Crypto.GenerateSalt());
        }

        /// <summary>
        /// Verify Password with the one in storage
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public static bool VerifyPassword(string password, string passwordHash)
        {
            return Crypto.Verify(password, passwordHash);
        }
    }
}
