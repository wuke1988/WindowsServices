using _4CounterService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _4LifeTimeManagementClient
{
    class Program
    {
        static int _invocationFrequency = 4;
        //static int _invocationFrequency = 1;通过远程对象调用的方式来延长远程服务对象
        static int _leaseRenewalFrequency = 1;

        static ISponsor _singletonSponsor;
        static ISponsor _caoSponsor;

        static void Main(string[] args)
        {
            IDictionary dictionary = new Hashtable();
            dictionary["port"] = 0;

            BinaryClientFormatterSinkProvider clientFormatterSinkProvider;
            clientFormatterSinkProvider = new BinaryClientFormatterSinkProvider();

            BinaryServerFormatterSinkProvider serverFormatterSinkProvider;
            serverFormatterSinkProvider = new BinaryServerFormatterSinkProvider();
            serverFormatterSinkProvider.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;

            IChannel channel = new TcpChannel(dictionary, clientFormatterSinkProvider, serverFormatterSinkProvider);
            ChannelServices.RegisterChannel(channel,false);

            Type t = typeof(CounterService);
            string url = "tcp://127.0.0.1:8501/RemoteLifeTimeMangement";
            RemotingConfiguration.RegisterActivatedClientType(t, url);
            CounterService caoCounter = new CounterService();

            CounterService singletonCounter = Activator.GetObject(t, "tcp://127.0.0.1:8501/RemoteLifeTimeMangement/Counter.rem") as CounterService;


            Thread caothread = new Thread(InvocateCounterService);
            caothread.Name = "cao";

            Thread singletonThread = new Thread(InvocateCounterService);
            singletonThread.Name = "singleton";

            caothread.Start(caoCounter);
            singletonThread.Start(singletonCounter);

            //Thread caoExtend = new Thread(ExtendLifetimeViaLease);
            //Thread singletonExtend = new Thread(ExtendLifetimeViaLease);

            //caoExtend.Start(caoCounter);
            //singletonExtend.Start(singletonCounter);

            //把该Sposnor负值给一个静态变量,这样不会被垃圾回收
            _singletonSponsor = ExtendLifetimeViaSponsor(singletonCounter);
            _caoSponsor = ExtendLifetimeViaSponsor(caoCounter);

            Console.ReadLine();

        }
        /// <summary>
        /// 通过ClientSponsor来延长远程对象生命周期
        /// </summary>
        /// <param name="counter"></param>
        /// <returns></returns>
        static ISponsor ExtendLifetimeViaSponsor(object counter)
        {
            CounterService counterService = counter as CounterService;

            ILease lease = RemotingServices.GetLifetimeService(counterService) as ILease;

            //为Lease注册一个Sposnor，并把Renew时间设为4s
            ClientSponsor clientSponsor = new ClientSponsor(TimeSpan.FromSeconds(4));

            clientSponsor.Register(counterService);

            return clientSponsor;
        }
        /// <summary>
        /// 采用延迟租约（Lease）的方式来延长远程对象生命周期
        /// </summary>
        /// <param name="counter"></param>
        static void ExtendLifetimeViaLease(object counter)
        {
            CounterService counterService = counter as CounterService;

            ILease lease = RemotingServices.GetLifetimeService(counterService) as ILease;

            while (true)
            {
                if (lease == null)
                {
                    Console.WriteLine("Can not retrieve the lease!");
                    break;
                }

                lease.Renew(TimeSpan.FromSeconds(_leaseRenewalFrequency));
                Thread.Sleep(_leaseRenewalFrequency*950);
            }
        }
        static void InvocateCounterService(object counter)
        {
            CounterService counterService = counter as CounterService;

            while (true)
            {
                try
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name.PadRight(10)}: The count is {counterService.GetCount()}");
                    Thread.Sleep(_invocationFrequency*1000);
                }
                catch (Exception ex)
                {
                    if (Thread.CurrentThread.Name == "singleton")
                    {
                        Console.WriteLine($"Fail to invocate Singleton counter because {ex.Message}");
                        break;
                    }
                    if (Thread.CurrentThread.Name == "cao")
                    {
                        Console.WriteLine($"Fail to invocate cao counter because {ex.Message}");
                        break;
                    }
                }
            }
        }
    }
}
