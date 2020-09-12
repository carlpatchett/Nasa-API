namespace NasaAPICore.DataStructures
{
    /// <summary>
    /// Contains information about an objects relative velocity.
    /// </summary>
    public class RelativeVelocity
    {
        public RelativeVelocity()
        {

        }

        /// <summary>
        /// The relative velocity in kilometers per second.
        /// </summary>
        public double KilometersPerSecond { get; set; }

        /// <summary>
        /// The relative velocity in kilometers per hour.
        /// </summary>
        public double KilometersPerHour { get; set; }

        /// <summary>
        /// The relative velocity in miles per hour.
        /// </summary>
        public double MilesPerHour { get; set; }
    }
}
