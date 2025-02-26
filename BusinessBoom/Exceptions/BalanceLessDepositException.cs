namespace BusinessBoom.Exceptions
{
    public class BalanceLessDepositException : Exception
    {
        public BalanceLessDepositException(string message) : base(message)
        {
        }
    }
}