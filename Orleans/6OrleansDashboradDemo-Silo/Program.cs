using Orleans.Runtime.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6OrleansDashboradDemo_Silo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Title = "DashboardSilo1";
                using (var silo = new SiloHost("DashboardSilo1"))
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
