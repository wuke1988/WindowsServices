using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;
using Orleans.Streams;

namespace _4OrleansStreamDemo
{
    public class ExplicitSubsriberGrain : Grain, IExplicitSubscriberGrain
    {
        protected IAsyncStream<string> stream;
        public Task ReceivedMessageAsync(string data)
        {
            Console.WriteLine($"Received Message is {data}");
            return Task.CompletedTask;
        }
        public override async Task OnActivateAsync()
        {
            var streamId = this.GetPrimaryKey();
            var streamProvider = this.GetStreamProvider("SMSProvider");
            stream = streamProvider.GetStream<string>(streamId, "_4OrleansStreamDemo");
            var subscriptionHandler = await stream.GetAllSubscriptionHandles();

            if (subscriptionHandler.Count > 0)
            {
                subscriptionHandler.ToList().ForEach(
                   async x =>
                   {
                       await x.ResumeAsync((payload, token)=>this.ReceivedMessageAsync(payload));
                   }
                   );
            }
        }
        public async Task<StreamSubscriptionHandle<string>> SubscribeAsync()
        {
            return await stream.SubscribeAsync((payload,token)=>this.ReceivedMessageAsync(payload));
        }
    }
}
