using Microsoft.Extensions.Configuration;

namespace Quantaco.Api.Tests
{
    public class AuthServiceTests
    {
        private readonly IConfiguration _configuration;
        public AuthServiceTests(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}
