using ACMESchool.Domain.Entities;

namespace ACMESchool.Domain.Repositories
{
    public interface ICourseRepository
    {
        void SaveCourse(Course course);
        Course GetCourseById(int id);
        void DeleteCourse(int id);
        void UpdateCourse(Course course);
        List<Course> GetAllCourses();
    }
}