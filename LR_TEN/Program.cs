using System;
using System.Globalization;
using System.IO;
using LR_Ten;

namespace LR_Ten
{

    
    class Matrix
    {
        private float[,] matrix;
        public int Rows { get; private set; }  // m
        public int Cols { get; private set; }  // n

        // Конструктор
        public Matrix() { }

        // Конструктор с размерами
        public Matrix(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            matrix = new float[rows, cols];
        }

        // Генерация матрицы заданного размера
        public void GenerateMatrix(int M, int N)
        {
            Rows = M;
            Cols = N;

            Random r = new Random(DateTime.Now.Millisecond);
            matrix = new float[M, N];

            for (int i = 0; i < M; i++)
                for (int j = 0; j < N; j++)
                    matrix[i, j] = (float)r.Next(1000) / 973f;
        }

        // Сохранение сгенерированной матрицы в файл
        public void SaveMatrix(string pFileName)
        {
            if (matrix != null && matrix.Length > 0)
            {
                if (File.Exists(pFileName))
                    File.Delete(pFileName);

                using (StreamWriter sw = new StreamWriter(pFileName))
                {
                    sw.WriteLine(Rows.ToString());
                    sw.WriteLine(Cols.ToString());

                    for (int i = 0; i < Rows; i++)
                        for (int j = 0; j < Cols; j++)
                            sw.WriteLine($"{i} {j} {matrix[i, j].ToString("E10", CultureInfo.InvariantCulture)}");
                }
            }
        }

        // Загрузка сохраненной матрицы из файла
        public bool LoadMatrix(string pFileName)
        {
            if (!File.Exists(pFileName))
                return false;

            try
            {
                using (StreamReader sr = new StreamReader(pFileName))
                {
                    Rows = Convert.ToInt32(sr.ReadLine());
                    Cols = Convert.ToInt32(sr.ReadLine());

                    matrix = new float[Rows, Cols];

                    for (int i = 0; i < Rows; i++)
                        for (int j = 0; j < Cols; j++)
                        {
                            string line = sr.ReadLine();
                            string[] parts = line.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                            // parts[2] — значение элемента матрицы
                            matrix[i, j] = float.Parse(parts[2], CultureInfo.InvariantCulture);
                        }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Вывод матрицы в консоль
        public void PrintMatrix(string name = "Матрица")
        {
            if (matrix == null || matrix.Length == 0)
            {
                Console.WriteLine("Матрица не загружена или пуста");
                return;
            }

            Console.WriteLine($"{name} ({Rows}x{Cols}):");
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                    Console.Write(matrix[i, j].ToString("E3") + " ");
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Сумма элементов в чётных строках (строки с индексом 2, 4, 6...)
        /// Нумерация строк с 1, поэтому чётные строки: 2, 4, 6...
        /// </summary>
        public float SumEvenRows()
        {
            if (matrix == null || matrix.Length == 0)
                throw new InvalidOperationException("Матрица пуста");

            float sum = 0;
            for (int i = 0; i < Rows; i++)
            {
                // Строка i+1 (нумерация с 1). Если чётная — суммируем все элементы строки
                if ((i + 1) % 2 == 0)
                {
                    for (int j = 0; j < Cols; j++)
                        sum += matrix[i, j];
                }
            }
            return sum;
        }

        internal float SumSecondaryDiagonal()
        {
            throw new NotImplementedException();
        }

        internal float SumEvenRowsAndSecondaryDiagonal()
        {
            throw new NotImplementedException();
        }
    }

    
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();

            Console.WriteLine("=== Программа расчёта суммы элементов чётных строк и второстепенной диагонали ===\n");

            // Создаём две матрицы
            Matrix matrixA = new Matrix();
            Matrix matrixB = new Matrix();

            // Генерируем и сохраняем в файлы (для примера)
            Console.WriteLine("Генерация и сохранение матриц в файлы...");
            matrixA.GenerateMatrix(5, 5);   // квадратная для наглядности диагонали
            matrixB.GenerateMatrix(4, 6);   // прямоугольная
            
            matrixA.SaveMatrix("MatrixA.txt");
            matrixB.SaveMatrix("MatrixB.txt");
            Console.WriteLine("Матрицы сохранены в MatrixA.txt и MatrixB.txt\n");

            // Очищаем объекты (симуляция новой загрузки)
            matrixA = new Matrix();
            matrixB = new Matrix();

            // Загружаем матрицы из файлов
            Console.WriteLine("Загрузка матриц из файлов...");
            
            if (!matrixA.LoadMatrix("MatrixA.txt"))
            {
                Console.WriteLine("Ошибка загрузки MatrixA.txt");
                Console.ReadKey();
                return;
            }
            
            if (!matrixB.LoadMatrix("MatrixB.txt"))
            {
                Console.WriteLine("Ошибка загрузки MatrixB.txt");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Матрицы успешно загружены!\n");

            // Выводим обе матрицы
            matrixA.PrintMatrix("Матрица A");
            matrixB.PrintMatrix("Матрица B");

            // Вычисляем суммы для первой матрицы
            float sumEvenRowsA = matrixA.SumEvenRows();
            float sumSecondaryDiagA = matrixA.SumSecondaryDiagonal();
            float sumCombinedA = matrixA.SumEvenRowsAndSecondaryDiagonal();

            // Вычисляем суммы для второй матрицы
            float sumEvenRowsB = matrixB.SumEvenRows();
            float sumSecondaryDiagB = matrixB.SumSecondaryDiagonal();
            float sumCombinedB = matrixB.SumEvenRowsAndSecondaryDiagonal();

            // Выводим результаты
            Console.WriteLine("\n=== РЕЗУЛЬТАТЫ ДЛЯ МАТРИЦЫ A ===");
            Console.WriteLine($"Сумма элементов в чётных строках:    {sumEvenRowsA:E6}");
            Console.WriteLine($"Сумма элементов на второстепенной диагонали: {sumSecondaryDiagA:E6}");
            Console.WriteLine($"Сумма (чётные строки ИЛИ диагональ): {sumCombinedA:E6}");

            Console.WriteLine("\n=== РЕЗУЛЬТАТЫ ДЛЯ МАТРИЦЫ B ===");
            Console.WriteLine($"Сумма элементов в чётных строках:    {sumEvenRowsB:E6}");
            Console.WriteLine($"Сумма элементов на второстепенной диагонали: {sumSecondaryDiagB:E6}");
            Console.WriteLine($"Сумма (чётные строки ИЛИ диагональ): {sumCombinedB:E6}");

            // Итоговая сумма обеих матриц (по заданию: "в обеих матрицах")
            Console.WriteLine("\n=== ОБЩИЙ РЕЗУЛЬТАТ ПО ДВУМ МАТРИЦАМ ===");
            Console.WriteLine($"Сумма чётных строк (A + B):     {(sumEvenRowsA + sumEvenRowsB):E6}");
            Console.WriteLine($"Сумма второстеп. диагонали (A + B): {(sumSecondaryDiagA + sumSecondaryDiagB):E6}");
            Console.WriteLine($"Объединённая сумма (A + B):     {(sumCombinedA + sumCombinedB):E6}");

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}