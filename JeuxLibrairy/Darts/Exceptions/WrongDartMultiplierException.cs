using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuxLibrary.Darts.Exceptions
{
    public class WrongDartMultiplierException : Exception
    {
        public WrongDartMultiplierException() { }
        public WrongDartMultiplierException(string message) : base(message) { }
    }
}
