using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _1BasicOperationServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Server is running ...");
            IPAddress iPAddress = new IPAddress(new byte[] { 127, 0, 0, 1 });
            TcpListener tcpListener = new TcpListener(iPAddress,8500);
            tcpListener.Start();
            Console.WriteLine("Start Listening ...");

            ////打印连接到的客户端信息
            //TcpClient remoteClient = tcpListener.AcceptTcpClient();
            //Console.WriteLine($"Client Connected！LocalEndPoint:{remoteClient.Client.LocalEndPoint}->{remoteClient.Client.RemoteEndPoint}");

            Console.WriteLine("输入\"Q\"键退出");


            PrintTcpClientInfo(tcpListener);


            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;
            } while (key != ConsoleKey.Q);

            Console.ReadLine();
        }
        /// <summary>
        /// 使用异步方法AcceptTcpClientAsync，不阻塞原代码执行（循环判断服务端关闭代码）
        /// </summary>
        /// <param name="tcpListener"></param>
        /// <returns></returns>
        static async Task PrintTcpClientInfo(TcpListener tcpListener)
        {
            while (true)
            {
                TcpClient client = await tcpListener.AcceptTcpClientAsync();
                Console.WriteLine($"Client Connected！{client.Client.LocalEndPoint} <-- {client.Client.RemoteEndPoint}");
            }
        }

    }
}
