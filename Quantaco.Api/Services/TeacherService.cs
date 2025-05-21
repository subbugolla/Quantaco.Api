using System.Collections.Generic;
using Quantaco.Api.Services.Interfaces;
using Quantaco.DataAccess.Repositories.Interfaces;
using Quantaco.Models;

namespace Quantaco.Api.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<IEnumerable<TeacherModel>> GetAllAsync(int offset, int limit)
        {
            //skip logic in service layer to keep controllers free of business logic
            var teachers = await _teacherRepository.GetAllAsync(offset, limit);
            IList<TeacherModel> teachersList = new List<TeacherModel>();
            foreach (var item in teachers)
            {
                teachersList.Add(new TeacherModel()
                {
                    Id = item.Id,
                    Email = item.Email,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    StudentCount = item.Students.Count(),
                    Username = item.UserName,
                });
            }
            return teachersList;
        }

        public async Task<TeacherModel> GetByIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentNullException(nameof(id));

            var teacher = await _teacherRepository.GetByIdAsync(id);
            if (teacher == null)
                throw new InvalidOperationException("Teacher not found");

            return new TeacherModel()
            {
                Id = teacher.Id,
                Username = teacher.UserName,
                FirstName = teacher.FirstName,
                Email = teacher.Email,
                LastName = teacher.LastName,
                StudentCount = teacher.Students.Count(),
            };
        }

        public async Task<int> GetTeachersCountAsync()
        {
            return await _teacherRepository.GetTotalTeacherCountAsync();
        }
    }
}
