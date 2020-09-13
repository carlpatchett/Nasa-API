using NasaAPICore.DataStructures;
using System;

namespace NasaAPICore
{
    /// <summary>
    /// Contains information about an objects approach data.
    /// </summary>
    public class CloseApproachData
    {
        public CloseApproachData()
        {

        }

        /// <summary>
        /// The <see cref="DateTime"/> of the close approach.
        /// </summary>
        public DateTime CloseApproachDate { get; set; }

        /// <summary>
        /// The time since epoch of the close approach.
        /// </summary>
        public double EpochDateClose { get; set; }

        /// <summary>
        /// The <see cref="DataStructures.RelativeVelocity"/> of the close approach.
        /// </summary>
        public RelativeVelocity RelativeVelocity { get; set; }

        /// <summary>
        /// The <see cref="DataStructures.MissDistance"/> of the close approach.
        /// </summary>
        public MissDistance MissDistance { get; set; }

        /// <summary>
        /// The orbiting body of the close approach.
        /// </summary>
        public string OrbitingBody { get; set; }
    }
}
