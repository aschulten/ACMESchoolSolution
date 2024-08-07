using ACMESchool.Domain.Entities;

namespace ACMESchool.Domain.Repositories
{
    public interface IDataStoreManager
    {
        void SaveStudent(Student student);
        Student GetStudentById(int id);
        void DeleteStudent(int id);
        void UpdateStudent(Student student);
        List<Student> GetAllStudents();

        void SaveCourse(Course course);
        Course GetCourseById(int id);
        void DeleteCourse(int id);
        void UpdateCourse(Course course);
        List<Course> GetAllCourses();
    }
}
