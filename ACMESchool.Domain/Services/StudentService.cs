using ACMESchool.Domain.Entities;
using ACMESchool.Domain.Repositories;

namespace ACMESchool.Domain.Services
{
    public class StudentService : IStudentRepository
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public void SaveStudent(Student student)
        {
            if (student != null)
            {
                _studentRepository.SaveStudent(student);
            }
            else
            {
                throw new ArgumentNullException(nameof(student));
            }
        }
        public void UpdateStudent(Student student)
        {
            if (student != null)
            {
                _studentRepository.UpdateStudent(student);
            }
            else
            {
                throw new ArgumentNullException(nameof(student));
            }
        }
        public void DeleteStudent(int id)
        {
            if (id != 0)
            {
                _studentRepository.DeleteStudent(id);
            }
            else
            {
                throw new ArgumentException();
            }
        }
        public Student GetStudentById(int id)
        {
            if (id != 0)
            {
                return _studentRepository.GetStudentById(id);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public List<Student> GetAllStudents()
        {
            return _studentRepository.GetAllStudents();
        }
    }
}
