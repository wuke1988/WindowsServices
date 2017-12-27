using Orleans.Runtime.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1OrleansTestDemo_Silo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Silo";

            try
            {
                using (var silo = new SiloHost(Console.Title))
                {
                    silo.ConfigFileName = "OrleansConfiguration.config";

                    silo.LoadOrleansConfig();

                    silo.InitializeOrleansSilo();

                    var startOk = silo.StartOrleansSilo();

                    Console.WriteLine("Silo started successfully");
                    Console.WriteLine("Press any key to exit");
                    Console.ReadLine();

                    silo.ShutdownOrleansSilo();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
