using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NasaAPICore.SQL
{
    /// <summary>
    /// Contains all functionaltiy required for making SQL Queries.
    /// </summary>
    public class SQLHub
    {

        public event EventHandler<string> SystemMessage;

        public SQLHub()
        {

        }

        #region Base SQL Query

        /// <summary>
        /// Performs an SQL query using the provided connection string.
        /// </summary>
        /// <param name="connectionString">The connection string for the SQL query.</param>
        /// <param name="query">The query to execute.</param>
        public void SQLQuery(string connectionString, string query)
        {
            try
            {
                this.SystemMessage?.Invoke(this, "####################");
                this.SystemMessage?.Invoke(this, "Beginning SQL Query...");

                var connection = new MySqlConnection(connectionString);
                var command = new MySqlCommand(query, connection);

                MySqlDataReader reader;

                this.SystemMessage?.Invoke(this, "Attempting SQL Connection...");
                connection.Open();
                this.SystemMessage?.Invoke(this, "SQL Connection Successful...");

                this.SystemMessage?.Invoke(this, "Executing SQL Query...");
                reader = command.ExecuteReader();
                this.SystemMessage?.Invoke(this, "SQL Query Executed Successfully...");

                while (reader.Read())
                {
                }

                this.SystemMessage?.Invoke(this, "Closing SQL Connection...");
                connection.Close();
                this.SystemMessage?.Invoke(this, "SQL Connection Closed Successfully...");
                this.SystemMessage?.Invoke(this, "####################");
            }
            catch (Exception ex)
            {
                this.SystemMessage?.Invoke(this, $"Exception encountered whilst executing SQL Query: \n {ex.Message}");
            }
        }

        #endregion

        #region Specific SQL Queries

        /// <summary>
        /// Stores all provided near earth objects in an SQL table.
        /// </summary>
        /// <param name="neos">The <see cref="IEnumerable{NearEarthObject}"/> containing near earth objects to store.</param>
        public void SQLQueryStoreNEOs(string connectionString, IEnumerable<NearEarthObject> neos)
        {
            var baseQuery = "INSERT IGNORE INTO near_earth_objects(name," +
                "nasa_jpl_url," +
                "absolute_magnitude," +
                "est_dia_kilometers_max," +
                "est_dia_kilometers_min," +
                "est_dia_meters_max," +
                "est_dia_meters_min," +
                "est_dia_miles_max," +
                "est_dia_miles_min," +
                "potentially_hazardous," +
                "cls_app_date," +
                "cls_app_epoch," +
                "cls_app_kilometers_per_second," +
                "cls_app_kilometers_per_hour," +
                "cls_app_miles_per_hour," +
                "miss_dst_astronomical," +
                "miss_dst_lunar," +
                "miss_dst_kilometers," +
                "miss_dst_miles," +
                "cls_app_orbiting_body," +
                "is_sentry_object) VALUES";

            foreach (var neo in neos)
            {
                baseQuery = $"{baseQuery} ('{neo.Name}'," +
                    $"'{neo.NasaJplUrl}'," +
                    $"'{neo.AbsoluteMagnitude}'," +
                    $"'{neo.EstimatedDiameter.KilometersMax}'," +
                    $"'{neo.EstimatedDiameter.KilometersMin}'," +
                    $"'{neo.EstimatedDiameter.MetersMax}'," +
                    $"'{neo.EstimatedDiameter.MetersMin}'," +
                    $"'{neo.EstimatedDiameter.MilesMax}'," +
                    $"'{neo.EstimatedDiameter.MilesMin}'," +
                    $"'{neo.PotentiallyHazardous}'," +
                    $"'{neo.CloseApproachData.CloseApproachDate}'," +
                    $"'{neo.CloseApproachData.EpochDateClose}'," +
                    $"'{neo.CloseApproachData.RelativeVelocity.KilometersPerSecond}'," +
                    $"'{neo.CloseApproachData.RelativeVelocity.KilometersPerHour}'," +
                    $"'{neo.CloseApproachData.RelativeVelocity.MilesPerHour}'," +
                    $"'{neo.CloseApproachData.MissDistance.Astronomical}'," +
                    $"'{neo.CloseApproachData.MissDistance.Lunar}'," +
                    $"'{neo.CloseApproachData.MissDistance.Kilometers}'," +
                    $"'{neo.CloseApproachData.MissDistance.Miles}'," +
                    $"'{neo.CloseApproachData.OrbitingBody}'," +
                    $"'{neo.IsSentryObject}'),";
            }

            var modifiedQueryString = baseQuery.Substring(0, baseQuery.Length - 1);

            this.SQLQuery(connectionString, modifiedQueryString);
        }

        #endregion
    }
}
