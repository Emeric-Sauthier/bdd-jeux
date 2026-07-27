namespace JeuxLibrary.Mastermind.Exceptions
{
    public class NoSecretCodeException : Exception
    {
        public NoSecretCodeException() { }

        public NoSecretCodeException(string message) : base(message) { }
    }
}
