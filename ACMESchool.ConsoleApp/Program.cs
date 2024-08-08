using ACMESchool.Domain.Repositories;
using ACMESchool.Domain.Services.Validations;
using ACMESchool.Domain.Services;
using ACMESchool.Tests.TestHelpers;

namespace ACMESchool.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string solutionDirectory = Directory.GetParent(currentDirectory).Parent.Parent.Parent.FullName;
            string dataFilePath = Path.Combine(solutionDirectory, "DataFile.txt");
            FileDataStoreManager dataStore = new FileDataStoreManager(dataFilePath);
            MockPaymentGateway mockPaymentGateway = new MockPaymentGateway();
            CourseValidationService courseValidationService = new CourseValidationService();
            CourseRepository courseRepository = new CourseRepository(dataStore);
            CourseService courseService = new CourseService(courseRepository, mockPaymentGateway);
            StudentValidationService studentValidationService = new StudentValidationService();
            StudentRepository studentRepository = new StudentRepository(dataStore);
            StudentService studentService = new StudentService(studentRepository);
            string idsFilePath = Path.Combine(solutionDirectory, "ids.txt");
            IdGenerator idGenerator = new IdGenerator(idsFilePath);
            
            var studentConsole = new StudentConsole(studentService, studentValidationService, idGenerator);
            var courseConsole = new CourseConsole(courseService, idGenerator, courseValidationService, studentService);
                        
            Console.WriteLine("Welcome to ACMESchool!");
            
            while (true)
            {
                Console.WriteLine("What do you want to work with? (1) Students, (2) Courses, (0) Exit ");
                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        studentConsole.Run();
                        break;
                    case "2":
                        courseConsole.Run();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }
    }
}
