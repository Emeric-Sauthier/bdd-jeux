using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuxLibrary.Mastermind.Exceptions
{
    public class WrongColorException : Exception
    {
        public WrongColorException() { }
        public WrongColorException(string message) : base(message) { }
    }
}
