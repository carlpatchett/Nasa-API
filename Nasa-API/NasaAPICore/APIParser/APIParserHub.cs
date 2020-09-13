using NasaAPICore.DataStructures;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace NasaAPICore.APIParser
{
    /// <summary>
    /// Contains all functionality required to parse API Requests from an <see cref="APIRequests.APIRequestHub"/>.
    /// </summary>
    public class APIParserHub
    {

        public APIParserHub()
        {

        }

        #region Object Parsing

        /// <summary>
        /// Parses a provided json response into an <see cref="IEnumerable{NearEarthObject}"/>.
        /// </summary>
        /// <param name="jsonResponse">The json response to parse.</param>
        /// <returns>An <see cref="IEnumerable{NearEarthObject}"/> containing all near earth objects parsed.</returns>
        public IEnumerable<NearEarthObject> ParseNEOsFromJson(string jsonResponse)
        {
            var obj = ToObject(jsonResponse) as IDictionary<string, object>;
            var links = obj["links"];
            var elementCount = obj["element_count"];
            var nearEarthObjectsByDate = (IDictionary<string, object>)obj["near_earth_objects"];

            var objectList = new List<NearEarthObject>();
            foreach (var dateEntry in nearEarthObjectsByDate)
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

        /// <summary>
        /// Parses a provided <see cref="DataTable"/> into an <see cref="IEnumerable{NearEarthObject}"/>.
        /// </summary>
        /// <param name="table">The <see cref="DataTable"/> containing information about near earth objects.</param>
        /// <returns>An <see cref="IEnumerable{NearEarthObject}"/> containing all near earth objects parsed.</returns>
        public IEnumerable<NearEarthObject> ParseNEOsFromDataTable(DataTable table)
        {
            var neos = new List<NearEarthObject>();
            foreach (DataRow row in table.Rows)
            {
                var neo = new NearEarthObject()
                {
                    Name = row[0].ToString(),
                    NasaJplUrl = row[1].ToString(),
                    AbsoluteMagnitude = double.Parse(row[2].ToString()),
                    EstimatedDiameter = new EstimatedDiameter()
                    {
                        KilometersMax = double.Parse(row[3].ToString()),
                        KilometersMin = double.Parse(row[4].ToString()),
                        MetersMax = double.Parse(row[5].ToString()),
                        MetersMin = double.Parse(row[6].ToString()),
                        MilesMax = double.Parse(row[7].ToString()),
                        MilesMin = double.Parse(row[8].ToString()),
                        FeetMax = double.Parse(row[9].ToString()),
                        FeetMin = double.Parse(row[10].ToString())
                    },
                    PotentiallyHazardous = bool.Parse(row[11].ToString()),
                    CloseApproachData = new CloseApproachData()
                    {
                        CloseApproachDate = DateTime.Parse(row[12].ToString()),
                        EpochDateClose = double.Parse(row[13].ToString()),
                        RelativeVelocity = new RelativeVelocity()
                        {
                            KilometersPerSecond = double.Parse(row[14].ToString()),
                            KilometersPerHour = double.Parse(row[15].ToString()),
                            MilesPerHour = double.Parse(row[16].ToString())
                        },
                        MissDistance = new MissDistance()
                        {
                            Astronomical = double.Parse(row[17].ToString()),
                            Lunar = double.Parse(row[18].ToString()),
                            Kilometers = double.Parse(row[19].ToString()),
                            Miles = double.Parse(row[20].ToString())
                        },
                        OrbitingBody = row[21].ToString()
                    },
                    IsSentryObject = bool.Parse(row[22].ToString())
                };

                neos.Add(neo);
            }

            return neos;
        }

        #endregion

        #region Helper Methods

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

        #endregion
    }
}
