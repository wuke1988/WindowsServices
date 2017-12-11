using ClassLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0AppDomainDemo
{
    /// <summary>
    /// 跨AppDomain调用对象
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            Console.WriteLine(currentDomain.FriendlyName);

            MarshalByValueDemo();

            MarshalByObjectDemo();

            Console.ReadLine();
        }
        static void MarshalByValueDemo()
        {
            Console.WriteLine("--------MarshalByValueDemo------------");

            // 创建一个新的应用程序域 - NewDomain
            AppDomain newDomain = AppDomain.CreateDomain("NewDomain");

            DemoClass obj;

            obj = (DemoClass)newDomain.CreateInstanceAndUnwrap("ClassLib", "ClassLib.DemoClass");

            obj.ShowAppDomain();
            obj.ShowCount("Jimmy");
            obj.ShowCount("Jimmy");

            Console.WriteLine("------------End---------------");
        }

        static void MarshalByObjectDemo()
        {
            Console.WriteLine("--------MarshalByObjectDemo------------");

            // 创建一个新的应用程序域 - NewDomain
            AppDomain newDomain = AppDomain.CreateDomain("NewDomain");

            DemoClass2 obj;
            DemoClass2 obj2;

            obj = (DemoClass2)newDomain.CreateInstanceAndUnwrap("ClassLib", "ClassLib.DemoClass2");

            obj.ShowAppDomain();
            obj.ShowCount("Zhang");
            obj.ShowCount("Zhang");

            obj2 = (DemoClass2)newDomain.CreateInstanceAndUnwrap("ClassLib", "ClassLib.DemoClass2");
            obj2.ShowAppDomain();
            obj2.ShowCount("Jimmy");
            obj2.ShowCount("Jimmy");


            Console.WriteLine("------------End---------------");
        }
    }
}
