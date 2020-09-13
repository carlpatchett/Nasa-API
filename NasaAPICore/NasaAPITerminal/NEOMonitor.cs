using NasaAPICore;
using System;
using System.Threading.Tasks;

namespace NasaAPINEOMonitor
{
    /// <summary>
    /// Contains functionaltiy related to monitoring timed retrieval of NEO data.
    /// </summary>
    class NEOMonitor
    {
        private APIHub mAPIHub;

        /// <summary>
        /// Creates a new instance of <see cref="NEOMonitor"/>.
        /// </summary>
        /// <param name="apiHub">The <see cref="APIHub"/> responsible for API related functionality.</param>
        public NEOMonitor(APIHub apiHub)
        {
            mAPIHub = apiHub;
        }

        /// <summary>
        /// Begins timed monitoring of NEO data retrieval.
        /// </summary>
        public void BeginMonitoring()
        {
            while (true)
            {
                Task.Run(() =>
                {
                    Console.WriteLine("#################");
                    Console.WriteLine("Checking If NEO Retrieval Is Required...");
                    if (this.IsRetrievalRequired())
                    {
                        Console.WriteLine("NEO Retrieval Is Required...");
                        var endDate = DateTime.UtcNow;
                        var startdate = endDate.AddDays(-7.0);

                        var response = mAPIHub.APIRequestHub.PerformAPIRequestNEO(startdate, endDate);
                        var neos = mAPIHub.APIParserHub.ParseNEOsFromJson(response.Result);

                        mAPIHub.SQLHub.SQLQueryStoreNEOs(mAPIHub.RegistryHub.ConnectionString, neos);

                        mAPIHub.RegistryHub.StoreLastRetrievalDate();

                        Console.WriteLine("NEO Retrieval Successful...");
                    }
                    else
                    {
                        Console.WriteLine("NEO Retrieval Not Required, Waiting...");
                        Console.WriteLine("#################");
                    }
                });

                System.Threading.Thread.Sleep(1.8e+6);
            }
        }

        private bool IsRetrievalRequired()
        {
            if (!mAPIHub.RegistryHub.LastRetrievedDate.HasValue)
            {
                return true;
            }

            var hoursSinceLastRetrieval = new TimeSpan(DateTime.UtcNow.Ticks - mAPIHub.RegistryHub.LastRetrievedDate.Value.Ticks).Hours;

            if (hoursSinceLastRetrieval > 2)
            {
                return true;
            }

            return false;
        }
    }
}
