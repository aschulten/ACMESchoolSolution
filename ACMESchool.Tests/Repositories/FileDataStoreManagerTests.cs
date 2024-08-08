using ACMESchool.Domain.Entities;
using ACMESchool.Domain.Exceptions;
using ACMESchool.Domain.Repositories;
using Newtonsoft.Json.Linq;
using Xunit;

namespace ACMESchool.Tests.Repositories
{
    public class FileDataStoreManagerTests : IDisposable
    {
        private readonly string _filePath;
        private FileStream _fileStream;

        public FileDataStoreManagerTests()
        {
            _filePath = "fileDataStoreManagerTest.json";
            _fileStream = new FileStream(_filePath, FileMode.Create);
        }

        [Fact]
        public void SaveEntity_EntityIsValid_SavesEntity()
        {
            Dispose();

            FileDataStoreManager dataStore = new FileDataStoreManager(_filePath);
            Student student = new Student { Id = 1, Name = "John Doe", Age = 33 };

            dataStore.SaveStudent(student);

            foreach (string line in File.ReadLines(_filePath))
            {
                JObject jsonObject = JObject.Parse(line);
                Assert.Equal("Student", jsonObject["Type"]?.ToString());
                Assert.Equal(1, jsonObject["Entity"]["Id"]?.ToObject<int>());
                Assert.Equal("John Doe", jsonObject["Entity"]["Name"]?.ToString());
                Assert.Equal(33, jsonObject["Entity"]["Age"]?.ToObject<int>());
            }
        }

        [Fact]
        public void GetEntityById_IdIsValid_ReturnsEntity()
        {
            Dispose();
            FileDataStoreManager dataStore = new FileDataStoreManager(_filePath);
            Student student = new Student { Id = 1, Name = "John Doe" };
            dataStore.SaveStudent(student);

            Student result = dataStore.GetStudentById(1);

            Assert.Equal(1, result.Id);
            Assert.Equal("John Doe", result.Name);
        }

        [Fact]
        public void DeleteEntity_IdIsValid_DeletesEntity()
        {
            Dispose();
            FileDataStoreManager dataStore = new FileDataStoreManager(_filePath);
            Student student = new Student { Id = 1, Name = "John Doe" };
            dataStore.SaveStudent(student);

            dataStore.DeleteStudent(1);

            string json = File.ReadAllText(_filePath);
            Assert.Empty(json);
        }

        [Fact]
        public void UpdateEntity_EntityIsValid_UpdatesEntity()
        {
            Dispose();
            FileDataStoreManager dataStore = new FileDataStoreManager(_filePath);
            Student student = new Student { Id = 1, Name = "John Doe" };
            dataStore.SaveStudent(student);
            student.Name = "Jane Doe";

            dataStore.UpdateStudent(student);

            foreach (string line in File.ReadLines(_filePath))
            {
                JObject jsonObject = JObject.Parse(line);
                Assert.Equal("Student", jsonObject["Type"]?.ToString());

                JToken entity = jsonObject["Entity"];
                if (entity is JObject)
                {
                    Assert.Equal(1, ((JObject)entity)["Id"]?.ToObject<int>());
                    Assert.Equal("Jane Doe", ((JObject)entity)["Name"]?.ToString());
                }
            }
        }

        [Fact]
        public void GetAllEntities_ReturnsAllEntities()
        {
            Dispose();
            FileDataStoreManager dataStore = new FileDataStoreManager(_filePath);
            Student student1 = new Student { Id = 1, Name = "John Doe" };
            Student student2 = new Student { Id = 2, Name = "Jane Doe" };
            dataStore.SaveStudent(student1);
            dataStore.SaveStudent(student2);

            List<Student> result = dataStore.GetAllStudents();

            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].Id);
            Assert.Equal("John Doe", result[0].Name);
            Assert.Equal(2, result[1].Id);
            Assert.Equal("Jane Doe", result[1].Name);
        }

        [Fact]
        public void SaveCourse_CourseIsValid_SavesCourse()
        {
            Dispose();
            FileDataStoreManager dataStore = new FileDataStoreManager(_filePath);
            Course course = new Course { Id = 1, Name = "Math" };

            dataStore.SaveCourse(course);

            string json = File.ReadAllText(_filePath);
            JObject jsonObject = JObject.Parse(json);
            Assert.Equal("Course", jsonObject["Type"]?.ToString());
            Assert.Equal(1, jsonObject["Entity"]["Id"]?.ToObject<int>());
            Assert.Equal("Math", jsonObject["Entity"]["Name"]?.ToString());
        }

        [Fact]
        public void GetCourseById_IdIsValid_ReturnsCourse()
        {
            Dispose();
            FileDataStoreManager dataStore = new FileDataStoreManager(_filePath);
            Course course = new Course { Id = 1, Name = "Math" };
            dataStore.SaveCourse(course);

            Course result = dataStore.GetCourseById(1);

            Assert.Equal(1, result.Id);
            Assert.Equal("Math", result.Name);
        }

        [Fact]
        public void GetCourseById_IdIsInvalid_ThrowsException()
        {
            Dispose();
            FileDataStoreManager dataStore = new FileDataStoreManager(_filePath);

            Assert.Throws<DataStoreException>(() => dataStore.GetCourseById(1));
        }

        [Fact]
        public void DeleteCourse_IdIsValid_DeletesCourse()
        {
            Dispose();
            FileDataStoreManager dataStore = new FileDataStoreManager(_filePath);
            Course course = new Course { Id = 1, Name = "Math" };
            dataStore.SaveCourse(course);

            dataStore.DeleteCourse(1);

            string json = File.ReadAllText(_filePath);
            Assert.Empty(json);
        }

        [Fact]
        public void DeleteCourse_IdIsInvalid_ThrowsException()
        {
            Dispose();
            FileDataStoreManager dataStore = new FileDataStoreManager(_filePath);

            Assert.Throws<DataStoreException>(() => dataStore.DeleteCourse(1));
        }

        [Fact]
        public void UpdateCourse_CourseIsValid_UpdatesCourse()
        {
            Dispose();
            FileDataStoreManager dataStore = new FileDataStoreManager(_filePath);
            Course course = new Course { Id = 1, Name = "Math" };
            dataStore.SaveCourse(course);
            course.Name = "Science";

            dataStore.UpdateCourse(course);

            string json = File.ReadAllText(_filePath);
            JObject jsonObject = JObject.Parse(json);
            Assert.Equal("Course", jsonObject["Type"]?.ToString());
            Assert.Equal(1, jsonObject["Entity"]["Id"]?.ToObject<int>());
            Assert.Equal("Science", jsonObject["Entity"]["Name"]?.ToString());
        }

        [Fact]
        public void GetAllCourses_ReturnsAllCourses()
        {
            Dispose();
            FileDataStoreManager dataStore = new FileDataStoreManager(_filePath);
            Course course1 = new Course { Id = 1, Name = "Math" };
            Course course2 = new Course { Id = 2, Name = "Science" };
            dataStore.SaveCourse(course1);
            dataStore.SaveCourse(course2);

            List<Course> result = dataStore.GetAllCourses();

            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].Id);
            Assert.Equal("Math", result[0].Name);
            Assert.Equal(2, result[1].Id);
            Assert.Equal("Science", result[1].Name);
        }

        public void Dispose()
        {
            _fileStream.Close();
            _fileStream.Dispose();
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }
    }
}