using Orleans.Runtime.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4OrleansStreamDemo_Silo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Client";

            try
            {
                using (var silo = new SiloHost(Console.Title))
                {
                    silo.ConfigFileName = "OrleansConfiguration.config";

                    silo.InitializeOrleansSilo();

                    var startedOk = silo.StartOrleansSilo();                    

                    Console.WriteLine("Silo started successfully");

                    Console.WriteLine("Press any key to exit...");

                    Console.ReadLine();

                    silo.ShutdownOrleansSilo();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
    }
}
