using System;
using System.Collections.Generic;
using System.Text;

namespace NasaAPICLI.Commands
{
    public class UpdateCommand : IConsoleCommand
    {
        private NasaAPICore.APIHub mAPIHub;

        public UpdateCommand(NasaAPICore.APIHub apiHub)
        {
            mAPIHub = apiHub;
        }

        public string Name => ConsoleCommands.UPDATE_COMMAND;

        public bool Execute(string[] args = null)
        {
            switch (args[0])
            {
                case ConsoleCommands.CONNECTION_STRING_PHRASE:

                    var connectionString = this. GetConnectionStringPrompt();

                    if (mAPIHub.RegistryHub.ValidateConnectionString(connectionString))
                    {
                        mAPIHub.RegistryHub.CreateAPIRegistryKeys();
                        mAPIHub.RegistryHub.StoreConnectionString(connectionString);
                        break;
                    }

                    Console.WriteLine("\nConnection string was invalid. Press any key to continue...");
                    Console.ReadLine();

                    return true;

                case ConsoleCommands.API_KEY_PHRASE:

                    var apiKey = this.GetAPIKeyPrompt();

                    if (mAPIHub.RegistryHub.ValidateAPIKey(apiKey))
                    {
                        mAPIHub.RegistryHub.CreateAPIRegistryKeys();
                        mAPIHub.RegistryHub.StoreAPIKey(apiKey);
                        break;
                    }

                    Console.WriteLine("\nAPI Key was invalid. Press any key to continue...");
                    Console.ReadLine();

                    return true;

                default:

                    return false;
            }

            return false;
        }

        private string GetAPIKeyPrompt()
        {
            Console.Clear();

            Console.WriteLine("Please enter a NASA API Key ...\n");

            return Console.ReadLine();
        }

        private string GetConnectionStringPrompt()
        {
            Console.Clear();

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
    }
}
