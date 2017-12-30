using _2OrleansGrainStateDemo_Interface;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2OrleansGrainStateDemo_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            InitialWithRetries();

            Test();
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
        private static void Test()
        {
            var petter = GrainClient.GrainFactory.GetGrain<IPersonGrain>("Petter");
            petter.SayHelloAsync();
            petter.SayHelloAsync();

            var tom = GrainClient.GrainFactory.GetGrain<IPersonGrain>("Tom");
            tom.SayHelloAsync();
            tom.SayHelloAsync();

            Console.ReadLine();
        }
    }
}
