using System;
using System.Collections.Generic;
using System.Text;

namespace NasaAPICore.DataStructures
{
    public class EstimatedDiameter
    {
        public EstimatedDiameter()
        {

        }

        public double KilometersMax { get; set; }

        public double KilometersMin { get; set; }

        public double MetersMax { get; set; }

        public double MetersMin { get; set; }

        public double MilesMax { get; set; }

        public double MilesMin { get; set; }

        public double FeetMax { get; set; }

        public double FeetMin { get; set; }

    }
}
