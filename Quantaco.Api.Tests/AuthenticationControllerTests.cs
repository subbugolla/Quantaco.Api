using Microsoft.AspNetCore.Mvc;
using Moq;
using Quantaco.Api.Controllers;
using Quantaco.Api.Services.Interfaces;
using Quantaco.Models;

namespace Quantaco.Api.Tests
{
    public class AuthenticationControllerTests
    {
        private readonly Mock<ILoginService> _mockLoginService;
        private readonly AuthenticationController _authenticationController;
        public AuthenticationControllerTests()
        {
            _mockLoginService = new Mock<ILoginService>();
            _authenticationController = new AuthenticationController(this._mockLoginService.Object);
        }

        [Fact]
        public async void Register_Success() 
        {
            TeacherRegistrationModel? registration = new TeacherRegistrationModel() { Username = "TestUser"};
            TeacherAuthResponseModel auth = new TeacherAuthResponseModel() { Teacher = new TeacherModel() { Username = "TestUser"} };
            _mockLoginService.Setup(m => m.RegisterAsync(registration)).ReturnsAsync(auth);
            var actual = await _authenticationController.Register(registration);
            Assert.IsType<OkObjectResult>((ObjectResult)actual.Result);
            Assert.True(registration.Username == ((TeacherAuthResponseModel)((Microsoft.AspNetCore.Mvc.ObjectResult)actual.Result).Value).Teacher.Username);
        }
    }
}