using ShareAssembly;
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

namespace _3RemoteCallbackServer
{
    class Program
    {
        static void Main(string[] args)
        {
            InitialServer();

        }
        static void InitialServer()
        {
            IDictionary dictionary = new Hashtable();
            dictionary["port"] = 8501;
            
            //服务端通道格式接收器
            BinaryServerFormatterSinkProvider severFormatter;
            severFormatter = new BinaryServerFormatterSinkProvider();
            severFormatter.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;

            //客户端通道格式接收器
            BinaryClientFormatterSinkProvider clientFormatter=new BinaryClientFormatterSinkProvider();

            //1.创建通道示例
            IChannel channel = new TcpChannel(dictionary, clientFormatter, severFormatter);
            //2.注册通道
            ChannelServices.RegisterChannel(channel, false);


            //3.注册服务对象类型
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(Server),"ServerActivated",WellKnownObjectMode.Singleton);
            RemotingConfiguration.ApplicationName = "RemoteCallbackServer";

            Console.WriteLine("方式: Server Activated Singleton");
            Console.WriteLine("服务已启动");

            Console.ReadLine();
        }
    }
}
