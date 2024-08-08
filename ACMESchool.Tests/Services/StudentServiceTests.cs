using ACMESchool.Domain.Repositories;
using ACMESchool.Domain.Services;
using ACMESchool.Tests.TestHelpers;
using Moq;
using Xunit;

namespace ACMESchool.Tests.Services
{
    public class StudentServiceTests
    {
        private readonly Mock<IStudentRepository> _studentRepositoryMock;
        private readonly StudentService _studentService;

        public StudentServiceTests()
        {
            _studentRepositoryMock = new Mock<IStudentRepository>();
            _studentService = new StudentService(_studentRepositoryMock.Object);
        }

        [Fact]
        public void SaveStudent_StudentIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _studentService.SaveStudent(null));
        }

        [Fact]
        public void UpdateStudent_StudentIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _studentService.UpdateStudent(null));
        }

        [Fact]
        public void DeleteStudent_IdIsZero_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _studentService.DeleteStudent(0));
        }

        [Fact]
        public void GetStudentById_IdIsZero_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _studentService.GetStudentById(0));
        }

        [Fact]
        public void SaveStudent_StudentIsValid_CallsSaveStudentOnRepository()
        {
            var student = MockData.GetMockStudent();

            _studentService.SaveStudent(student);

            _studentRepositoryMock.Verify(r => r.SaveStudent(student), Times.Once);
        }

        [Fact]
        public void UpdateStudent_StudentIsValid_CallsUpdateStudentOnRepository()
        {
            var student = MockData.GetMockStudent();

            _studentService.UpdateStudent(student);

            _studentRepositoryMock.Verify(r => r.UpdateStudent(student), Times.Once);
        }

        [Fact]
        public void DeleteStudent_IdIsValid_CallsDeleteStudentOnRepository()
        {
            var id = 1;

            _studentService.DeleteStudent(id);

            _studentRepositoryMock.Verify(r => r.DeleteStudent(id), Times.Once);
        }

        [Fact]
        public void GetStudentById_IdIsValid_ReturnsStudentFromRepository()
        {
            var id = 1;
            var student = MockData.GetMockStudent();
            _studentRepositoryMock.Setup(r => r.GetStudentById(id)).Returns(student);

            var result = _studentService.GetStudentById(id);

            Assert.Equal(student, result);
        }

        [Fact]
        public void GetAllStudents_ReturnsAllStudentsFromRepository()
        {
            var students = MockData.GetMockStudentsWithDifferentAges();
            _studentRepositoryMock.Setup(r => r.GetAllStudents()).Returns(students);

            var result = _studentService.GetAllStudents();

            Assert.Equal(students, result);
        }
    }
}
