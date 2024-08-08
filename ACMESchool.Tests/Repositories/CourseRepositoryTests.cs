using ACMESchool.Domain.Repositories;
using ACMESchool.Tests.TestHelpers;
using Moq;
using Xunit;

namespace ACMESchool.Tests.Repositories
{
    public class CourseRepositoryTests
    {
        private readonly Mock<IDataStoreManager> _dataStoreMock;
        private readonly CourseRepository _courseRepository;

        public CourseRepositoryTests()
        {
            _dataStoreMock = new Mock<IDataStoreManager>();
            _courseRepository = new CourseRepository(_dataStoreMock.Object);
        }

        [Fact]
        public void SaveCourse_CourseIsValid_SavesCourse()
        {
            var course = MockData.GetMockCourse();

            _courseRepository.SaveCourse(course);

            _dataStoreMock.Verify(r => r.SaveCourse(course), Times.Once);
        }

        [Fact]
        public void GetCourseById_IdIsValid_ReturnsCourse()
        {
            var id = 1;
            var course = MockData.GetMockCourse();
            _dataStoreMock.Setup(r => r.GetCourseById(id)).Returns(course);

            var result = _courseRepository.GetCourseById(id);

            Assert.Equal(course, result);
        }

        [Fact]
        public void DeleteCourse_IdIsValid_DeletesCourse()
        {
            var id = 1;

            _courseRepository.DeleteCourse(id);

            _dataStoreMock.Verify(r => r.DeleteCourse(id), Times.Once);
        }

        [Fact]
        public void UpdateCourse_CourseIsValid_UpdatesCourse()
        {
            var course = MockData.GetMockCourse();

            _courseRepository.UpdateCourse(course);

            _dataStoreMock.Verify(r => r.UpdateCourse(course), Times.Once);
        }

        [Fact]
        public void GetAllCourses_ReturnsAllCourses()
        {
            var courses = MockData.GetMockCoursesWithStudents();
            _dataStoreMock.Setup(r => r.GetAllCourses()).Returns(courses);

            var result = _courseRepository.GetAllCourses();

            Assert.Equal(courses, result);
        }
    }
}