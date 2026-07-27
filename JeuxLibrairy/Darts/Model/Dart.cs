using JeuxLibrary.Darts.Enums;
using JeuxLibrary.Darts.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuxLibrary.Darts.Model
{
    public class Dart
    {
        public const int Bull = 25;

        public int Sector { get; }
        public Multiplier Multiplier { get; }
        public int Points => Sector * (int)Multiplier;

        public Dart(int sector, Multiplier multiplier)
        {
            if ((sector < 0 || sector > 20) && sector != Bull)
            {
                throw new WrongDartSectorException($"Invalid value '{sector}'.");
            }
            else if (sector == Bull && multiplier == Multiplier.Triple)
            {
                throw new WrongDartMultiplierException($"Invalid multiplier.");
            }

            Sector = sector;
            Multiplier = multiplier;
        }
    }
}
