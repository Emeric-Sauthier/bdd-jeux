using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuxLibrary.Darts.Exceptions
{
    public class WrongDartMuliplierException : Exception
    {
        public WrongDartMuliplierException() { }
        public WrongDartMuliplierException(string message) : base(message) { }
    }
}
