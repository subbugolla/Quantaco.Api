using Quantaco.DataAccess.Entities;

namespace Quantaco.DataAccess.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student?> GetStudentByIdAsync(int id);
        Task<IEnumerable<Student>> GetStudentsByTeacherIdAsync(int teacherId, int offset, int limit);
        Task<int> GetStudentCountByTeacherIdAsync(int teacherId);
        Task<Student> AddStudentAsync(Student student);
        Task DeleteStudentAsync(Student student);
    }
}
