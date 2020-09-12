using System;
using System.Collections.Generic;
using System.Text;

namespace NasaAPICore.DataStructures
{
    /// <summary>
    /// Contains information about an objects estimated diameter.
    /// </summary>
    public class EstimatedDiameter
    {
        public EstimatedDiameter()
        {

        }

        /// <summary>
        /// The maximum estimated diameter in kilometers.
        /// </summary>
        public double KilometersMax { get; set; }

        /// <summary>
        /// The minimum estimated diameter in kilometers.
        /// </summary>
        public double KilometersMin { get; set; }

        /// <summary>
        /// The maximum estimated diameter in meters.
        /// </summary>
        public double MetersMax { get; set; }

        /// <summary>
        /// The minimum estimated diameter in meters.
        /// </summary>
        public double MetersMin { get; set; }

        /// <summary>
        /// The maximum estimated diameter in miles.
        /// </summary>
        public double MilesMax { get; set; }

        /// <summary>
        /// The minimum estimated diameter in miles.
        /// </summary>
        public double MilesMin { get; set; }

        /// <summary>
        /// The maximum estimated diameter in feet.
        /// </summary>
        public double FeetMax { get; set; }

        /// <summary>
        /// The minimum estimated diameter in feet.
        /// </summary>
        public double FeetMin { get; set; }

    }
}
