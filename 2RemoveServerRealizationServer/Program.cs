using ServerLib;
using SharedLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace _2RemoveServerRealizationServer
{
    class Program
    {
        static void Main(string[] args)
        {
            RegisterChannel();

            //ServerActivated();
            ServerFactoryActivated();

            RemotingConfiguration.ApplicationName = "RemovedDemo";

            Console.ReadLine();
        }
        private static void RegisterChannel()
        {
            //1.创建通道示例
            IChannelReceiver channelReceiver = new TcpChannel(8501);
            //2.注册通道
            ChannelServices.RegisterChannel(channelReceiver, false);

            IChannelReceiver channelReceiver1 = new HttpChannel(8502);
            ChannelServices.RegisterChannel(channelReceiver1, false);
        }
        private static void ServerActivated()
        {
            Console.WriteLine("方式: Server Activated Singleton");
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(DemoClassRealization),"ServerActivated",WellKnownObjectMode.Singleton);
            Console.WriteLine("服务已启动");
        }
        private static void ServerFactoryActivated()
        {
            Console.WriteLine("方式: Server Activated Factory Singleton");
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(ServerFactory), "ServerFactotyActivated", WellKnownObjectMode.SingleCall);
            Console.WriteLine();
        }
    }
}
