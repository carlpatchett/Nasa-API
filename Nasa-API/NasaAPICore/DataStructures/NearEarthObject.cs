using NasaAPICore.DataStructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace NasaAPICore
{
    public class NearEarthObject
    {
        public NearEarthObject()
        {

        }

        public bool IsChecked { get; set; }

        public string Link { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public string NasaJplUrl { get; set; }

        public double AbsoluteMagnitude { get; set; }

        public EstimatedDiameter EstimatedDiameter { get; set; }

        public bool PotentiallyHazardous { get; set; }

        public CloseApproachData CloseApproachData { get; set; }

        public bool IsSentryObject { get; set; }
    }
}
