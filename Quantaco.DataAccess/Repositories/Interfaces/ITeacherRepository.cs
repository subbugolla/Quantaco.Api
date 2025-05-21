using Quantaco.DataAccess.Entities;

namespace Quantaco.DataAccess.Repositories.Interfaces
{
    public interface ITeacherRepository
    {
        Task<Teacher?> GetByIdAsync(int id);
        Task<Teacher?> GetByUsernameAsync(string username);
        Task<Teacher?> GetByEmailAsync(string email);
        Task<IEnumerable<Teacher>> GetAllAsync(int offset, int limit);
        Task<int> GetTotalTeacherCountAsync();
        Task<Teacher> AddAsync(Teacher teacher);
        Task UpdateAsync(Teacher teacher);
        Task<bool> UsernameExistsAsync(string username);
        Task<bool> EmailExistsAsync(string email);
    }
}
