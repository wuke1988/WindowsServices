using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3.AsyncSendMesageServer
{
    class Program
    {
        static void Main(string[] args)
        {
            RequestHandler handler = new RequestHandler();
            string input;

            // 第一种情况测试 - 一条消息完整发送
            input = "[length=13]明天中秋，祝大家节日快乐！";
            handler.PrintOutput(input);
            Console.WriteLine();

            // 第二种情况测试 - 两条完整消息一次发送
            input = "明天中秋，祝大家节日快乐！";
            input = String.Format
                ("[length=13]{0}[length=13]{0}", input);
            handler.PrintOutput(input);
            Console.WriteLine();

            // 第三种情况测试A - 两条消息不完整发送
            input = "[length=13]明天中秋，祝大家节日快乐！[length=13]明天中秋";
            handler.PrintOutput(input);

            input = "，祝大家节日快乐！";
            handler.PrintOutput(input);
            Console.WriteLine();

            // 第三种情况测试B - 两条消息不完整发送
            input = "[length=13]明天中秋，祝大家";
            handler.PrintOutput(input);

            input = "节日快乐！[length=13]明天中秋，祝大家节日快乐！";
            handler.PrintOutput(input);
            Console.WriteLine();


            // 第四种情况测试 - 元数据不完整
            input = "[leng";
            handler.PrintOutput(input);     // 不会有输出

            input = "th=13]明天中秋，祝大家节日快乐！";
            handler.PrintOutput(input);
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
