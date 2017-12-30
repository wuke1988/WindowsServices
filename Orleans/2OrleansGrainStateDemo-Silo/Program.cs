using Orleans.Runtime.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2OrleansGrainStateDemo_Silo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "GrainStateSilo";
            try
            {
                using (var siloHost = new SiloHost(Console.Title))
                {
                    siloHost.ConfigFileName = "OrleansConfiguration.config";

                    siloHost.LoadOrleansConfig();

                    siloHost.InitializeOrleansSilo();

                    siloHost.StartOrleansSilo();

                    Console.WriteLine("Silo start successfully");
                    Console.WriteLine("Press any key to exit");

                    Console.ReadLine();
                    siloHost.ShutdownOrleansSilo();
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
