using NasaAPICore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NasaAPICLI.Commands
{
    public class SQLCommand : IConsoleCommand
    {
        private APIHub mAPIHub;

        public SQLCommand(APIHub apiHub)
        {
            mAPIHub = apiHub;
        }

        public string Name => ConsoleCommands.SQL_COMMAND;

        public bool Execute(string[] args = null)
        {
            switch (args[0])
            {
                case ConsoleCommands.STORE_SQL_COMMAND:

                    this.StoreSQLCommand(args[1]);

                    return true;

                case ConsoleCommands.RETRIEVE_SQL_COMMAND:

                    var table = this.RetrieveSQLCommand(args[1]);

                    foreach (DataRow dataRow in table.Rows)
                    {
                        foreach (var item in dataRow.ItemArray)
                        {
                            Console.WriteLine($"{item}");
                        }

                        Console.WriteLine("\n");
                    }

                    return true;

                default:

                    return false;
            }
        }

        public void StoreSQLCommand(string target)
        {
            switch (target)
            {
                case ConsoleCommands.NEOS_PHRASE:

                    Console.Clear();

                    while (true)
                    {
                        Console.WriteLine("Please enter the number of days to retrieve and store near earth objects for..");
                        if (double.TryParse(Console.ReadLine(), out var parsedDouble))
                        {
                            if (parsedDouble > 0 && parsedDouble <= 7)
                            {
                                var endDate = DateTime.UtcNow;
                                var startDate = endDate.AddDays(-parsedDouble);

                                var neos = mAPIHub.APIParserHub.ParseNEOsFromJson(mAPIHub.APIRequestHub.PerformAPIRequestNEO(startDate, endDate).Result);
                                mAPIHub.SQLHub.SQLQueryStoreNEOs(mAPIHub.RegistryHub.ConnectionString, neos);

                                break;
                            }
                        };

                        Console.WriteLine("\nNumber entered must be a positive number, and 7 days or less. (This is due to a Nasa API restriction).\n\n");
                    }

                    break;

                default:

                    break;
            }
        }

        public DataTable RetrieveSQLCommand(string target)
        {
            switch (target)
            {
                case ConsoleCommands.NEOS_PHRASE:

                    return mAPIHub.SQLHub.SQLQueryRetrieveNEOs(mAPIHub.RegistryHub.ConnectionString);

                default:

                    return null;
            }

        }
    }
}