using ShareAssembly;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace _3RemoteCallbackClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Initail();
            
            //创建远程对象
            Server server = new Server();
            //创建本地对象
            Client client = new Client();

            server.Test();

            server.NumberChanged += new NumberChangedEventHandler(client.OnNumberChanged);

            server.DoSomething();
            server.InvokeClientAdd(client, 10, 9);
            server.GetCount(client);

            Console.ReadLine();
        }
        static void Initail()
        {        
            IDictionary dictionary = new Hashtable();
            dictionary["port"] = 0;

            //客户端通道格式接收器
            BinaryClientFormatterSinkProvider clientProvider = new BinaryClientFormatterSinkProvider();
            //服务端通道格式接收器
            BinaryServerFormatterSinkProvider serverProvider = new BinaryServerFormatterSinkProvider();
            serverProvider.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
            
            //1.注册通道
            IChannel channel = new TcpChannel(dictionary, clientProvider, serverProvider);
            ChannelServices.RegisterChannel(channel,false);

            //2.注册远程服务对象
            string url = "tcp://127.0.0.1:8501/RemoteCallbackServer/ServerActivated";
            RemotingConfiguration.RegisterWellKnownClientType(typeof(Server),url);                     
        }
    }
}
