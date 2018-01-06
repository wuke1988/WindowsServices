using _2OrleansGrainStateDemo_Interface;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5OrleansWebAPIDemo_Business
{
    public class Person
    {
        public string SayHello(String Name)
        {
            var grain = GrainClient.GrainFactory.GetGrain<IPersonGrain>(Name);
            grain.SayHelloAsync();
            return "sucess";
        }
    }
}
