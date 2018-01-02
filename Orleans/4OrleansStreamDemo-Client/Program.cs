using _4OrleansStreamDemo;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4OrleansStreamDemo_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            InitialWithRetries();

            while (true)
            {
                Console.WriteLine("Press 'exit' to exit...");
                var input = Console.ReadLine();
                if (input == "exit")
                    break;
                var publishGrain = GrainClient.GrainFactory.GetGrain<IPublisherGrain>(Guid.Empty);
                publishGrain.PublishMessageAsync(input);
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
