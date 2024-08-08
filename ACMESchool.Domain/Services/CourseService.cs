using ACMESchool.Domain.Entities;
using ACMESchool.Domain.Exceptions;
using ACMESchool.Domain.Repositories;

namespace ACMESchool.Domain.Services
{
    public class CourseService : ICourseRepository
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IPaymentGateway _paymentGateway;

        public CourseService(ICourseRepository courseRepository, IPaymentGateway paymentGateway)
        {
            _courseRepository = courseRepository;
            _paymentGateway = paymentGateway;
        }

        public void SaveCourse(Course course)
        {
            if (course != null)
            {
                _courseRepository.SaveCourse(course);
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public void UpdateCourse(Course course)
        {
            if (course != null)
            {
                _courseRepository.UpdateCourse(course);
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public List<Course> GetAllCourses()
        {
            return _courseRepository.GetAllCourses();
        }

        public Course GetCourseById(int id)
        {
            if (id != 0)
            {
                return _courseRepository.GetCourseById(id);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void DeleteCourse(int id)
        {
            _courseRepository.DeleteCourse(id);
        }

        public void AssignStudentToCourse(Student student, Course course)
        {
            if (course.Fee > 0)
            {
                ProcessCoursePayment(course.Fee);
            }

            course.Students.Add(student);
            UpdateCourse(course);
        }

        public void UnassignStudentFromCourse(Student student, Course course)
        {
            course.Students = course.Students.Where(s => s.Id != student.Id).ToList();
            UpdateCourse(course);
        }

        public List<Course> GetFilteredByDates(DateTime startDate, DateTime endDate)
        {
            return GetAllCourses()
                .Where(course => course.StartDate.Date >= startDate.Date && course.EndDate.Date <= endDate.Date)
                .ToList();
        }

        private void ProcessCoursePayment(decimal fee)
        {
            if (!_paymentGateway.ProcessPayment(fee))
            {
                throw new PaymentProcessingException("Payment could not be processed");
            }
        }
    }
}