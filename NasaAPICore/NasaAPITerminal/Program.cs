using NasaAPICore;
using System;

namespace NasaAPITerminal
{
    class Program
    {
        static void Main(string[] args)
        {
            var controller = new DataController();

            var result = controller.RetrieveNEOData(new DateTime(2020, 09, 08), new DateTime(2020, 09, 09));

            var count = 0;
            foreach(var item in result)
            {
                count++;
            }

            Console.WriteLine($"Retrieved {count} objects!");
        }
    }
}
