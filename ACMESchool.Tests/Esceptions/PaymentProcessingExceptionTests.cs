using ACMESchool.Domain.Exceptions;
using Xunit;

namespace ACMESchool.Tests.Esceptions
{
    public class PaymentProcessingExceptionTests
    {
        [Fact]
        public void Constructor_Default_Message_Null()
        {
            var defaultMessage = "Exception of type 'ACMESchool.Domain.Exceptions.PaymentProcessingException' was thrown.";

            var exception = new PaymentProcessingException();

            Assert.Equal(defaultMessage, exception.Message);
        }

        [Fact]
        public void Constructor_Message_Set_Message()
        {
            var message = "Test message";
            var exception = new PaymentProcessingException(message);

            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void Constructor_Message_And_InnerException_Set_Message_And_InnerException()
        {
            var message = "Test message";
            var innerException = new Exception("Inner exception");
            var exception = new PaymentProcessingException(message, innerException);

            Assert.Equal(message, exception.Message);
            Assert.Equal(innerException, exception.InnerException);
        }
    }
}