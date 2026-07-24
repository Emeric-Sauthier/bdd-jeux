namespace JeuxLibrairy.TicTacToe.Exceptions
{
    public class CellOccupiedException : Exception
    {
        public CellOccupiedException() { }
        public CellOccupiedException(string message) : base(message) { }
    }
}
