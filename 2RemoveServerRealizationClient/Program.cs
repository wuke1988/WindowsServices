using SharedLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace _2RemoveServerRealizationClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //1.确立通道传送方式
            TcpClientChannel clientChannel = new TcpClientChannel();
            ChannelServices.RegisterChannel(clientChannel, false);

            //因为客户端仅引用了IDemoClass，所以无法通过new 对象的方式创建远程对象。
            //所以无法使用客户端注册远程对象（RemotingConfiguration.RegisterWellKnownClientType）。
            //故而只能采用Activator.GetObject()方式来创建远程对象
            //IDemoClass demo =(IDemoClass)Activator.GetObject(typeof(IDemoClass), "tcp://127.0.0.1:8501/RemovedDemo/ServerActivated");
            //demo.ShowAppDomain();
            //demo.ShowCount("Zhang");

            IServerFactory serverFactory = (IServerFactory)Activator.GetObject(typeof(IServerFactory), "tcp://127.0.0.1:8501/RemovedDemo/ServerFactotyActivated");
            IDemoClass demo= serverFactory.GetDemoClass();
            demo.ShowAppDomain();
            demo.ShowCount("Zhang");

            Console.ReadLine();
        }
    }
}
