using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal class CreateWork
    {
        private static int CountWork = 0;
        public CreateWork()
        {
            LoadFromFile();
        }

        // Чтение задание из файла 
        // считывается матрица смежности с длиной маршрута
        private static void LoadFromFile()
        {
            string text = null;
            using (StreamReader sr = new StreamReader("job.txt"))
            {
                text = sr.ReadToEnd();
            }

            int _i = text.LastIndexOf(", " + Environment.NewLine);

            int _N = Int32.Parse(text.Substring(text.LastIndexOf("N=") + 2));

            text = text.Substring(0, text.Length - 5);



            string _mass = text.Substring(0, _i + 2);


            string _text = text.Substring(_i + 4);
            string[] arr = _text.ToCharArray().Select(c => c.ToString()).ToArray();


            GetPer(arr, ParseStringToMass(_mass), _N);
            Console.WriteLine($"Create {CountWork} work!");
        }
        // Разбиение на подзадачи
        // из матрицы смежности графа берется маршрут имеющий длину N и не 
        // проходящий через один и тот же город дважды. Получаем матрицу NxN с необохомым маршрутом.
        // и после этого записывается в файл для отправки на другую машину для выполнения задачи.
        private static void GetPer(string[] list, int[][] mass, int N)
        {
            string[] path = new string[N];

            int[][] pathmass = new int[N][];

            // разбивания матрицы на части 
            for (int i = 0; i < N; i++)
            {
                path[i] = list[0];
                list = list.Skip(1).ToArray();

            }
            if (path.Length == N)
            {
                // удаления повторяющего маршрута

                for (int i = 0; i < N; i++)
                {
                    pathmass[i] = new int[N];
                    for (int j = 0; j < N; j++)
                    {
                        pathmass[i][j] = mass[i][j];
                    }
                }
                mass = mass.Skip(N).ToArray();
                for (int g = 0; g < mass.Length; g++)
                {
                    mass[g] = mass[g].Skip(N).ToArray();
                }

                // сохранение в файл job#.jb
                string text = ParseMassToString(pathmass) + String.Join("", path);
                SaveFromFile(String.Format("job{0}.jb", ++CountWork), text);
            }
            if (list.Length >= N)
            {
                GetPer(list, mass, N);
            }
        }
        // метод сохранения файла
        private static void SaveFromFile(string filename, string text)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.WriteLine(text);
            }
            Console.WriteLine($"Save from {filename}");
        }
        // метот конвертации из текста в массив вес ребер
        private static int[][] ParseStringToMass(string text)
        {
            int x = 0;

            text = text.Replace(" ", String.Empty);

            string[] _t = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            int[][] mass = new int[_t.Length][];

            foreach (var item in _t)
            {

                List<int> values = new List<int>();
                string[] _temp = item.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                mass[x] = new int[_temp.Length];
                for (int i = 0; i < _temp.Length; i++)
                {
                    mass[x][i] = Int32.Parse(_temp[i].ToString());
                }
                x++;
            }

            return mass;

        }
        // метот конвертации из массив вес ребер в текст
        private static string ParseMassToString(int[][] mass)
        {
            string text = null;
            foreach (var item in mass)
            {
                foreach (var _item in item)
                {
                    text = text + _item + ", ";
                }
                text = text + Environment.NewLine;
            }
            return text;
        }


    }
}