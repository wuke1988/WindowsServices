using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib
{
    public interface IDemoClass
    {
        int GetCount();
        DemoCount GetNewCount();
        void ShowAppDomain();
        void ShowCount(string name);
    }
    [Serializable]
    public struct DemoCount
    {
        private readonly int count;
        public DemoCount(int count)
        {
            this.count = count;
        }
        public int Count
        {
            get { return count; }
        }
        public void ShowAppDomain()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            Console.WriteLine(currentDomain.FriendlyName);
        }
    }
}
