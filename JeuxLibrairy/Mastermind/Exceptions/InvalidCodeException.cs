namespace JeuxLibrary.Mastermind.Exceptions
{
    public class InvalidCodeException : Exception
    {
        public InvalidCodeException() { }
        public InvalidCodeException(string message) : base(message) { }
    }
}
