using System;
using System.Collections.Generic;
using System.Text;

namespace NasaAPICLI.Commands
{
    public class QuitCommand : IConsoleCommand
    {
        public string Name => ConsoleCommands.QUIT_COMMAND;

        public bool Execute(string[] args = null)
        {
            Console.WriteLine("\nPress any key to quit...");
            Console.ReadLine();

            Environment.Exit(0);

            return true;
        }
    }
}
