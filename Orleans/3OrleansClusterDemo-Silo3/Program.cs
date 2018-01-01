using Orleans.Runtime.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3OrleansClusterDemo_Silo3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "ClusterSilo3";
            try
            {
                using (var silo = new SiloHost(Console.Title))
                {
                    silo.ConfigFileName = "OrleansConfiguration.config";

                    silo.LoadOrleansConfig();

                    silo.InitializeOrleansSilo();

                    silo.StartOrleansSilo();

                    Console.WriteLine("Silo start successfully");
                    Console.WriteLine("Press any key to exit");

                    Console.ReadLine();

                    silo.ShutdownOrleansSilo();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            Console.ReadLine();
        }
    }
}
