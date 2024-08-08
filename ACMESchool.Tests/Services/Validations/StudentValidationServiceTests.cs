using ACMESchool.Domain.Entities;
using ACMESchool.Domain.Services.Validations;
using Xunit;

namespace ACMESchool.Tests.Services.Validations
{
    public class StudentValidationServiceTests
    {
        [Fact]
        public void ValidateStudent_NameIsRequired_ReturnsError()
        {
            var student = new Student { Id = 1, Name = null, Age = 18 };

            var errors = new StudentValidationService().ValidateStudent(student);

            Assert.Single(errors);
            Assert.Contains("Student name is required", errors);
        }

        [Fact]
        public void ValidateStudent_StudentMustBeAtLeast18YearsOld_ReturnsError()
        {
            var student = new Student { Id = 1, Name = "John", Age = 17 };

            var errors = new StudentValidationService().ValidateStudent(student);

            Assert.Single(errors);
            Assert.Contains("Student must be at least 18 years old", errors);
        }

        [Fact]
        public void ValidateStudent_AllErrors_ReturnsError()
        {
            var student = new Student { Id = 1, Name = null, Age = 17 };

            var errors = new StudentValidationService().ValidateStudent(student);

            Assert.Equal(2, errors.Count);
            Assert.Contains("Student must be at least 18 years old", errors);
            Assert.Contains("Student name is required", errors);
        }

        [Fact]
        public void ValidateStudent_NoErrors_ReturnsEmptyList()
        {
            var student = new Student { Id = 1, Name = "John", Age = 20 };

            var errors = new StudentValidationService().ValidateStudent(student);

            Assert.Empty(errors);
        }
    }
}
