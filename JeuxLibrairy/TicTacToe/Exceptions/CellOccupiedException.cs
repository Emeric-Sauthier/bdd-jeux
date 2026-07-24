using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuxLibrairy.TicTacToe.Exceptions
{
    public class CellOccupiedException : Exception
    {
        public CellOccupiedException() { }
        public CellOccupiedException(string message) : base(message) { }
    }
}
