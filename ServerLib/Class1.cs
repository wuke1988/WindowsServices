using SharedLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLib
{
    public class DemoClassRealization : MarshalByRefObject,IDemoClass
    {
        private int count = 0;
        public int GetCount()
        {
            return count;
        }

        public DemoCount GetNewCount()
        {
            return new DemoCount(count);
        }

        public void ShowAppDomain()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            Console.WriteLine(currentDomain.FriendlyName);
        }

        public void ShowCount(string name)
        {
            count++;
            Console.WriteLine("{0},the count is {1}.", name, count);
        }
    }
}
