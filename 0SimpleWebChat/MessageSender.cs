using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _0SimpleWebChat
{
    public class MessageSender : IMessageSender
    {
        TcpClient tcpClient;
        Stream streamToServer;
        public bool Connect(IPAddress ip, int port)
        {
            tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect(ip,port);
                streamToServer = tcpClient.GetStream();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool SendMessage(Message message)
        {
            try
            {
                lock (streamToServer)
                {
                    byte[] buffer = Encoding.Unicode.GetBytes(message.ToString());
                    streamToServer.Write(buffer, 0, buffer.Length);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public void SignOut()
        {
            try
            {
                if (streamToServer != null)
                    streamToServer.Dispose();
                tcpClient.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
