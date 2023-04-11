using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal class Result
    {
        public Result()
        {
            _Result();
        }

        // Чтение всех файлов с результатами и поиск наиболее короткий путь
        public static void _Result()
        {
            int result = -1;
            string job = null;
            //получаем данные из всех файлов complite
            string[] allfiles = Directory.GetFiles(Environment.CurrentDirectory);
            foreach (string filename in allfiles)
            {
                if (filename.Contains("complite"))
                {
                    using (StreamReader sr = new StreamReader(filename))
                    {
                        int i = Convert.ToInt32(sr.ReadLine());
                        int a = filename.LastIndexOf(".jb");
                        int b = filename.LastIndexOf("complite");
                        string flname = filename.Substring(b+8, a-(b+8));
                        if (result == -1) {
                            result = i; job = flname; 
                        }
                        if (result > i ) { 
                            result = i; job = flname; 
                        }
                    }
                }

            }
            Console.WriteLine($"Work Result: {result} from number job: {job}");
        }
    }
}
