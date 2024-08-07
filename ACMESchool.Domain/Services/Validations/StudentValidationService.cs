using ACMESchool.Domain.Entities;

namespace ACMESchool.Domain.Services.Validations
{

    public class StudentValidationService
    {
        public List<string> ValidateStudent(Student student)
        {
            var errors = new List<string>();
            if (string.IsNullOrEmpty(student.Name))
            {
                errors.Add("Student name is required");
            }

            if (student.Age < 18 )
            {
                errors.Add("Student must be at least 18 years old");
            }
            return errors;
        }
    }
}
