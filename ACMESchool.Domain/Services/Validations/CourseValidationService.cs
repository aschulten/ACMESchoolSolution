using ACMESchool.Domain.Entities;

namespace ACMESchool.Domain.Services.Validations
{
    public class CourseValidationService
    {
        public List<string> ValidateCourse(Course course)
        {
            var errors = new List<string>();
            if (string.IsNullOrEmpty(course.Name))
            {
                errors.Add("Course name is required");
            }
            if (course.Fee < 0)
            {
                errors.Add("Course fee must be greater than or equal to zero.");
            }
            if (course.StartDate.Date > course.EndDate.Date)
            {
                errors.Add("Course end date must be greater than or equal to start date.");
            }
            // Removed this validation due PoC purpose
            //if (course.StartDate.Date < DateTime.UtcNow.Date)
            //{
            //    errors.Add("Course start date must be greater than or equal to today's date.");
            //}
            return errors;
        }

        public List<string> ValidateDatesFilter(DateTime startDate, DateTime endDate)
        {
            var errors = new List<string>();
            if (startDate > endDate)
            {
                errors.Add("End date must be equal to or greater than start date");
            }
            return errors;
        }
    }
}