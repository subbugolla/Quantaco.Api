using Quantaco.Api.Helpers;
using Quantaco.DataAccess.Entities;
using Quantaco.DataAccess.Repositories.Interfaces;
using Quantaco.Models;
using Quantaco.Api.Services.Interfaces;
namespace Quantaco.Api.Services
{
    public class LoginService : ILoginService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IAuthService _authService;

        public LoginService(ITeacherRepository teacherRepository, IAuthService authService)
        {
            _teacherRepository = teacherRepository;
            _authService = authService;
        }

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<TeacherAuthResponseModel> RegisterAsync(TeacherRegistrationModel registration)
        {
            if (registration == null) throw new ArgumentNullException(nameof(registration));

            //check uniqueness before registering
            if (await _teacherRepository.UsernameExistsAsync(registration.Username))
                throw new InvalidOperationException("Username already exists");

            if (await _teacherRepository.EmailExistsAsync(registration.Email))
                throw new InvalidOperationException("Email already exists");

            var teacher = new Teacher
            {
                UserName = registration.Username,
                Email = registration.Email,
                FirstName = registration.FirstName,
                LastName = registration.LastName,
                PasswordHash = LoginHelper.HashPassword(registration.Password)
            };

            await _teacherRepository.AddAsync(teacher);

            var teacherModel = new TeacherModel()
            {
                Id = teacher.Id,
                Username = registration.Username,
                Email = registration.Email,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                StudentCount = teacher.Students.Count,
            };
            var token = _authService.GenerateJwtToken(teacher);

            return new TeacherAuthResponseModel
            {
                Token = token,
                Teacher = teacherModel
            };
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<TeacherAuthResponseModel> LoginAsync(TeacherLoginModel loginModel)
        {
            if (loginModel == null) throw new ArgumentNullException(nameof(loginModel));

            var teacher = await _teacherRepository.GetByUsernameAsync(loginModel.Username);
            if (teacher == null || !LoginHelper.VerifyPassword(loginModel.Password, teacher.PasswordHash))
                throw new InvalidOperationException("Invalid username or password");

            var teacherModel = new TeacherModel() 
            { 
                Email = teacher.Email,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Id = teacher.Id,
                StudentCount = teacher.Students.Count,
                Username = teacher.UserName,
            };
            var token = _authService.GenerateJwtToken(teacher);

            return new TeacherAuthResponseModel
            {
                Token = token,
                Teacher = teacherModel
            };
        }
    }
}
