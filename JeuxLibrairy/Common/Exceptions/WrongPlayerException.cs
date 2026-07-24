namespace JeuxLibrairy.Common.Exceptions
{
    public class WrongPlayerException : Exception
    {
        public WrongPlayerException() { }
        public WrongPlayerException(string message) : base(message) { }
    }
}
