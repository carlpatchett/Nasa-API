using NasaAPICore.DataStructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace NasaAPICore
{
    public class CloseApproachData
    {
        public CloseApproachData()
        {

        }

        public DateTime CloseApproachDate { get; set; }

        public double EpochDateClose { get; set; }

        public RelativeVelocity RelativeVelocity { get; set; }

        public MissDistance MissDistance { get; set; }

        public string OrbitingBody { get; set; }
    }
}
