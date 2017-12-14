using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace _0SimpleWebChat
{
    public interface IMessageSender
    {
        /// <summary>
        /// 建立连接
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        bool Connect(IPAddress ip, int port);
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        bool SendMessage(Message message);
        /// <summary>
        /// 注销系统
        /// </summary>
        void SignOut();
    }
}
