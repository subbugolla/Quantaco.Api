using Microsoft.EntityFrameworkCore;
using Quantaco.DataAccess.Context;
using Quantaco.DataAccess.Entities;
using Quantaco.DataAccess.Repositories.Interfaces;

namespace Quantaco.DataAccess.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly QuantacoDbContext _context;

        public StudentRepository(QuantacoDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add a new student
        /// </summary>
        /// <param name="student">Student</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Student is null</exception>
        public async Task<Student> AddStudentAsync(Student student)
        {
            if (student == null) throw new ArgumentNullException(nameof(student));

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task DeleteStudentAsync(Student student)
        {
            if (student == null) throw new ArgumentNullException(nameof(student));

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentNullException(nameof(id));

            //include teacher objects
            return await _context.Students
                  .Include(s => s.Teacher)
                  .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Student>> GetStudentsByTeacherIdAsync(int teacherId, int offset, int limit)
        {
            if (teacherId <= 0) throw new ArgumentNullException(nameof(teacherId));

            //implement paging. order by to make sure same paging can occur without issues. can be improved to take order by values
            return await _context.Students
                .Include(s => s.Teacher)
                .Where(s => s.TeacherId == teacherId)
                .OrderBy(s => s.LastName)
                .ThenBy(s => s.FirstName)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<int> GetStudentCountByTeacherIdAsync(int teacherId)
        {
            return await _context.Students.CountAsync(s => s.TeacherId == teacherId);
        }

    }
}
