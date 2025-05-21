using Quantaco.Api.Helpers;
using Quantaco.DataAccess.Entities;
using Quantaco.DataAccess.Repositories.Interfaces;
using Quantaco.Models;
using Quantaco.Api.Services.Interfaces;

namespace Quantaco.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(Teacher teacher)
        {
            //ideally you would use identity provider to handle this
            if (_configuration == null) throw new ArgumentException("Invalid config");

            var tokenIssuer = _configuration["Secret:TokenIssuer"] ?? string.Empty;
            var tokenAudience = _configuration["Secret:TokenAudience"] ?? string.Empty;
            var securityKey = _configuration["Secret:TokenSecurityKey"] ?? string.Empty;

            return AuthTokenHelper.GenerateJwtToken(teacher.Id.ToString(), teacher.Email, teacher.UserName, tokenIssuer, securityKey, tokenAudience);
        }
    }
}
