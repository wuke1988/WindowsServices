using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _0SimpleWebChat
{
    public delegate void PortNumberBeReadyEventHandler(int port);
    public class MessageReceiver : IMessageReceiver
    {
        public event ClientConnectedEventHandler clientConnected;
        public event MessageReceivedEventHandler messageReceived;
        public event ConnectionLosedEventHandler connectionLosed;

        public event PortNumberBeReadyEventHandler portNumberBeReady;

        private TcpListener tcpListener;
        private Thread receiverThread;

        public MessageReceiver()
        {
            StartListen();
        }

        public void StartListen()
        {
            receiverThread = new Thread(ListenTreadMethod);
            receiverThread.IsBackground = true;
            receiverThread.Start();
        }

        private void ListenTreadMethod()
        {
            IPAddress iPAddress = new IPAddress(new byte[] { 127, 0, 0, 1 });
            tcpListener = new TcpListener(iPAddress, 0);

            tcpListener.Start();

            // 1.Notify UI port is ready
            IPEndPoint endPoint = tcpListener.LocalEndpoint as IPEndPoint;
            int port = endPoint.Port;

            if (portNumberBeReady != null)
                portNumberBeReady(port);

            // 2.循环监测是否有客户链接
            // 如果有，则通知链接成功

            TcpClient remoteClient;
            while (true)
            {
                try
                {
                    remoteClient = tcpListener.AcceptTcpClient();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }

                if (remoteClient != null)
                {
                    IPEndPoint remoteEndPoint = remoteClient.Client.RemoteEndPoint as IPEndPoint;
                    if (clientConnected != null)
                    {
                        clientConnected(remoteEndPoint);
                    }
                    // 读取发送的数据
                    byte[] buffer = new byte[8192];
                    // 及时Stream清理对象
                    using (Stream stream = remoteClient.GetStream())
                    {
                        while (true)
                        {
                            try
                            {
                                int byteRead = stream.Read(buffer, 0, 8192);
                                if (byteRead == 0)
                                {
                                    throw new Exception("客户端已断开连接");
                                }
                                string message = Encoding.Unicode.GetString(buffer,0, byteRead);

                                if (messageReceived != null)
                                    messageReceived(message);
                            }
                            catch (Exception ex)
                            {
                                if (remoteClient != null)
                                {
                                    if (connectionLosed != null)
                                        connectionLosed(ex.Message);
                                    break;// 退出当前客户端链接
                                }
                            }
                        }
                    }                        
                }
            }
        }
        public void StopListen()
        {
            if (tcpListener != null)
            {
                try
                {
                    tcpListener.Stop();
                    tcpListener = null;
                    receiverThread.Abort();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
