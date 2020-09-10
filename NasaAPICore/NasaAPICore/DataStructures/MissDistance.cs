using System;
using System.Collections.Generic;
using System.Text;

namespace NasaAPICore.DataStructures
{
    public class MissDistance
    {
        public MissDistance()
        {

        }

        public double Astronomical { get; set; }

        public double Lunar { get; set; }

        public double Kilometers { get; set; }

        public double Miles { get; set; }
    }
}
