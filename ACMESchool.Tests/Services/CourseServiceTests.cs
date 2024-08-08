using ACMESchool.Domain.Entities;
using ACMESchool.Domain.Exceptions;
using ACMESchool.Domain.Repositories;
using ACMESchool.Domain.Services;
using ACMESchool.Tests.TestHelpers;
using Moq;
using Xunit;

namespace ACMESchool.Tests.Services
{
    public class CourseServiceTests
    {
        private readonly Mock<ICourseRepository> _courseRepositoryMock;
        private readonly CourseService _courseService;
        private readonly MockPaymentGateway _paymentGatewayMock;

        public CourseServiceTests()
        {
            _courseRepositoryMock = new Mock<ICourseRepository>();
            _paymentGatewayMock = new MockPaymentGateway();
            _courseService = new CourseService(_courseRepositoryMock.Object, _paymentGatewayMock);
        }

        [Fact]
        public void SaveCourse_CourseIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _courseService.SaveCourse(null));
        }

        [Fact]
        public void UpdateCourse_CourseIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _courseService.UpdateCourse(null));
        }

        [Fact]
        public void GetCourseById_IdIsZero_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _courseService.GetCourseById(0));
        }

        [Fact]
        public void SaveCourse_CourseIsValid_CallsSaveCourseOnRepository()
        {
            var course = MockData.GetMockCourse();

            _courseService.SaveCourse(course);

            _courseRepositoryMock.Verify(r => r.SaveCourse(course), Times.Once);
        }

        [Fact]
        public void UpdateCourse_CourseIsValid_CallsUpdateCourseOnRepository()
        {
            var course = MockData.GetMockCourse();

            _courseService.UpdateCourse(course);

            _courseRepositoryMock.Verify(r => r.UpdateCourse(course), Times.Once);
        }

        [Fact]
        public void GetCourseById_IdIsValid_ReturnsCourseFromRepository()
        {
            var courseId = 1;
            var course = MockData.GetMockCourse();
            _courseRepositoryMock.Setup(r => r.GetCourseById(courseId)).Returns(course);

            var result = _courseService.GetCourseById(courseId);

            Assert.Equal(course, result);
        }

        [Fact]
        public void GetAllCourses_ReturnsAllCoursesFromRepository()
        {
            var courses = MockData.GetMockCoursesWithStudents();
            _courseRepositoryMock.Setup(r => r.GetAllCourses()).Returns(courses);

            var result = _courseService.GetAllCourses();

            Assert.Equal(courses, result);
        }

        [Fact]
        public void DeleteCourse_IdIsValid_CallsDeleteCourseOnRepository()
        {
            var courseId = 1;

            _courseService.DeleteCourse(courseId);

            _courseRepositoryMock.Verify(r => r.DeleteCourse(courseId), Times.Once);
        }

        [Fact]
        public void AssignStudentToCourse_StudentAndCourseAreValid_CallsUpdateCourseOnRepository()
        {
            var student = MockData.GetMockStudent();
            var course = MockData.GetMockCourse();

            _courseService.AssignStudentToCourse(student, course);

            _courseRepositoryMock.Verify(r => r.UpdateCourse(course), Times.Once);
        }

        [Fact]
        public void UnassignStudentFromCourse_StudentAndCourseAreValid_CallsUpdateCourseOnRepository()
        {
            var student = MockData.GetMockStudent();
            var course = MockData.GetMockCourse();

            _courseService.UnassignStudentFromCourse(student, course);

            _courseRepositoryMock.Verify(r => r.UpdateCourse(course), Times.Once);
        }

        [Fact]
        public void GetFilteredByDates_StartAndEndDatesAreValid_ReturnsFilteredCourses()
        {
            var startDate = DateTime.Now.AddDays(-1);
            var endDate = DateTime.Now.AddDays(1);
            var courses = new List<Course>
            {
                new Course { Name = "Test Course 1", StartDate = startDate, EndDate = endDate },
                new Course { Name = "Test Course 2", StartDate = startDate.AddDays(-1), EndDate = endDate.AddDays(-1) }
            };
            _courseRepositoryMock.Setup(r => r.GetAllCourses()).Returns(courses);

            var result = _courseService.GetFilteredByDates(startDate, endDate);

            Assert.Single(result);
            Assert.Equal(courses[0], result[0]);
        }

        [Fact]
        public void AssignStudentToCourse_PaymentFails_ThrowsPaymentProcessingException()
        {
            var student = MockData.GetMockStudent();
            var course = MockData.GetMockCourse();
            var paymentGatewayMock = new Mock<IPaymentGateway>();
            paymentGatewayMock.Setup(pg => pg.ProcessPayment(course.Fee)).Returns(false);
            var courseService = new CourseService(_courseRepositoryMock.Object, paymentGatewayMock.Object);

            Assert.Throws<PaymentProcessingException>(() => courseService.AssignStudentToCourse(student, course));
        }
    }
}