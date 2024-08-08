namespace ACMESchool.Domain.Services
{
    public interface IPaymentGateway
    {
        bool ProcessPayment(decimal amount);
    }
}