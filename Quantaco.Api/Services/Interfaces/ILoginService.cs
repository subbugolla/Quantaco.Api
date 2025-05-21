using Quantaco.Models;

namespace Quantaco.Api.Services.Interfaces
{
    public interface ILoginService
    {
        Task<TeacherAuthResponseModel> RegisterAsync(TeacherRegistrationModel registration);
        Task<TeacherAuthResponseModel> LoginAsync(TeacherLoginModel login);
    }
}
