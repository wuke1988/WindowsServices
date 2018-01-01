using _2OrleansGrainStateDemo_Interface;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _3OrleansClusterDemo_Client
{
    class Program
    {
        
        static void Main(string[] args)
        {
            InitialWithRetries();

            Random random = new Random();

            while (true)
            {
                Thread.Sleep(1000);

                var grainId = random.Next().ToString();

                var grain = GrainClient.GrainFactory.GetGrain<IPersonGrain>("Test-"+grainId);

                grain.SayHelloAsync();
            }            
        }
        private static void InitialWithRetries()
        {
            try
            {
                GrainClient.Initialize("ClientConfiguration.config");
                Console.WriteLine("Client success connect to silo");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed connect to silo {ex.Message}");
            }
        }
    }
}
