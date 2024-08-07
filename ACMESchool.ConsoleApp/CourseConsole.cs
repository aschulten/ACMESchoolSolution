using ACMESchool.Domain.Entities;
using ACMESchool.Domain.Repositories;
using ACMESchool.Domain.Services;
using ACMESchool.Domain.Services.Validations;
using System;

namespace ACMESchool.ConsoleApp
{
    class CourseConsole
    {
        private readonly CourseService _courseService;
        private readonly CourseValidationService _courseValidationService;
        private readonly IdGenerator _idGenerator;
        private readonly StudentService _studentService;
        public CourseConsole(CourseService courseService, IdGenerator idGenerator, CourseValidationService courseValidationService, StudentService studentService)
        {
            _courseService = courseService;
            _idGenerator = idGenerator;
            _courseValidationService = courseValidationService;
            _studentService = studentService;
        }

        public void Run()
        {
            Console.WriteLine($"Select an option to do with courses (1) Create, (2) List, (3) Search, (4) Update, (5) Delete,\r\n(6) Assign student to course, (7) Unassign student from course, (8) List filtered by dates, (0) Exit");
            var option = Console.ReadLine();
            var op = option;
            while (true)
            {                
                switch (op)
                {
                    case "1":
                        CreateCourse();
                        break;
                    case "2":
                        ListCourses();
                        break;
                    case "3":
                        SearchCourse();
                        break;
                    case "4":
                        UpdateCourse();
                        break;
                    case "5":
                        DeleteCourse();
                        break;
                    case "6":
                        AssignStudentToCourse();
                        break;
                    case "7":
                        UnassignStudentFromCourse();
                        break;
                    case "8":
                        GetFilteredByDates();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid Option");
                        break;
                }
                Console.WriteLine($"Select an option to do with courses (1) Create, (2) List, (3) Search, (4) Update, (5) Delete,\r\n(6) Assign student to course, (7) Unassign student from course, (8) List filtered by dates, (0) Exit");
                op = Console.ReadLine();
            }
        }

        private void CreateCourse()
        {
            Console.WriteLine("Enter the course name:");
            var name = Console.ReadLine();
            Console.WriteLine("Enter the course fee:");
            var fee = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Enter the start date (yyyy-MM-dd):");
            var startDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter the end date (yyyy-MM-dd):");
            var endDate = DateTime.Parse(Console.ReadLine());
            var id = _idGenerator.GetNextCourseId();
            var course = new Course { Id = id,Name = name, Fee = fee, StartDate = startDate, EndDate = endDate };
            var errors = _courseValidationService.ValidateCourse(course);
            if (errors.Count != 0)
            {
                foreach (var error in errors)
                {
                    Console.WriteLine($"Error: {error}");
                }
            }
            else
            {
                _courseService.SaveCourse(course);
            }
        }

        private void ListCourses()
        {
            var courses = _courseService.GetAllCourses();
            foreach (var course in courses)
            {
                Console.WriteLine($"ID: {course.Id}, Name: {course.Name}, Fee: {course.Fee}, Start Date: {course.StartDate}, End Date: {course.EndDate}");
            }
        }

        private void SearchCourse()
        {
            Console.WriteLine("Enter the ID of the course to search:");
            var id = int.Parse(Console.ReadLine());
            var course = _courseService.GetCourseById(id);
            if (course != null)
            {
                Console.WriteLine($"ID: {course.Id}, Name: {course.Name}, Fee: {course.Fee}, Start Date: {course.StartDate}, End Date: {course.EndDate}");
            }
            else
            {
                Console.WriteLine("Course not found");
            }
        }

        private void UpdateCourse()
        {
            Console.WriteLine("Enter the ID of the course to update:");
            var id = int.Parse(Console.ReadLine());
            var course = _courseService.GetCourseById(id);
            if (course != null)
            {
                Console.WriteLine("Enter the new course name:");
                var name = Console.ReadLine();
                Console.WriteLine("Enter the new course fee:");
                var fee = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Enter the new start date (yyyy-MM-dd):");
                var startDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Enter the new end date (yyyy-MM-dd):");
                var endDate = DateTime.Parse(Console.ReadLine());

                course.Name = name;
                course.Fee = fee;
                course.StartDate = startDate;
                course.EndDate = endDate;
                var errors = _courseValidationService.ValidateCourse(course);
                if (errors.Count != 0)
                {
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Error: {error}");
                    }
                }
                else
                {
                    _courseService.UpdateCourse(course);
                }
            }
            else
            {
                Console.WriteLine("Course not found");
            }
        }

        private void DeleteCourse()
        {
            Console.WriteLine("Enter the ID of the course to delete:");
            var id = int.Parse(Console.ReadLine());
            _courseService.DeleteCourse(id);
        }
        public void AssignStudentToCourse()
        {
            Console.WriteLine("Enter the student Id:");
            var studentId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the course Id:");
            var courseId = int.Parse(Console.ReadLine());
            var student = _studentService.GetStudentById(studentId);
            var course = _courseService.GetCourseById(courseId);

            if (course == null || student == null)
            {
                Console.WriteLine("Course or student not found");
                return;
            }
            _courseService.AssignStudentToCourse(student, course);
            Console.WriteLine($"Student {student.Name} assigned to {course.Name} course successfully");
        }

        public void UnassignStudentFromCourse()
        {
            Console.WriteLine("Enter the student Id:");
            var studentId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the course Id:");
            var courseId = int.Parse(Console.ReadLine());
            var course = _courseService.GetCourseById(courseId);
            var student = _studentService.GetStudentById(studentId);

            if (course == null || student == null)
            {
                Console.WriteLine("Course or student not found");
                return;
            }

            _courseService.UnassignStudentFromCourse(student, course);
            Console.WriteLine($"Student {student.Name} unassigned to {course.Name} course successfully");
        }

        public void GetFilteredByDates()
        {
            Console.WriteLine("Enter start date (yyyy-MM-dd):");
            DateTime startDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter end date (yyyy-MM-dd):");
            DateTime endDate = DateTime.Parse(Console.ReadLine());
            var errors = _courseValidationService.ValidateDatesFilter(startDate, endDate);
            if (errors.Count != 0)
            {
                foreach (var error in errors)
                {
                    Console.WriteLine($"Error: {error}");
                }
            }
            else
            {
                var courses = _courseService.GetFilteredByDates(startDate, endDate);

                if (courses.Any())
                {                    
                    foreach (var course in courses)
                    {
                        Console.WriteLine("Courses found:");
                        Console.WriteLine($"ID: {course.Id}, Name: {course.Name}, Start date: {course.StartDate}, End date: {course.EndDate}");
                        Console.WriteLine("Students:");
                        if (course.Students.Any())
                        {
                            foreach (var student in course.Students)
                            {
                                Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Age: {student.Age}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No students assigned to the course");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No course found for selected dates");
                }
            }
        }
    }
}