namespace BusinessBoom.Exceptions
{
    public class WalletNotFoundException : Exception
    {
        public WalletNotFoundException(string message) : base(message) { }
    }
}
