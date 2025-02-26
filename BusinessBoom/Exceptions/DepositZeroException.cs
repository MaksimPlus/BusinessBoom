namespace BusinessBoom.Exceptions
{
    public class DepositZeroException : Exception
    {
        public DepositZeroException(string message) : base(message) { }
    }
}
