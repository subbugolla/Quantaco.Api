using Quantaco.DataAccess.Entities;
using Quantaco.Models;

namespace Quantaco.Api.Services.Interfaces
{
    public interface IAuthService
    {
        string GenerateJwtToken(Teacher teacher);
    }
}
