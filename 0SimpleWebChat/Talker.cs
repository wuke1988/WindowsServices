using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace _0SimpleWebChat
{
    public class Talker:IDisposable
    {
        private IMessageReceiver receiver;
        private IMessageSender sender;

        public Talker(IMessageReceiver receiver, IMessageSender sender)
        {
            this.receiver = receiver;
            this.sender = sender;
        }
        public Talker()
        {
            this.receiver = new MessageReceiver();
            this.sender = new MessageSender();
        }
        public event ClientConnectedEventHandler ClientConnected
        {
            add { receiver.clientConnected += value; }
            remove { receiver.clientConnected -= value; }
        }
        public event MessageReceivedEventHandler MessageRecieved
        {
            add { receiver.messageReceived += value; }
            remove { receiver.messageReceived -= value; }
        }
        public event ConnectionLosedEventHandler ConnectionLosed
        {
            add { receiver.connectionLosed += value; }
            remove { receiver.connectionLosed -= value; }
        }
        public event PortNumberBeReadyEventHandler PortNumberBrReady
        {
            add { ((MessageReceiver)receiver).portNumberBeReady += value; }
            remove { ((MessageReceiver)receiver).portNumberBeReady -= value; }
        }

        public bool SendMessage(Message message)
        {
            return sender.SendMessage(message);
        }
        public bool ConnectByIp(IPAddress ip, int port)
        {           
           return sender.Connect(ip, port);
        }
        public void Dispose()
        {
            receiver.StopListen();
            sender.SignOut();
        }
        public void SignOut()
        {
            sender.SignOut();
        }

    }
}
