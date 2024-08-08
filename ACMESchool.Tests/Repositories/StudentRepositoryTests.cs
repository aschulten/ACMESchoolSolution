using ACMESchool.Domain.Repositories;
using ACMESchool.Tests.TestHelpers;
using Moq;
using Xunit;

namespace ACMESchool.Tests.Repositories
{
    public class StudentRepositoryTests
    {
        private readonly Mock<IDataStoreManager> _dataStoreMock;
        private readonly StudentRepository _studentRepository;

        public StudentRepositoryTests()
        {
            _dataStoreMock = new Mock<IDataStoreManager>();
            _studentRepository = new StudentRepository(_dataStoreMock.Object);
        }

        [Fact]
        public void SaveStudent_StudentIsValid_SavesStudent()
        {
            var student = MockData.GetMockStudent();
            _studentRepository.SaveStudent(student);
            _dataStoreMock.Verify(r => r.SaveStudent(student), Times.Once);
        }

        [Fact]
        public void GetStudentById_IdIsValid_ReturnsStudent()
        {
            var id = 1;
            var student = MockData.GetMockStudent();
            _dataStoreMock.Setup(r => r.GetStudentById(id)).Returns(student);
            var result = _studentRepository.GetStudentById(id);
            Assert.Equal(student, result);
        }

        [Fact]
        public void DeleteStudent_IdIsValid_DeletesStudent()
        {
            var id = 1;
            _studentRepository.DeleteStudent(id);
            _dataStoreMock.Verify(r => r.DeleteStudent(id), Times.Once);
        }

        [Fact]
        public void UpdateStudent_StudentIsValid_UpdatesStudent()
        {
            var student = MockData.GetMockStudent();
            _studentRepository.UpdateStudent(student);
            _dataStoreMock.Verify(r => r.UpdateStudent(student), Times.Once);
        }

        [Fact]
        public void GetAllStudents_ReturnsAllStudents()
        {
            var students = MockData.GetMockStudentsWithDifferentAges();
            _dataStoreMock.Setup(r => r.GetAllStudents()).Returns(students);
            var result = _studentRepository.GetAllStudents();
            Assert.Equal(students, result);
        }
    }
}
