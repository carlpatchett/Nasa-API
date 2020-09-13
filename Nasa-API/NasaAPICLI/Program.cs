using NasaAPICLI.Commands;
using NasaAPICore;
using System;
using System.Linq;

namespace NasaAPICLI
{
    class Program
    {
        private static APIHub mAPIHub = new APIHub();

        private static UpdateCommand mUpdateCommand;
        private static SQLCommand mSQLCommand;
        private static QuitCommand mQuitCommand;

        static void Main(string[] args)
        {
            mUpdateCommand = new UpdateCommand(mAPIHub);
            mSQLCommand = new SQLCommand(mAPIHub);
            mQuitCommand = new QuitCommand();

            RunCore();
        }

        private static void RunCore()
        {
            while (true)
            {
                Console.Clear();

                PrintStart();

                PrintOptions();

                var userInput = Console.ReadLine();

                var baseCommand = userInput.Split(' ')[0];
                var args = userInput.Split(' ').Skip(1).ToArray();

                switch (baseCommand)
                {
                    case ConsoleCommands.QUIT_COMMAND:

                        mQuitCommand.Execute();
                        break;

                    case ConsoleCommands.UPDATE_COMMAND:

                        if (!mUpdateCommand.Execute(args))
                        {
                            Console.WriteLine("\nCommand was not registered. Press any key to continue...");
                            Console.ReadLine();
                        };

                        break;

                    case ConsoleCommands.SQL_COMMAND:

                        if (!mSQLCommand.Execute(args))
                        {
                            Console.WriteLine("\nCommand was not registered. Press any key to continue...");
                            Console.ReadLine();
                        };

                        break;

                    default:

                        Console.WriteLine("\nCommand was not registered. Press any key to continue...");
                        Console.ReadLine();
                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
            }
        }

        private static void PrintStart()
        {
            Console.WriteLine("##########################");
            Console.WriteLine("\n");
            Console.WriteLine("\nWelcome to the NASA API CLI Tool.");
            Console.WriteLine("\nYou can use this tool to interact directly with the API.");
            Console.WriteLine("\n");
            Console.WriteLine("\n##########################");
            Console.WriteLine("\n");
        }

        private static void PrintOptions()
        {
            Console.WriteLine("\n'update <phrase>' - runs the prompt to update the stored <phrase>.");
            Console.WriteLine("\n'update api key' - runs the prompt to update the stored API Key.");
            Console.WriteLine("\n'update connection string' - runs the prompt to update the stored connection string.");
            Console.WriteLine("\n'sql retrieve <phrase>' - retrieves all <phrase> from the SQL database and prints them.");
            Console.WriteLine("\n'sql retrieve neos' - retrieves all near earth objects from the SQL database and prints them.");
            Console.WriteLine("\n'sql store <phrase>' - stores all <phrase> to the SQL database.");
            Console.WriteLine("\n'sql store neos' - stores all near earth objects to the SQL database.");
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
