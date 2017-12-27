using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _3.AsyncSendMesageServer
{
    public class RequestHandler
    {
        private string temp;

        public string[] GetActualString(string input)
        {
            return GetActualString(input,null);
        }

        private string[] GetActualString(string input, List<string> outputList)
        {
            if (outputList == null)
                outputList = new List<string>();

            if (!string.IsNullOrEmpty(temp))
            {
                input = temp + input ;
            }

            string pattern = @"(?<=^\[length=)(\d+)(?=\])";
            if (Regex.IsMatch(input, pattern))
            {
                Match match = Regex.Match(input, pattern);

                int length = Convert.ToInt32(match.Groups[0].Value);

                int startPosition = input.IndexOf("]");

                int inputStrLength = input.Length - startPosition - 1;

                // 去掉[length]剩余的字符串
                string output = input.Substring(startPosition + 1);

                if (inputStrLength == length)
                {
                    // 每次取整字符串后
                    // 将临时缓存清空
                    outputList.Add(output);
                    temp = "";
                }
                else if (inputStrLength < length)
                {
                    // 如果之后的长度小于应有的长度，
                    // 说明没有发完整，则应将整条信息，包括元数据，全部缓存
                    // 与下一条数据合并起来再进行处理
                    temp = input;
                }
                else if (inputStrLength > length)
                {
                    // 如果之后的长度大于应有的长度，
                    // 说明消息发完整了，但是有多余的数据
                    // 多余的数据可能是截断消息，也可能是多条完整消息

                    outputList.Add(output.Substring(0, length));
                    temp = "";

                    input = output.Substring(length);
                    // 递归调用
                    GetActualString(input, outputList);
                }
            }
            else
            {
                temp = input;
            }

            return outputList.ToArray();

        }

        public void PrintOutput(string input)
        {
            Console.WriteLine($"输入:{input}");
            string[] outputs = GetActualString(input); 
            foreach (string output in outputs)
                Console.WriteLine($"输出:{output}");
        }
    }
}
