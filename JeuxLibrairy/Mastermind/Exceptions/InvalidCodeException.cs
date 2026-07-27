using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuxLibrary.Mastermind.Exceptions
{
    public class InvalidCodeException : Exception
    {
        public InvalidCodeException() { }
        public InvalidCodeException (string message) : base(message) { }
    }
}
