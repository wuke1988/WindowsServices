using ClassLib;
using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace Demo1Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //客户激活测试
            //ClientActivatedDemo();

            //服务激活测试
            ServerActivatedDemo();

            Console.ReadLine();
        }
        #region 客户激活
        private static void ClientActivatedDemo()
        {
            //1.确立通道传送方式
            TcpClientChannel clientChannel = new TcpClientChannel();
            ChannelServices.RegisterChannel(clientChannel, false);

            //2.客户激活远程对象
            ClientActivated();

            RunTest("Jimmy", "Zhang");
            RunTest("Bruce", "Wang");
        }

        private static void ClientActivated()
        {
            Type t = typeof(DemoClass2);
            string url = "tcp://127.0.0.1:8501";
            RemotingConfiguration.RegisterActivatedClientType(t, url);

        }


        private static void RunTest(string firstName, string familyName)
        {
            DemoClass2 obj = new DemoClass2();

            obj.ShowAppDomain();

            obj.ShowCount(firstName);
            Console.WriteLine("{0}, the count is {1}.\n", firstName, obj.GetCount());

            obj.ShowCount(familyName);
            Console.WriteLine("{0}, the count is {1}.", familyName, obj.GetCount());
        }
        #endregion

        #region 服务激活

        private static void ServerActivatedDemo()
        {
            TcpClientChannel clientChannel = new TcpClientChannel();
            ChannelServices.RegisterChannel(clientChannel, false);
            ServerActivated();

            RunTest("Jimmy", "Zhang");
        }
        /// <summary>
        ///  如果不进行注册创建远程对象,
        ///  可以通过 RemotingServices.Connect(),
        ///  Activator.GetObject()
        ///  Activator.CreateInstance()方法来完成
        /// </summary>
        private static void ServerActivated()
        {
            Type t = typeof(DemoClass2);
            string url = "tcp://127.0.0.1:8501/SimpleRemote/ServerActivated";
            RemotingConfiguration.RegisterWellKnownClientType(t, url);
        }
        #endregion
    }
}
