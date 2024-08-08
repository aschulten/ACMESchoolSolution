using ACMESchool.Domain.Entities;
using ACMESchool.Domain.Repositories;
using Newtonsoft.Json;
using Xunit;

namespace ACMESchool.Tests.Repositories
{
    public class IdGeneratorTests
    {
        private readonly string _configFile;

        public IdGeneratorTests()
        {
            _configFile = "idgenerator_test.json";
        }

        [Fact]
        public void LoadLastIds_FileExists_LoadsIds()
        {
            IdStorage storage = new IdStorage { LastStudentId = 10, LastCourseId = 20 };
            string json = JsonConvert.SerializeObject(storage);
            File.WriteAllText(_configFile, json);

            IdGenerator idGenerator = new IdGenerator(_configFile);

            Assert.Equal(11, idGenerator.GetNextStudentId());
            Assert.Equal(21, idGenerator.GetNextCourseId());
        }

        [Fact]
        public void LoadLastIds_FileDoesNotExist_SetsIdsToZero()
        {
            string tempFile = "tempFile";
            IdGenerator idGenerator = new IdGenerator(tempFile);
            try
            {
                Assert.Equal(1, idGenerator.GetNextStudentId());
                Assert.Equal(1, idGenerator.GetNextCourseId());
            }
            finally
            {
                File.Delete(tempFile);
            }
        }

        [Fact]
        public void GetNextStudentId_IncrementsId()
        {
            string tempFile = "tempFile";
            IdGenerator idGenerator = new IdGenerator(tempFile);
            try
            {
                Assert.Equal(1, idGenerator.GetNextStudentId());
                Assert.Equal(2, idGenerator.GetNextStudentId());
            }
            finally
            {
                File.Delete(tempFile);
            }
        }

        [Fact]
        public void GetNextCourseId_IncrementsId()
        {
            string tempFile = "tempFile";
            IdGenerator idGenerator = new IdGenerator(tempFile);
            try
            {
                Assert.Equal(1, idGenerator.GetNextCourseId());
                Assert.Equal(2, idGenerator.GetNextCourseId());
            }
            finally
            {
                File.Delete(tempFile);
            }
        }
    }
}
