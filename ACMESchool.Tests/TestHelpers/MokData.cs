using ACMESchool.Domain.Entities;

namespace ACMESchool.Tests.TestHelpers
{
    public static class MockData
    {
        public static Course GetMockCourse()
        {
            return new Course
            {
                Id = 1,
                Name = "Mock Course",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(15),
                Fee = 100,
                Students = []
            };
        }

        public static List<Course> GetMockCoursesWithStudents()
        {
            var students = new List<Student>
            {
                new Student { Id = 1, Name = "Mock Student 1" },
                new Student { Id = 2, Name = "Mock Student 2" },
                new Student { Id = 3, Name = "Mock Student 3" }
            };

            return new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name = "Mock Course 1",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(15),
                    Fee = 100,
                    Students = students.Take(2).ToList()
                },
                new Course
                {
                    Id = 2,
                    Name = "Mock Course 2",
                    StartDate = DateTime.Now.AddDays(16),
                    EndDate = DateTime.Now.AddDays(30),
                    Fee = 200,
                    Students = students.Skip(1).Take(2).ToList()
                },
                new Course
                {
                    Id = 3,
                    Name = "Mock Course 3",
                    StartDate = DateTime.Now.AddDays(31),
                    EndDate = DateTime.Now.AddDays(45),
                    Fee = 300,
                    Students = students.Skip(2).ToList()
                }
            };
        }

        public static Student GetMockStudent()
        {
            return new Student
            {
                Id = 1,
                Name = "Mock Student",
                Age = 20
            };
        }

        public static List<Student> GetMockStudentsWithDifferentAges()
        {
            return new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name = "Mock Student 1",
                    Age = 18
                },
                new Student
                {
                    Id = 2,
                    Name = "Mock Student 2",
                    Age = 25
                },
                new Student
                {
                    Id = 3,
                    Name = "Mock Student 3",
                    Age = 30
                }
            };
        }
    }
}