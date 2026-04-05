using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks; // Добавлено для работы с задачами

namespace Lab22_Variant12
{
    class Program
    {
        // Оставляем делегат для соблюдения условий лабы
        delegate int[] GetEvenNumbersDelegate(int size);

        static void Main(string[] args)
        {
            Console.WriteLine("Лабораторная работа №22. Вариант 12.");
            Console.Write("Введите размер исходного массива: ");
            
            if (!int.TryParse(Console.ReadLine(), out int arraySize)) arraySize = 10;

            // 1. Лямбда-выражение (оставляем без изменений)
            GetEvenNumbersDelegate del = (size) =>
            {
                Console.WriteLine("\n[Поток] Начата обработка...");
                Random rnd = new Random();
                int[] sourceArray = Enumerable.Range(0, size).Select(_ => rnd.Next(1, 100)).ToArray();

                Thread.Sleep(3000); // Имитация долгой работы

                return sourceArray.Where(n => n % 2 == 0).ToArray();
            };

            // 2. Асинхронный запуск (заменяем BeginInvoke на Task.Run)
            // Это современный способ запустить делегат в отдельном потоке
            Task<int[]> task = Task.Run(() => del(arraySize));

            // 3. Мониторинг процесса (оставляем логику проверки завершения)
            Console.Write("Выполнение задачи ");
            while (!task.IsCompleted)
            {
                Console.Write(".");
                Thread.Sleep(200);
            }

            // 4. Получение результата (вместо EndInvoke используем .Result)
            int[] result = task.Result;

            Console.WriteLine("\n\nРезультат (чётные числа):");
            Console.WriteLine(result.Length > 0 ? string.Join(", ", result) : "Чётных чисел нет");

            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}