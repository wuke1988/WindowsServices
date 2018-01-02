using _4OrleansStreamDemo;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4OrleansStreamDemo_ClientB
{
    class Program
    {
        static void Main(string[] args)
        {
            InitialWithRetries();
            while (true)
            {               
                var input = Console.ReadLine();
                if (input == "exit")
                    break;
                var subscriberGrain = GrainClient.GrainFactory.GetGrain<IExplicitSubscriberGrain>(Guid.Empty);

                var streamHandle = subscriberGrain.SubscribeAsync().Result;

                Console.WriteLine("Press enter to exit...");
                Console.ReadLine();

                streamHandle.UnsubscribeAsync();
            }
        }
        static void InitialWithRetries()
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
