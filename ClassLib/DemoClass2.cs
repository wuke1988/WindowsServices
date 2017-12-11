using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib
{
    /// <summary>
    /// 对象供跨程序域调用使用：按引用封送
    /// </summary>
    public class DemoClass2:MarshalByRefObject
    {
        private int count = 0;
        public DemoClass2()
        {
            Console.WriteLine("\n--------- DemoClass2 Constructor-------------\n");
        }
        public void ShowCount(string name)
        {
            count++;
            Console.WriteLine("{0},the count is {1}.", name, count);
        }

        public void ShowAppDomain()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            Console.WriteLine(currentDomain.FriendlyName);
        }

        public int GetCount()
        {
            return count;
        }
    }
}
