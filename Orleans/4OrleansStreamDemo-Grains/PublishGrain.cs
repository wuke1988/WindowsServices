using Orleans;
using Orleans.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4OrleansStreamDemo
{
    public class PublishGrain : Grain, IPublisherGrain
    {
        protected IAsyncStream<string> stream;

        public override Task OnActivateAsync()
        {
            var streamId = this.GetPrimaryKey();
            var streamProvider = this.GetStreamProvider("SMSProvider");
            //使用隐式订阅者
            stream = streamProvider.GetStream<string>(streamId, "_4OrleansStreamDemo");
            return base.OnActivateAsync();
        }

        public async Task PublishMessageAsync(string data)
        {
            Console.WriteLine($"Sending data: {data}");
            await stream.OnNextAsync(data);
        }
    }
}
