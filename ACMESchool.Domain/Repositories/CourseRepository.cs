using ACMESchool.Domain.Entities;

namespace ACMESchool.Domain.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly FileDataStoreManager _fileDataStore;

        public CourseRepository(FileDataStoreManager dataStore)
        {
            _fileDataStore = dataStore;
        }

        public void SaveCourse(Course course)
        {
            _fileDataStore.SaveCourse(course);
        }

        public Course GetCourseById(int id)
        {
            return _fileDataStore.GetCourseById(id);
        }

        public void DeleteCourse(int id)
        {
            _fileDataStore.DeleteCourse(id);
        }

        public void UpdateCourse(Course course)
        {
            _fileDataStore.UpdateCourse(course);
        }

        public List<Course> GetAllCourses()
        {
            return _fileDataStore.GetAllCourses();
        }
    }
}
