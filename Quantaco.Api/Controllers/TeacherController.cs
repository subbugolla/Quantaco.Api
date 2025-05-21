using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quantaco.Api.Services.Interfaces;
using Quantaco.Models;

namespace Quantaco.Api.Controllers
{
    /// <summary>
    /// Teachers controller - only authorized users should be able to call this
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/teacher")]
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet("get-all-teachers")]
        public async Task<ActionResult<IEnumerable<TeacherModel>>> GetAllTeacher(int offset, int limit)
        {
            //Implement paging for performance optimization
            var teachers = await _teacherService.GetAllAsync(offset, limit);
            var totalCount = await _teacherService.GetTeachersCountAsync();

            return Ok(new TeachersListModel
            {
                Items = teachers,
                TotalCount = totalCount,
                Offset = offset,
                Limit = limit,
            });
        }

        [HttpGet("get-teacher-by-id")]
        public async Task<ActionResult<TeacherModel>> GetTeacherById(int id)
        {
            var teacher = await _teacherService.GetByIdAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }
    }
}
