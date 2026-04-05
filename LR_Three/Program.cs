using System;
using System.IO;

namespace LR_Three
{
    class Program
    {
        static void Main(string[] args)
        {
            // Сохраняем стандартные потоки и перенаправляем их в файлы
            TextWriter save_out = Console.Out;
            TextReader save_in = Console.In;
            
            // Убедись, что файлы input.txt и output.txt созданы в папке с .exe или проектом
            var new_out = new StreamWriter(@"output.txt");
            var new_in = new StreamReader(@"input.txt");
            
            Console.SetOut(new_out);
            Console.SetIn(new_in);

            int t = 0, N = 0;
            double X = 0, Y = 0, p = 0;

            // Читаем параметры: 
            // t (тип цикла), N (кол-во итераций), X и Y (значения переменных)
            try
            {
                t = Convert.ToInt32(Console.ReadLine());
                N = Convert.ToInt32(Console.ReadLine());
                X = Convert.ToDouble(Console.ReadLine());
                Y = Convert.ToDouble(Console.ReadLine());
            }
            catch { /* Обработка ошибок чтения, если нужно */ }

            int i = 1;

            // t == 0: Цикл FOR
            if (t == 0)
            {
                for (i = 1; i <= N; i++)
                {
                    p += CalculateTerm(i, X, Y);
                }
            }

            // t == 1: Цикл WHILE
            if (t == 1)
            {
                i = 1;
                while (i <= N)
                {
                    p += CalculateTerm(i, X, Y);
                    i++;
                }
            }

            // t == 2: Цикл DO WHILE
            if (t == 2)
            {
                i = 1;
                do
                {
                    if (N > 0) // Проверка, чтобы не зайти в цикл при N=0
                        p += CalculateTerm(i, X, Y);
                    i++;
                } while (i <= N);
            }

            // Вывод результата с 6 знаками после запятой
            Console.WriteLine(String.Format("{0:0.000000}", p));

            // Закрываем потоки
            Console.SetOut(save_out); new_out.Close();
            Console.SetIn(save_in); new_in.Close();
        }

        // Вспомогательный метод для расчета i-го члена ряда
        static double CalculateTerm(int i, double x, double y)
        {
            // Определяем основание (X или Y) и степень/начало знаменателя
            int n = 2 * i - 1;
            double baseValue = (i % 2 != 0) ? x : y;
            
            // Числитель: baseValue в степени n
            double numerator = Math.Pow(baseValue, n);
            
            // Знаменатель: n * (n + 1) * (n + 2)
            double denominator = (double)n * (n + 1) * (n + 2);
            
            // Знак: если i нечетное (1, 3...), то минус. Если четное (2, 4...) — плюс.
            double sign = (i % 2 != 0) ? -1.0 : 1.0;
            
            return sign * (numerator / denominator);
        }
    }
}