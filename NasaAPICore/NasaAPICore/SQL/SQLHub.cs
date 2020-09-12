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
            var baseQuery = "INSERT IGNORE INTO near_earth_objects(name,data) VALUES";

            foreach (var neo in neos)
            {
                baseQuery = $"{baseQuery} ('{neo.Name}','{JsonConvert.SerializeObject(neo)}'),";
            }

            var modifiedQueryString = baseQuery.Substring(0, baseQuery.Length - 1);

            this.SQLQuery(connectionString, modifiedQueryString);
        }

        #endregion
    }
}
