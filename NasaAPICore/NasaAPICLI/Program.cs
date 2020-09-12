using NasaAPICore;
using System;

namespace NasaAPICLI
{
    class Program
    {
        private static APIHub mAPIHub = new APIHub();

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        private static void RunCore()
        {

            while (true)
            {
                Console.Clear();

                Console.WriteLine("\nPlease select from one of the following options:");

                var userInput = Console.ReadLine();
                var splitArguments = userInput.Split(' ');

                if (splitArguments.Length == 1)
                {
                    switch (splitArguments[0].ToLowerInvariant())
                    {
                        case ConsoleCommands.QUIT_COMMAND:

                            Console.WriteLine("\nPress any key to quit...");
                            Console.ReadLine();
                            Environment.Exit(0);
                            break;

                        default:

                            Console.WriteLine("\nCommand that was entered was unknown. Press any key to continue...");
                            Console.ReadLine();
                            break;
                    }
                }
                else if (splitArguments.Length == 2)
                {
                    switch (splitArguments[0].ToLowerInvariant())
                    {
                        case ConsoleCommands.UPDATE_COMMAND:

                            switch (splitArguments[1].ToLowerInvariant())
                            {
                                case ConsoleCommands.CONNECTION_STRING_PHRASE:

                                    var connectionString = GetConnectionStringPrompt();

                                    if (mAPIHub.RegistryHub.ValidateConnectionString(connectionString))
                                    {
                                        mAPIHub.RegistryHub.CreateAPIRegistryKeys();
                                        mAPIHub.RegistryHub.StoreConnectionString(connectionString);
                                        break;
                                    }

                                    Console.WriteLine("\nConnection string was invalid. Press any key to continue...");
                                    Console.ReadLine();

                                    break;
                                case ConsoleCommands.API_KEY_PHRASE:

                                    var apiKey = GetAPIKeyPrompt();

                                    if (mAPIHub.RegistryHub.ValidateAPIKey(apiKey))
                                    {
                                        mAPIHub.RegistryHub.CreateAPIRegistryKeys();
                                        mAPIHub.RegistryHub.StoreAPIKey(apiKey);
                                        break;
                                    }

                                    Console.WriteLine("\nAPI Key was invalid. Press any key to continue...");
                                    Console.ReadLine();

                                    break;
                            }
                            break;

                        default:

                            Console.WriteLine("\nCommand that was entered was unknown. Press any key to continue...");
                            Console.ReadLine();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\nCommand that was entered was unknown. Press any key to continue...");
                    Console.ReadLine();
                }
            }
        }

        private static void PrintOptions()
        {
            Console.WriteLine("\n'update <setting>' - runs the CLI tool to update the given setting.");
            Console.WriteLine("\n'quit' - quits the CLI tool.");
            Console.WriteLine("\n");
        }

        private static string GetAPIKeyPrompt()
        {
            Console.Clear();

            Console.WriteLine("Please enter a NASA API Key ...\n");

            return Console.ReadLine();
        }

        private static string GetConnectionStringPrompt()
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
