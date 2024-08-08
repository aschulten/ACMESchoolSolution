using ACMESchool.Domain.Exceptions;
using Xunit;

namespace ACMESchool.Tests.Esceptions
{
    public class DataStoreExceptionTests
    {
        [Fact]
        public void DataStoreException_MessageAndInnerException_SetCorrectly()
        {
            string message = "Data Error";
            Exception innerException = new Exception("Inner exception 2");

            DataStoreException exception = new DataStoreException(message, innerException);

            Assert.Equal($"Data Error. Inner exception: Inner exception 2", exception.Message);
            Assert.Same(innerException, exception.InnerException);
        }

        [Fact]
        public void DataStoreException_NullInnerException_ThrowsArgumentNullException()
        {
            Assert.Throws<NullReferenceException>(() => new DataStoreException("Data Error", null));
        }
    }
}