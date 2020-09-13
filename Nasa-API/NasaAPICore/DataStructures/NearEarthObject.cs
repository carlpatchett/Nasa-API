using NasaAPICore.DataStructures;

namespace NasaAPICore
{
    /// <summary>
    /// Contains information about a near earth object.
    /// </summary>
    public class NearEarthObject
    {
        /// <summary>
        /// Creates a new instance of <see cref="NearEarthObject"/>.
        /// </summary>
        public NearEarthObject()
        {

        }

        public bool IsChecked { get; set; }

        /// <summary>
        /// Gets/Sets the near earth object link.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Gets/Sets the near earth objects Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets/Sets the near earth object name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets/Sets the near earth object NASA JPL URL.
        /// </summary>
        public string NasaJplUrl { get; set; }

        /// <summary>
        /// Gets/Sets the near earth object absolute magnitude.
        /// </summary>
        public double AbsoluteMagnitude { get; set; }

        /// <summary>
        /// Gets/Sets the near earth object estimated diameter.
        /// </summary>
        public EstimatedDiameter EstimatedDiameter { get; set; }

        /// <summary>
        /// Gets/Sets whether the near earth object is potentially hazardous.
        /// </summary>
        public bool PotentiallyHazardous { get; set; }

        /// <summary>
        /// Gets/Sets the near earth object close approach data.
        /// </summary>
        public CloseApproachData CloseApproachData { get; set; }

        /// <summary>
        /// Gets/Sets whether the near earth object is a sentry.
        /// </summary>
        public bool IsSentryObject { get; set; }
    }
}
