using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace NasaAPICore.APIRequests
{
    /// <summary>
    /// Contains all functionality required to make NASA API requests.
    /// </summary>
    public class APIRequestHub
    {
        private const string NEAR_OBJECTS_REQUEST_URL = "https://api.nasa.gov/neo/rest/v1/feed?";
        private const string NASA_API_KEY = "H8bADZaJKT5icCqe0PwuM8FWJbIEVElirNGzvnBN";

        public APIRequestHub()
        {

        }

        #region Base API Request

        /// <summary>
        /// Performs an API retrieval request.
        /// </summary>
        /// <param name="requestURL">The url of the API endpoint to retrieve data from.</param>
        public async Task<string> PerformAPIRequest(string requestURL)
        {
            var request = WebRequest.Create(requestURL);

            return await Task.Run(() =>
            {
                try
                {
                    using (var response = (HttpWebResponse)request.GetResponse())
                    using (var stream = response.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
                        var temp = reader.ReadToEnd();

                        return temp;
                    };
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        #endregion

        #region Specific API Requests

        /// <summary>
        /// Performs a Near Earth Object API retrieval request.
        /// </summary>
        /// <param name="startDate">The start <see cref="DateTime"/> from which to retrieve all near earth objects from.</param>
        /// <param name="endDate">The end <see cref="DateTime"/> from which to retrieve all near earth objects from.</param>
        public async Task<string> PerformAPIRequestNEO(DateTime startDate, DateTime endDate)
        {
            var modifiedRequestUrl = $"{NEAR_OBJECTS_REQUEST_URL}start_date={startDate:yyyy-MM-dd}&end_date={endDate:yyyy-MM-dd}&api_key={NASA_API_KEY}";

            return await this.PerformAPIRequest(modifiedRequestUrl);
        }

        #endregion
    }
}
