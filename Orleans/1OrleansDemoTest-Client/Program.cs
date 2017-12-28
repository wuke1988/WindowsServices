using _1OlreansTestDemo_Interfaces;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1OrleansDemoTest_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeWithRetries();
            Console.WriteLine("Client");
            DoWork();
            Console.ReadLine();
        }
        private static void DoWork()
        {
            Task task1 = Task.Factory.StartNew(() =>
            {
                AddCount("T1");
            });
            Task task2 = Task.Factory.StartNew(() =>
            {
                AddCount("T2");
            });
            Task task3 = Task.Factory.StartNew(() =>
            {
                AddCount("T3");
            });

            Task.WaitAll(task1, task2, task3);
        }
        private static void InitializeWithRetries()
        {
            try
            {
                GrainClient.Initialize("ClientConfiguration.config");
                Console.WriteLine("Client success connect to silo host");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed connect to silo host {ex.Message}");
                Console.ReadLine();
            }
        }

        private static void AddCount(string taskName)
        {
            var test = Orleans.GrainClient.GrainFactory.GetGrain<ITest>(0);
            Parallel.For(0,200,(i)=> test.AddCount(taskName));
        }
        //private static void RemoteCall(string taskName)
        //{
        //    var test = Orleans.GrainClient.GrainFactory.GetGrain<ITest>(0);

        //    test.AddCount(taskName);
        //}
    }
}
