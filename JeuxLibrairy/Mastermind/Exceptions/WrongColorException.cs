namespace JeuxLibrary.Mastermind.Exceptions
{
    public class WrongColorException : Exception
    {
        public WrongColorException() { }
        public WrongColorException(string message) : base(message) { }
    }
}
