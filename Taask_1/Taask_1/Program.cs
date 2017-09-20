using System;
using System.IO;

namespace Task_1
{
    class Program
    {
        static int Min(int[] arr, int beg, int end) //функция нахождения минимального значения на интервале от beg до end 
        {
            int min = Int32.MaxValue;
            for (int j = beg; j < end; j++)
            {
                if (arr[j] < min)
                {
                    min = arr[j];
                }
            }
            return min;
        }

        static void Main(string[] args)
        {
            try
            {
                using (StreamReader sr = new StreamReader("INPUT.txt")) //ввод из файла 
                {
                    int k = Convert.ToInt32(sr.ReadLine()); //количество строк K 
                    string[] answer = new string[k]; //создание массива из K-элементов, где будут храниться ответы 
                    for (int ki = 0; ki < k; ki++) //построчная обработка 
                    {
                        string inputStr = sr.ReadLine(); //считывание строки 
                        string[] spl = inputStr.Split();
                        int n = Convert.ToInt32(spl[0]); //количество охранников N 
                        int[] beg = new int[n]; //массив начал смен N охранников 
                        int[] end = new int[n]; //массив концов смен N охранников 
                        int[] intervals = new int[10000]; //массив под единичные интервалы времени 
                        for (int i = 0; i < 10000; i++)
                        {
                            intervals[i] = 0;
                        }
                        for (int i = 1; i < n * 2 + 1; i++) //попарная запись в массивы начал и концов смен 
                        {
                            if (i % 2 == 1)
                                beg[(i - 1) / 2] = Convert.ToInt32(spl[i]);
                            else
                                end[(i - 1) / 2] = Convert.ToInt32(spl[i]);
                        }
                        for (int i = 0; i < n; i++) //проверяем каждого охранника 
                        {
                            for (int j = beg[i]; j < end[i]; j++) //если на единичный интервал попадает смена охранника, то добавляем 1. 
                            {
                                intervals[j]++;
                            } //в итоге получится, что каждому единичному интервалу соответствует количесто охранников, смена которых попадает на этот интервал 
                        }
                        for (int i = 0; i < n; i++) //проверяем каждого охранника 
                        {
                            if (Min(intervals, beg[i], end[i]) == 1) //если охранник незаменим в свою смену, то есть нельзя беспрепятственно 
                            { //освободить его от работы, то ответ будет Accepted 
                                answer[ki] = "Accepted";
                            }
                            else
                            {
                                answer[ki] = "Wrong Answer"; //если есть интервалы времени, где работает более 1 охранника, то есть одного 
                                break; //можно убрать, то Wrong Answer 
                            }
                        }
                        if (Min(intervals, 0, 10000) == 0) //если есть интервалы времени, где охранники не работают, то Wrong Answer 
                        {
                            answer[ki] = "Wrong Answer";
                        }
                        //Console.WriteLine(answer[ki]); 
                    }
                    File.WriteAllLines("OUTPUT.txt", answer); //вывод в файл 

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
