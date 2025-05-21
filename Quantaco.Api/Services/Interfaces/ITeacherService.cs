using Quantaco.Models;

namespace Quantaco.Api.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<TeacherModel> GetByIdAsync(int id);
        Task<IEnumerable<TeacherModel>> GetAllAsync(int offset, int limit);
        Task<int> GetTeachersCountAsync();
    }
}
