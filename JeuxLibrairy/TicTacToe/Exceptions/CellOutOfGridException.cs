using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuxLibrairy.TicTacToe.Exceptions
{
    public class CellOutOfGridException : Exception
    {
        public CellOutOfGridException() { }
        public CellOutOfGridException(string message) : base(message) { }
    }
}
