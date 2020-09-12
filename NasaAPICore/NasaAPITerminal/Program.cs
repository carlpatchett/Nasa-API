using NasaAPICore;
using System;

namespace NasaAPITerminal
{
    class Program
    {
        private static APIHub mAPIHub;

        static void Main(string[] args)
        {
            mAPIHub = new APIHub();

            var monitor = new NEOMonitor(mAPIHub);

            if (!mAPIHub.RegistryHub.StoredConnectionStringExists() || string.IsNullOrEmpty(mAPIHub.RegistryHub.ConnectionString))
            {
                mAPIHub.RegistryHub.StoreConnectionString(GetConnectionStringPrompt());
            }

            if (!mAPIHub.RegistryHub.StoredAPIKeyExists() || string.IsNullOrEmpty(mAPIHub.RegistryHub.APIKey))
            {
                mAPIHub.RegistryHub.StoreAPIKey(GetAPIKeyPrompt());
            }

            mAPIHub.SystemMessage += mDataController_SystemMessage;

            monitor.BeginMonitoring();
        }

        private static string GetAPIKeyPrompt()
        {
            if (!string.IsNullOrEmpty(mAPIHub.RegistryHub.APIKey))
            {
                return mAPIHub.RegistryHub.APIKey;
            }

            Console.WriteLine("Please enter a NASA API Key ...\n");

            return Console.ReadLine();
        }

        private static string GetConnectionStringPrompt()
        {
            if (!string.IsNullOrEmpty(mAPIHub.RegistryHub.ConnectionString))
            {
                return mAPIHub.RegistryHub.ConnectionString;
            }

            Console.WriteLine("Please enter an SQL datasource (ie. localhost) ...\n");

            var datasource = Console.ReadLine();

            Console.WriteLine("\nPlease enter an SQL port (ie. 3306) ...\n");

            var port = Console.ReadLine();

            Console.WriteLine("\nPlease enter an SQL username (ie. dave) ...\n");

            var username = Console.ReadLine();

            Console.WriteLine("\nPlease enter an SQL password (ie. test123) ...\n");

            var password = Console.ReadLine();

            Console.WriteLine("\nPlease enter an SQL database name (ie. my_database) ...\n");

            var databaseName = Console.ReadLine();

            return $"datasource={datasource};port={port};username={username};password={password};database={databaseName}"; ;
        }

        private static void mDataController_SystemMessage(object sender, string message)
        {
            Console.WriteLine(message);
        }
    }
}
