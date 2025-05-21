using Microsoft.EntityFrameworkCore;
using Quantaco.DataAccess.Context;
using Quantaco.DataAccess.Entities;
using Quantaco.DataAccess.Repositories.Interfaces;

namespace Quantaco.DataAccess.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly QuantacoDbContext _context;

        public TeacherRepository(QuantacoDbContext context)
        {
            _context = context;
        }

        public async Task<Teacher> AddAsync(Teacher teacher)
        {
            if (teacher == null) throw new ArgumentNullException(nameof(teacher));

            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();
            return teacher;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            if (email == null) throw new ArgumentNullException(nameof(email));

            return await _context.Teachers.AnyAsync(t => t.Email == email);
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync(int skip, int take)
        {
            //can be improved to take orderby values
            return await _context.Teachers
                .Include(t => t.Students)
                .OrderBy(t => t.UserName)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<Teacher?> GetByEmailAsync(string email)
        {
            if (email == null) throw new ArgumentNullException(nameof(email));

            return await _context.Teachers
                .Include(t => t.Students)
                .FirstOrDefaultAsync(t => t.Email == email);
        }

        public async Task<Teacher?> GetByIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentNullException(nameof(id));

            return await _context.Teachers
                .Include(t => t.Students)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Teacher?> GetByUsernameAsync(string username)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));

            return await _context.Teachers
                .Include(t => t.Students)
                .FirstOrDefaultAsync(t => t.UserName == username);
        }

        public async Task<int> GetTotalTeacherCountAsync()
        {
            return await _context.Teachers.CountAsync();
        }

        public async Task UpdateAsync(Teacher teacher)
        {
            if (teacher == null) throw new ArgumentNullException(nameof(teacher));

            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));

            return await _context.Teachers.AnyAsync(t => t.UserName == username);
        }
    }
}
