using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareAssembly
{
    public class Client:MarshalByRefObject
    {
        private int count = 0;

        public int Add(int x, int y)
        {
            //当有服务端调用时，打印下面一行
            Console.WriteLine($"Add Callback:x={x},y={y}");
            return x + y;
        }

        public int Count
        {
            get
            {
                count++;
                return count;
            }
        }

        public void OnNumberChanged(string name, int count)
        {
            Console.WriteLine("OnNumberChanged callback:");
            Console.WriteLine($"Servername={name},Server.count={count}");
        }
    }
}
