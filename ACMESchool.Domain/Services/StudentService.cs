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
            _studentRepository.SaveStudent(student);
        }
        public void UpdateStudent(Student student)
        {
            _studentRepository.UpdateStudent(student);
        }
        public void DeleteStudent(int id)
        {
            _studentRepository.DeleteStudent(id);
        }
        public Student GetStudentById(int id)
        {
            return _studentRepository.GetStudentById(id);
        }

        public List<Student> GetAllStudents()
        {
            return _studentRepository.GetAllStudents();
        }
    }
}
