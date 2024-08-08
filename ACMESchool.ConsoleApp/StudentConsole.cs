using ACMESchool.Domain.Entities;
using ACMESchool.Domain.Repositories;
using ACMESchool.Domain.Services;
using ACMESchool.Domain.Services.Validations;

namespace ACMESchool.ConsoleApp
{
    internal class StudentConsole
    {
        private readonly StudentService _studentService;
        private readonly StudentValidationService _studentValidationService;
        private readonly IdGenerator _idGenerator;

        public StudentConsole(StudentService studentService, StudentValidationService studentValidationService, IdGenerator idGenerator)
        {
            _studentService = studentService;
            _studentValidationService = studentValidationService;
            _idGenerator = idGenerator;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("Select an option to do with students (1) Create, (2) List, (3) Search, (4) Update, (5) Delete, (0) Exit");
                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        CreateStudent();
                        break;

                    case "2":
                        ListStudents();
                        break;

                    case "3":
                        SearchStudent();
                        break;

                    case "4":
                        UpdateStudent();
                        break;

                    case "5":
                        DeleteStudent();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }

        private void CreateStudent()
        {
            Console.WriteLine("Enter students name:");
            var name = Console.ReadLine();
            Console.WriteLine("Enter students age:");
            var age = int.Parse(Console.ReadLine());
            var id = _idGenerator.GetNextStudentId();
            var student = new Student { Id = id, Name = name, Age = age };
            var errors = _studentValidationService.ValidateStudent(student);
            if (errors.Count != 0)
            {
                foreach (var error in errors)
                {
                    Console.WriteLine($"Error: {error}");
                }
            }
            else
            {
                _studentService.SaveStudent(student);
            }
        }

        private void ListStudents()
        {
            var students = _studentService.GetAllStudents();
            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Age: {student.Age}");
            }
        }

        private void SearchStudent()
        {
            Console.WriteLine("Enter the ID of the student to search:");
            var id = int.Parse(Console.ReadLine());
            var student = _studentService.GetStudentById(id);
            if (student != null)
            {
                Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Age: {student.Age}");
            }
            else
            {
                Console.WriteLine("Student not found");
            }
        }

        private void UpdateStudent()
        {
            Console.WriteLine("Enter the ID of the student to update:");
            var id = int.Parse(Console.ReadLine());
            var student = _studentService.GetStudentById(id);
            if (student != null)
            {
                Console.WriteLine("Enter new students name:");
                var name = Console.ReadLine();
                Console.WriteLine("Enter new students age:");
                var age = int.Parse(Console.ReadLine());

                student.Name = name;
                student.Age = age;

                var errors = _studentValidationService.ValidateStudent(student);
                if (errors.Count != 0)
                {
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Error: {error}");
                    }
                }
                else
                {
                    _studentService.UpdateStudent(student);
                }
            }
            else
            {
                Console.WriteLine("Student not found");
            }
        }

        private void DeleteStudent()
        {
            Console.WriteLine("Enter the ID of the student to delete:");
            var id = int.Parse(Console.ReadLine());
            var student = _studentService.GetStudentById(id);
            if (student != null)
            {
                _studentService.DeleteStudent(id);
            }
            else
            {
                Console.WriteLine("Student not found");
            }
        }
    }
}