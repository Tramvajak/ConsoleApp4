using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Создание подзадач
            new CreateWork();


            Console.WriteLine();

            // поиск всех подзадач в корневом каталоге
            int i = 0;
            string[] allfiles = Directory.GetFiles(Environment.CurrentDirectory);
            foreach (string filename in allfiles)
            {
                if (filename.Contains("job") && filename.Contains(".jb"))
                {
                    // Выполнение позадач
                    new RunWork(++i);
                    Console.WriteLine("=======");
                }

            }

            // результат выполнение позадач
            new Result();

            Console.ReadLine();
        }
    }
}
