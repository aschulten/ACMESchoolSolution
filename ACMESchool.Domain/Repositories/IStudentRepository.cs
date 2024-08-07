using ACMESchool.Domain.Entities;

namespace ACMESchool.Domain.Repositories
{
    public interface IStudentRepository
    {
        void SaveStudent(Student student);
        Student GetStudentById(int id);
        void DeleteStudent(int id);
        void UpdateStudent(Student student);
        List<Student> GetAllStudents();

    }
}