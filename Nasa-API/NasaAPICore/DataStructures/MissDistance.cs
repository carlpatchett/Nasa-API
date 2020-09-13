namespace NasaAPICore.DataStructures
{
    /// <summary>
    /// Contains information about an objects miss distance when on a close approach.
    /// </summary>
    public class MissDistance
    {
        /// <summary>
        /// Creates a new instance of <see cref="MissDistance"/>.
        /// </summary>
        public MissDistance()
        {

        }

        /// <summary>
        /// The miss distance in Astronominal Units (AU).
        /// </summary>
        public double Astronomical { get; set; }

        /// <summary>
        /// The miss distance in Lunar Duistance (LD). 
        /// </summary>
        public double Lunar { get; set; }

        /// <summary>
        /// The miss distance in kilometers.
        /// </summary>
        public double Kilometers { get; set; }

        /// <summary>
        /// The miss distance in miles.
        /// </summary>
        public double Miles { get; set; }
    }
}
