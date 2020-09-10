using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using NasaAPICore.DataStructures;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NasaAPICore
{
    public class DataController
    {
        private const string NEAR_OBJECTS_REQUEST_URL = "https://api.nasa.gov/neo/rest/v1/feed?";
        private const string NASA_API_KEY = "H8bADZaJKT5icCqe0PwuM8FWJbIEVElirNGzvnBN";
        //private const string NEAR_OBJECTS_REQUEST = "https://api.nasa.gov/neo/rest/v1/feed?start_date=2020-09-08&end_date=2020-09-09&api_key=H8bADZaJKT5icCqe0PwuM8FWJbIEVElirNGzvnBN";

        public IEnumerable<NearEarthObject> RetrieveNEOData(DateTime startDate, DateTime endDate)
        {

            var modifiedRequestUrl = $"{NEAR_OBJECTS_REQUEST_URL}start_date={startDate:yyyy-MM-dd}&end_date={endDate:yyyy-MM-dd}&api_key={NASA_API_KEY}";

            var request = WebRequest.Create(modifiedRequestUrl);

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    var temp = reader.ReadToEnd();

                    return this.ParseNEOString(temp);
                };
            }
            catch
            {
                throw;
            }
        }

        private IEnumerable<NearEarthObject> ParseNEOString(string jsonResponse)
        {
            var obj = ToObject(jsonResponse) as IDictionary<string, object>;
            var links = obj["links"];
            var elementCount = obj["element_count"];
            var nearEarthObjectsByDate = (IDictionary<string, object>)obj["near_earth_objects"];

            var objectList = new List<NearEarthObject>();
            foreach  (var dateEntry in nearEarthObjectsByDate)
            {
                var parsedDate = DateTime.Parse(dateEntry.Key);
                var type = dateEntry.Value.GetType();
                var nearEarthObjects = (List<object>)dateEntry.Value;

                foreach (var nearEarthObjectEntry in nearEarthObjects)
                {
                    var parsedNeo = (IDictionary<string, object>)nearEarthObjectEntry;

                    var estDia = (IDictionary<string, object>)parsedNeo["estimated_diameter"];

                    var kilometers = (IDictionary<string, object>)estDia["kilometers"];
                    var estKmDiaMin = double.Parse(kilometers["estimated_diameter_min"].ToString());
                    var estKmDiaMax = double.Parse(kilometers["estimated_diameter_max"].ToString());

                    var meters = (IDictionary<string, object>)estDia["meters"];
                    var estMetersDiaMin = double.Parse(kilometers["estimated_diameter_min"].ToString());
                    var estMetersDiaMax = double.Parse(kilometers["estimated_diameter_max"].ToString());

                    var miles = (IDictionary<string, object>)estDia["miles"];
                    var estMilesDiaMin = double.Parse(kilometers["estimated_diameter_min"].ToString());
                    var estMilesDiaMax = double.Parse(kilometers["estimated_diameter_max"].ToString());

                    var feet = (IDictionary<string, object>)estDia["feet"];
                    var estFeetDiaMin = double.Parse(kilometers["estimated_diameter_min"].ToString());
                    var estFeetDiaMax = double.Parse(kilometers["estimated_diameter_max"].ToString());

                    var parsedCloseApproachData = (List<object>)parsedNeo["close_approach_data"];
                    var closeApproachDatet = (IDictionary<string, object>)parsedCloseApproachData[0];
                    var closeApproachDate = closeApproachDatet["close_approach_date"];

                    var epochDateCloset = (IDictionary<string, object>)parsedCloseApproachData[0];
                    var epochDateClose = epochDateCloset["epoch_date_close_approach"];

                    var parsedRelativeVelocityt = (IDictionary<string, object>)parsedCloseApproachData[0];
                    var parsedRelativeVelocity = (IDictionary<string, object>)parsedRelativeVelocityt["relative_velocity"];

                    var parsedMissDistancet = (IDictionary<string, object>)parsedCloseApproachData[0];
                    var parsedMissDistance = (IDictionary<string, object>)parsedMissDistancet["miss_distance"];

                    var orbitingBodyt = (IDictionary<string, object>)parsedCloseApproachData[0];
                    var orbitingBody = orbitingBodyt["orbiting_body"];

                    var neo = new NearEarthObject()
                    {
                        Id = parsedNeo["id"].ToString(),
                        Name = parsedNeo["name"].ToString(),
                        NasaJplUrl = parsedNeo["nasa_jpl_url"].ToString(),
                        AbsoluteMagnitude = double.Parse(parsedNeo["absolute_magnitude_h"].ToString()),
                        EstimatedDiameter = new EstimatedDiameter()
                        {
                            KilometersMax = estKmDiaMax,
                            KilometersMin = estKmDiaMin,
                            MetersMax = estMetersDiaMax,
                            MetersMin = estMetersDiaMin,
                            MilesMax = estMilesDiaMax,
                            MilesMin = estMilesDiaMin,
                            FeetMax = estFeetDiaMax,
                            FeetMin = estFeetDiaMin
                        },
                        PotentiallyHazardous = Boolean.Parse(parsedNeo["is_potentially_hazardous_asteroid"].ToString()),
                        CloseApproachData = new CloseApproachData()
                        {
                            CloseApproachDate = DateTime.Parse(closeApproachDate.ToString()),
                            EpochDateClose = double.Parse(epochDateClose.ToString()),
                            RelativeVelocity = new RelativeVelocity()
                            {
                                KilometersPerSecond = double.Parse(parsedRelativeVelocity["kilometers_per_second"].ToString()),
                                KilometersPerHour = double.Parse(parsedRelativeVelocity["kilometers_per_hour"].ToString()),
                                MilesPerHour = double.Parse(parsedRelativeVelocity["miles_per_hour"].ToString())
                            },
                            MissDistance = new MissDistance()
                            {
                                Astronomical = double.Parse(parsedMissDistance["astronomical"].ToString()),
                                Lunar = double.Parse(parsedMissDistance["lunar"].ToString()),
                                Kilometers = double.Parse(parsedMissDistance["kilometers"].ToString()),
                                Miles = double.Parse(parsedMissDistance["miles"].ToString())
                            },
                            OrbitingBody = orbitingBody.ToString()
                        },
                        IsSentryObject = bool.Parse(parsedNeo["is_sentry_object"].ToString())
                    };

                    objectList.Add(neo);
                }
            }

            return objectList;
        }

        private static object ToObject(string json)
        {
            if (string.IsNullOrEmpty(json))
                return null;

            return ToObject(JToken.Parse(json));
        }

        private static object ToObject(JToken token)
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                    return token.Children<JProperty>()
                                .ToDictionary(prop => prop.Name,
                                              prop => ToObject(prop.Value),
                                              StringComparer.OrdinalIgnoreCase);

                case JTokenType.Array:
                    return token.Select(ToObject).ToList();

                default:
                    return ((JValue)token).Value;
            }
        }
    }
}
