using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _1BasicOperationDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // SingleClient();
            MultiCLient();

            Console.ReadLine();
        }
        /// <summary>
        /// 单个客户端与服务端连接
        /// </summary>
        static void SingleClient()
        {
            Console.WriteLine("Client Running ...");

            TcpClient tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect("localhost", 8500);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine($"Server Connected！{tcpClient.Client.LocalEndPoint}-{tcpClient.Client.RemoteEndPoint}");
        }
        /// <summary>
        /// 一个服务端口可以对应多个客户端口
        /// </summary>
        static void MultiCLient()
        {
            Console.WriteLine("Client Running ...");
            TcpClient tcpClient;

            for (int i = 0; i <= 2; i++)
            {
                try
                {
                    // 一个TcpClient对象对应一个Socket，一个Socket对应着一个端口，
                    // 如果不使用new操作符重新创建对象，那么就相当于使用一个已经与服务端建立了连接的端口再次与远程建立连接
                    tcpClient = new TcpClient();
                    tcpClient.Connect("localhost", 8500);

                    Thread.Sleep(4000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
                Console.WriteLine($"Server Connected！{tcpClient.Client.LocalEndPoint}-{tcpClient.Client.RemoteEndPoint}");
            }
        }
    }
}
