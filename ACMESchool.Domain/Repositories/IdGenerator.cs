using ACMESchool.Domain.Entities;
using Newtonsoft.Json;

namespace ACMESchool.Domain.Repositories
{
    public class IdGenerator
    {
        private readonly string _configFile;
        private int _lastStudentId;
        private int _lastCourseId;

        public IdGenerator(string configFile)
        {
            _configFile = configFile;
            LoadLastIds();
        }

        private void LoadLastIds()
        {
            if (File.Exists(_configFile))
            {
                string json = File.ReadAllText(_configFile);
                IdStorage storage = JsonConvert.DeserializeObject<IdStorage>(json);
                _lastStudentId = storage.LastStudentId;
                _lastCourseId = storage.LastCourseId;
            }
            else
            {
                _lastStudentId = 0;
                _lastCourseId = 0;
            }
        }

        public int GetNextStudentId()
        {
            int newId = Interlocked.Increment(ref _lastStudentId);
            SaveLastIds();
            return newId;
        }

        public int GetNextCourseId()
        {
            int newId = Interlocked.Increment(ref _lastCourseId);
            SaveLastIds();
            return newId;
        }

        private void SaveLastIds()
        {
            IdStorage storage = new IdStorage { LastStudentId = _lastStudentId, LastCourseId = _lastCourseId };
            string json = JsonConvert.SerializeObject(storage);
            File.WriteAllText(_configFile, json);
        }
    }
}
