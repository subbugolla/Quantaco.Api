using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Quantaco.Api.Services.Interfaces;
using Quantaco.Models;

namespace Quantaco.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/student")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;

        public StudentController(IStudentService studentService, ITeacherService teacherService)
        {
            _studentService = studentService;
            _teacherService = teacherService;
        }

        /// <summary>
        /// Create Student
        /// </summary>
        /// <param name="createStudentModel"></param>
        /// <returns></returns>
        [HttpPost("create-student")]
        public async Task<ActionResult<StudentModel>> Create(CreateStudentModel createStudentModel)
        {
            var teacherId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (teacherId <= 0)
            {
                return Forbid();
            }

            var teacher = await _teacherService.GetByIdAsync(teacherId);
            if (teacher == null)
            {
                return NotFound("Teacher Doesn't exist. Please register");
            }

            var student = await _studentService.CreateStudentAsync(teacher.Id, createStudentModel);

            return Ok(student);
        }

        /// <summary>
        /// Get Student By logged in teacher Id
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("get-students-by-teacher")]
        public async Task<ActionResult<StudentsListModel>> GetByTeacher(int offset, int limit)
        {
            var teacherId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (teacherId <= 0)
            {
                return Forbid();
            }

            var teacher = await _teacherService.GetByIdAsync(teacherId);
            if (teacher == null)
            {
                return NotFound("Teacher Doesn't exist. Please register");
            }
            //Retrieve limited records for peroformance and UI pagination
            var students = await _studentService.GetStudentByTeacherIdAsync(teacher.Id, offset, limit);
            var totalCount = await _studentService.GetStudentCountByTeacherIdAsync(teacher.Id);

            return Ok(new StudentsListModel
            {
                Items = students,
                TotalCount = totalCount,
                Offset = limit,
                Limit = offset,
            });
        }

        /// <summary>
        /// Get Student By studentId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get-student-by-id")]
        public async Task<ActionResult<StudentModel>> GetStudentById(int id)
        {
            var teacherId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (teacherId <= 0)
            {
                return Forbid();
            }

            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            //Second level authorization.
            var teacher = await _teacherService.GetByIdAsync(teacherId);
            if (teacher == null || student.TeacherId != teacher.Id)
            {
                return Forbid();
            }

            return Ok(student);
        }

        /// <summary>
        /// Delete Student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete-student")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            var teacherId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (teacherId <= 0)
            {
                return Forbid();
            }

            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            //Second level authorization
            var teacher = await _teacherService.GetByIdAsync(teacherId);
            if (teacher == null || student.TeacherId != teacher.Id)
            {
                return Forbid();
            }

            await _studentService.DeleteStudentAsync(teacherId, id);
            return NoContent();
        }
    }
}
