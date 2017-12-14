using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0SimpleWebChat
{
    public struct Message
    {
        private readonly string userName;
        private readonly string content;
        private readonly DateTime postDate;

        public Message(string userName, string content, DateTime dateTime)
        {
            this.userName = userName;
            this.content = content;
            this.postDate = dateTime;
        }

        public string UserName { get { return userName; } }
        public string Content { get { return content; } }
        public DateTime PostDate { get { return postDate; } }

        public override string ToString()
        {
            return $"{userName}[{postDate}]:\r\n{content}\r\n";
        }
    }
}
