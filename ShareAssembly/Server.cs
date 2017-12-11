using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShareAssembly
{
    public delegate void NumberChangedEventHandler(string name, int count);
    public class Server:MarshalByRefObject
    {
        private int count = 0;
        private string serverName = "SimpleServer";

        public event NumberChangedEventHandler NumberChanged;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DoSomething()
        {
            if (NumberChanged != null)
            {
                Delegate[] dels = NumberChanged.GetInvocationList();
                foreach (Delegate del in dels)
                {
                    NumberChangedEventHandler handler = del as NumberChangedEventHandler;
                    try
                    {
                        handler(serverName, count);
                    }
                    catch (Exception ex)
                    {
                        Delegate.Remove(NumberChanged, del);
                    }
                }
            }
        }
        public void InvokeClientAdd(Client client, int x, int y)
        {
            int result = client.Add(x, y);
            Console.WriteLine($"Invoke client method :x={x},y={y},total={result}");
        }
        public void GetCount(Client client)
        {
            Console.WriteLine($"Count value from client: {client.Count}");
        }
        public void Test()
        {
            Console.WriteLine("Hello World");
        }
    }
}
