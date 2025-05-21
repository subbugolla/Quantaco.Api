using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Quantaco.Api.Helpers
{
    public static class AuthTokenHelper
    {
        /// <summary>
        /// Generate Jwt Auth token along with the Login User information
        /// </summary>
        /// <param name="sub"></param>
        /// <param name="email"></param>
        /// <param name="uniqueName"></param>
        /// <param name="issuer"></param>
        /// <param name="jwtKey"></param>
        /// <param name="audience"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static string GenerateJwtToken(string sub, string email, string uniqueName, string issuer, string jwtKey, string audience)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey ?? throw new InvalidOperationException("JWT key not configured")));
            
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //add necessary claims for token validation
            var claims = new Claim[]
            {
                new (JwtRegisteredClaimNames.Sub, sub),
                new (JwtRegisteredClaimNames.Email, email),
                new (JwtRegisteredClaimNames.UniqueName, uniqueName),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
