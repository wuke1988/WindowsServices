using _1OlreansTestDemo_Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1OrleansTestDemo_Grains
{
    public class TestGrain : Orleans.Grain, ITest
    {
        private int num = 0;

        public Task AddCount(string taskName)
        {
            Console.WriteLine($"{taskName}-----------------{num}");
            num++;
            return Task.CompletedTask;
        }
    }
}
