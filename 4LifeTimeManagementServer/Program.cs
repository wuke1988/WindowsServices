using _4CounterService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _4LifeTimeManagementServer
{
    class Program
    {
        static void Main(string[] args)
        {
            InitialService();
            GCCollect();
            Console.ReadLine();
        }
        static void GCCollect()
        {
            while (true)
            {
                Thread.Sleep(10000);
                GC.Collect();
            }
        }
        static void InitialService()
        {            
            IDictionary dictionary = new Hashtable();
            dictionary["port"] = 8501;

            //服务端通道格式接收器
            BinaryServerFormatterSinkProvider serverProvider = new BinaryServerFormatterSinkProvider();
            serverProvider.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
            
            //客户端通道格式接收器
            BinaryClientFormatterSinkProvider clientProvider = new BinaryClientFormatterSinkProvider();

            //1.创建通道示例
            IChannel channel = new TcpChannel(dictionary, clientProvider,serverProvider);
            //2.注册通道
            ChannelServices.RegisterChannel(channel,false);

            //3.注册远程对象类型
            //服务端激活类型
            Type type = typeof(CounterService);
            RemotingConfiguration.RegisterWellKnownServiceType(type, "Counter.rem", WellKnownObjectMode.Singleton);
            Console.WriteLine("方式: Server Activated Singleton");

            //客户端激活类型
            RemotingConfiguration.RegisterActivatedServiceType(type);
            Console.WriteLine("方式: Client Activated Object");

            RemotingConfiguration.ApplicationName = "RemoteLifeTimeMangement";

            
            Console.WriteLine("Remote LifeTime Mangement Server Start......");
        }
    }
}
