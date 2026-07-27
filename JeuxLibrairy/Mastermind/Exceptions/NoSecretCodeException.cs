using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuxLibrary.Mastermind.Exceptions
{
    public class NoSecretCodeException : Exception
    {
        public NoSecretCodeException() { }

        public NoSecretCodeException(string message) : base(message) { }
    }
}
