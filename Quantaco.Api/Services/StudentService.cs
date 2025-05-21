using Quantaco.Api.Services.Interfaces;
using Quantaco.DataAccess.Entities;
using Quantaco.DataAccess.Repositories.Interfaces;
using Quantaco.Models;

namespace Quantaco.Api.Services
{
    public class StudentService : IStudentService
    {

        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;

        public StudentService(
        IStudentRepository studentRepository,
        ITeacherRepository teacherRepository)
        {
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
        }

        public async Task<StudentModel> CreateStudentAsync(int teacherId, CreateStudentModel createStudent)
        {
            if (teacherId <= 0) throw new ArgumentNullException(nameof(teacherId));

            var teacher = await _teacherRepository.GetByIdAsync(teacherId);

            if (teacher == null)
                throw new InvalidOperationException("Teacher not found");

            var student = new Student
            {
                FirstName = createStudent.FirstName,
                LastName = createStudent.LastName,
                Email = createStudent.Email,
                TeacherId = teacherId
            };

            await _studentRepository.AddStudentAsync(student);
            
            return new StudentModel()
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                TeacherId = teacherId
            };
        }

        public async Task DeleteStudentAsync(int teacherId, int studentId)
        {
            if (teacherId <= 0 || studentId <= 0) throw new ArgumentNullException("Invalid data");

            var student = await _studentRepository.GetStudentByIdAsync(studentId);

            if (student == null)
                throw new InvalidOperationException("Student doesn't exist");

            if (student.TeacherId != teacherId)
                throw new InvalidOperationException("Unauthorized to delete the student");

            await _studentRepository.DeleteStudentAsync(student);
        }

        public async Task<StudentModel> GetStudentByIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentNullException(nameof(id));

            var student = await _studentRepository.GetStudentByIdAsync(id);

            if (student == null)
                throw new InvalidOperationException("Student doesn't exist");

            return new StudentModel()
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                TeacherId = student.TeacherId
            };
        }

        public async Task<IEnumerable<StudentModel>> GetStudentByTeacherIdAsync(int teacherId, int offset, int limit)
        {
            if (teacherId <= 0) throw new ArgumentNullException(nameof(teacherId));

            var students = await _studentRepository.GetStudentsByTeacherIdAsync(teacherId, offset, limit);
            IList<StudentModel> studentList = new List<StudentModel>();
            foreach (var student in students)
            {
                studentList.Add(new StudentModel() 
                { 
                    Email = student.Email,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    TeacherId=student.TeacherId,
                    Id = student.Id
                });
            }
            return studentList;
        }

        public async Task<int> GetStudentCountByTeacherIdAsync(int teacherId)
        {
            if (teacherId <= 0) throw new ArgumentNullException(nameof(teacherId));

            return await _studentRepository.GetStudentCountByTeacherIdAsync(teacherId);
        }
    }
}
