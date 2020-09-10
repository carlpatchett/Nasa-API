using System;
using System.Collections.Generic;
using System.Text;

namespace NasaAPICore.DataStructures
{
    public class RelativeVelocity
    {
        public RelativeVelocity()
        {

        }

        public double KilometersPerSecond { get; set; }

        public double KilometersPerHour { get; set; }

        public double MilesPerHour { get; set; }
    }
}
