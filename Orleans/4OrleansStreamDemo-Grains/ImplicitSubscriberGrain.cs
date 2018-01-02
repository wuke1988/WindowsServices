using Orleans;
using Orleans.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4OrleansStreamDemo
{
    [ImplicitStreamSubscription("_4OrleansStreamDemo")]
    public class ImplicitSubscriberGrain : Grain, IImplicitSubscriberGrain, IAsyncObserver<String>
    {
        protected StreamSubscriptionHandle<String> streamSubscriptionHandler;

        public override async Task OnActivateAsync()
        {
            var streamId = this.GetPrimaryKey();
            var streamProvider = this.GetStreamProvider("SMSProvider");
            var stream = streamProvider.GetStream<string>(streamId, "_4OrleansStreamDemo");
            streamSubscriptionHandler = await stream.SubscribeAsync(OnNextAsync);
        }
        public override async Task OnDeactivateAsync()
        {
            if (streamSubscriptionHandler != null)
            {
                await streamSubscriptionHandler.UnsubscribeAsync();
            }
        }

        public Task OnCompletedAsync()
        {
            return Task.CompletedTask;
        }

        public Task OnErrorAsync(Exception ex)
        {
            return Task.CompletedTask;
        }

        public Task OnNextAsync(string item, StreamSequenceToken token = null)
        {
            Console.WriteLine($"Received Message is {item}");
            return Task.CompletedTask;
        }
    }
}
