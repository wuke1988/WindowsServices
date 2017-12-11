using ClassLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            RegisterChannel1();

            RegisterChannel2();

            //3.添加可调用的远程对象，WellKonwnObjectMode可选择为SingleTon 或者 SingleCall 或者 客户激活
            //客户激活
            //ClientActivated();
            ServerActivatedSingleCall();

            RemotingConfiguration.ApplicationName = "SimpleRemote";

            Console.WriteLine("服务开启，按任意键退出...");
            

            Console.ReadLine();
        }
        private static void RegisterChannel1()
        {

            //1.创建通道示例
            IChannelReceiver channelReceiver = new TcpChannel(8501);
            //2.注册通道
            ChannelServices.RegisterChannel(channelReceiver,false);

            IChannelReceiver channelReceiver1 = new HttpChannel(8502);
            ChannelServices.RegisterChannel(channelReceiver1,false);
           
        }

        private static void RegisterChannel2()
        {
            //创建格式器
            IServerFormatterSinkProvider formatProvider = new BinaryServerFormatterSinkProvider();
            //设置通道相关属性
            IDictionary property = new Hashtable();
            property["port"] = 8503;

            //1.创建通道示例
            IChannelReceiver channelReceiver = new TcpChannel(property,null,formatProvider);
            //2.注册通道
            ChannelServices.RegisterChannel(channelReceiver, false);        
        }

        #region 激活方式
        /// <summary>
        ///  注册 客户激活对象 Client Activated Object
        /// </summary>
        static void ClientActivated()
        {
            Console.WriteLine("方式：Client Activated Object");
            //客户激活对象的注册方式需要使用RemotingConfiguration类型的RegisterActivatedServiceType()静态方法
            RemotingConfiguration.RegisterActivatedServiceType(typeof(DemoClass2));
        }

        private static void ServerActivatedSingleCall()
        {
            Console.WriteLine("方式: Server Activated Singleton");
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(DemoClass2), "ServerActivated", WellKnownObjectMode.SingleCall);
        }

        private static void ServerActivatedSingleton()
        {
            Console.WriteLine("方式: Server Activated SingleCall");
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(DemoClass2), "ServerActivated", WellKnownObjectMode.Singleton);
        }
        #endregion
    }
}
