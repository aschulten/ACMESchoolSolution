using ACMESchool.Domain.Services;

namespace ACMESchool.Tests.TestHelpers
{
    public class MockPaymentGateway : IPaymentGateway
    {
        public bool ProcessPayment(decimal amount)
        {
            return true;
        }
    }
}