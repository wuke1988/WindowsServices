using SharedLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLib
{
    public class ServerFactory:MarshalByRefObject,IServerFactory
    {
        public IDemoClass GetDemoClass()
        {
            DemoClassRealization demo = new DemoClassRealization();
            return demo;
        }
    }
}
