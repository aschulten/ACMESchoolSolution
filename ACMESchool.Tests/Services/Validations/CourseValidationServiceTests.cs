using ACMESchool.Domain.Services.Validations;
using Xunit;
using ACMESchool.Tests.TestHelpers;

namespace ACMESchool.Tests.Services.Validations
{
    public class CourseValidationServiceTests
    {
        [Fact]
        public void ValidateCourse_NameIsRequired_ReturnsError()
        {
            var course = MockData.GetMockCourse();
            course.Name = null;

            var errors = new CourseValidationService().ValidateCourse(course);

            Assert.Single(errors);
            Assert.Contains("Course name is required", errors);
        }

        [Fact]
        public void ValidateCourse_FeeIsNegative_ReturnsError()
        {
            var course = MockData.GetMockCourse();
            course.Fee = -1;

            var errors = new CourseValidationService().ValidateCourse(course);

            Assert.Single(errors);
            Assert.Contains("Course fee must be greater than or equal to zero.", errors);
        }

        [Fact]
        public void ValidateCourse_StartDateIsAfterEndDate_ReturnsError()
        {
            var course = MockData.GetMockCourse();
            course.StartDate = DateTime.Now.AddDays(1);
            course.EndDate = DateTime.Now;

            var errors = new CourseValidationService().ValidateCourse(course);

            Assert.Single(errors);
            Assert.Contains("Course end date must be greater than or equal to start date.", errors);
        }

        [Fact]
        public void ValidateCourse_NoErrors_ReturnsEmptyList()
        {
            var course = MockData.GetMockCourse();

            var errors = new CourseValidationService().ValidateCourse(course);

            Assert.Empty(errors);
        }


        [Fact]
        public void ValidateDatesFilter_StartDateIsAfterEndDate_ReturnsError()
        {
            var startDate = DateTime.Now.AddDays(1);
            var endDate = DateTime.Now;

            var errors = new CourseValidationService().ValidateDatesFilter(startDate, endDate);

            Assert.Single(errors);
            Assert.Contains("End date must be equal to or greater than start date", errors);
        }

    }
}
