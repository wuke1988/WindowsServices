using _2OrleansGrainStateDemo_Interface;
using Orleans;
using Orleans.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2OrleansGrainStateDemo_Grains
{
    [StorageProvider(ProviderName ="OrleansStorage")]
    public class PersonGrain : Grain<PersonGrainState>, IPersonGrain
    {
        public async Task SayHelloAsync()
        {
            string primarykey = this.GetPrimaryKeyString();

            bool saidHelloBefore = this.State.SaidHello;

            string saidHelloBeforeStr = saidHelloBefore ? "already" : null;

            Console.WriteLine($"{primarykey} {saidHelloBeforeStr} said Hello!");

            saidHelloBefore = true;

           await this.WriteStateAsync();
        }
    }
}
