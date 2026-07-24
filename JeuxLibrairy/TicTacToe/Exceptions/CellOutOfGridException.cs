namespace JeuxLibrairy.TicTacToe.Exceptions
{
    public class CellOutOfGridException : Exception
    {
        public CellOutOfGridException() { }
        public CellOutOfGridException(string message) : base(message) { }
    }
}
