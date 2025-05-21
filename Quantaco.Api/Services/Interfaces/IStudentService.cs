using Quantaco.Models;

namespace Quantaco.Api.Services.Interfaces
{
    public interface IStudentService
    {
        Task<StudentModel> CreateStudentAsync(int teacherId, CreateStudentModel createStudent);
        Task<IEnumerable<StudentModel>> GetStudentByTeacherIdAsync(int teacherId, int page = 1, int pageSize = 10);
        Task<StudentModel> GetStudentByIdAsync(int id);
        Task DeleteStudentAsync(int teacherId, int studentId);
        Task<int> GetStudentCountByTeacherIdAsync(int teacherId);
    }
}
