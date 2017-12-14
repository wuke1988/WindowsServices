using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace _0SimpleWebChat
{
    public delegate void ClientConnectedEventHandler(IPEndPoint endPoint);
    public delegate void MessageReceivedEventHandler(string message);
    public delegate void ConnectionLosedEventHandler(string Info);
    public interface IMessageReceiver
    {
        /// <summary>
        /// 启动监听
        /// </summary>
        void StartListen();
        /// <summary>
        /// 停止监听
        /// </summary>
        void StopListen();

        event ClientConnectedEventHandler clientConnected;

        event MessageReceivedEventHandler messageReceived;

        event ConnectionLosedEventHandler connectionLosed;
    }
}
