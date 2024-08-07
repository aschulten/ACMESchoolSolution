namespace ACMESchool.Domain.Exceptions
{
    public class DataStoreException : Exception
    {
        public DataStoreException(string message, Exception innerException)
        : base($"{message}. Inner exception: {innerException.Message}", innerException)
        {
        }
    }
}
