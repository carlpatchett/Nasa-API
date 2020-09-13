using System;
using System.Collections.Generic;
using System.Text;

namespace NasaAPICLI.Commands
{
    public interface IConsoleCommand
    {
        string Name { get; }

        bool Execute(string[] args = null);
    }
}
