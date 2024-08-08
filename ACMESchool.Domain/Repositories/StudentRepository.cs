using ACMESchool.Domain.Entities;

namespace ACMESchool.Domain.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IDataStoreManager _fileDataStore;

        public StudentRepository(IDataStoreManager dataStore)
        {
            _fileDataStore = dataStore;
        }

        public void SaveStudent(Student student)
        {
            _fileDataStore.SaveStudent(student);
        }

        public Student GetStudentById(int id)
        {
            return _fileDataStore.GetStudentById(id);
        }

        public void DeleteStudent(int id)
        {
            _fileDataStore.DeleteStudent(id);
        }

        public void UpdateStudent(Student student)
        {
            _fileDataStore.UpdateStudent(student);
        }

        public List<Student> GetAllStudents()
        {
            return _fileDataStore.GetAllStudents();
        }
    }
}