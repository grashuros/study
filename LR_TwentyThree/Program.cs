using System;
using System.Threading;

namespace Lab23_Variant12
{
    // Структура для хранения параметров матрицы
    public struct MatrixParams
    {
        public int Rows;
        public int Cols;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Лабораторная работа №23 (Вариант 12) ===");

            try
            {
                // 1. Ввод параметров пользователем
                Console.Write("Введите количество строк матрицы: ");
                int rows = int.Parse(Console.ReadLine());

                Console.Write("Введите количество столбцов матрицы: ");
                int cols = int.Parse(Console.ReadLine());

                // Заполняем структуру данными
                MatrixParams mp = new MatrixParams { Rows = rows, Cols = cols };

                // 2. Создание и запуск параметризированного потока
                // Используем делегат ParameterizedThreadStart
                Thread myThread = new Thread(new ParameterizedThreadStart(CalculateMatrixSum));

                Console.WriteLine("\n[Main]: Запуск фонового потока...");
                myThread.Start(mp);

                // Ожидаем завершения потока, чтобы консоль не закрылась раньше времени
                myThread.Join();
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: Введено не число!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }

            Console.WriteLine("\n[Main]: Работа программы завершена. Нажмите любую клавишу...");
            Console.ReadKey();
        }

        // Метод, который будет выполняться в отдельном потоке
        static void CalculateMatrixSum(object obj)
        {
            // Распаковываем объект обратно в структуру
            MatrixParams p = (MatrixParams)obj;
            
            Random rnd = new Random();
            int[,] matrix = new int[p.Rows, p.Cols];
            long totalSum = 0;

            Console.WriteLine($"[Thread]: Генерация матрицы {p.Rows}x{p.Cols}...");

            // Заполнение матрицы и расчет суммы
            for (int i = 0; i < p.Rows; i++)
            {
                for (int j = 0; j < p.Cols; j++)
                {
                    matrix[i, j] = rnd.Next(1, 11); // Числа от 1 до 10 для простоты проверки
                    totalSum += matrix[i, j];
                    
                    // Выводим элемент (если матрица не слишком большая)
                    if (p.Rows <= 15 && p.Cols <= 15)
                        Console.Write(matrix[i, j] + "\t");
                }
                if (p.Rows <= 15) Console.WriteLine();
            }

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine($"[Thread]: Расчет окончен. Сумма всех элементов = {totalSum}");
        }
    }
}