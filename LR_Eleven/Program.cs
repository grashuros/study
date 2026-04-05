using System;
using System.IO;
using System.Text;

namespace Lab11Variant12
{
    // Структура согласно твоему заданию
    struct Worker
    {
        public int ID;               // ID
        public string Name;          // Сотрудник
        public int Category;         // Категория товара (1-4)
        public double Salary;        // З/п рабочего
        public int Quantity;         // Количество произведенных товаров
        public double UnitPrice;     // Цена за единицу товара
    }

    class Program
    {
        static void Main(string[] args)
        {
            string filename = "production.dat";

            // 1. Создаем файл с тестовыми данными
            CreateFile(filename);

            // 2. Выводим содержимое файла на экран
            Console.WriteLine("Содержимое файла:");
            PrintFile(filename);

            // 3. Выполняем расчеты согласно варианту
            ProcessData(filename);

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        static void CreateFile(string path)
        {
            Worker[] workers = new Worker[]
            {
                new Worker { ID = 1, Name = "Иванов А.А.", Category = 1, Salary = 50000, Quantity = 100, UnitPrice = 600 }, 
                new Worker { ID = 2, Name = "Петров Б.Б.", Category = 2, Salary = 45000, Quantity = 40, UnitPrice = 1000 }, 
                new Worker { ID = 3, Name = "Сидоров В.В.", Category = 1, Salary = 30000, Quantity = 150, UnitPrice = 400 }, 
                new Worker { ID = 4, Name = "Кузнецов Г.Г.", Category = 3, Salary = 55000, Quantity = 50, UnitPrice = 1200 },
                new Worker { ID = 5, Name = "Смирнов Д.Д.", Category = 4, Salary = 40000, Quantity = 20, UnitPrice = 1500 }  
            };

            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create), Encoding.UTF8))
            {
                foreach (var w in workers)
                {
                    writer.Write(w.ID);
                    writer.Write(w.Name);
                    writer.Write(w.Category);
                    writer.Write(w.Salary);
                    writer.Write(w.Quantity);
                    writer.Write(w.UnitPrice);
                }
            }
        }

        static void PrintFile(string path)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open), Encoding.UTF8))
            {
                Console.WriteLine("{0,-3} | {1,-15} | {2,-5} | {3,-10} | {4,-5} | {5,-10}", 
                    "ID", "Сотрудник", "Кат.", "З/п", "Кол-во", "Цена/ед");
                Console.WriteLine(new string('-', 65));

                while (reader.PeekChar() > -1)
                {
                    int id = reader.ReadInt32();
                    string name = reader.ReadString();
                    int cat = reader.ReadInt32();
                    double sal = reader.ReadDouble();
                    int quant = reader.ReadInt32();
                    double price = reader.ReadDouble();

                    Console.WriteLine("{0,-3} | {1,-15} | {2,-5} | {3,-10:F2} | {4,-5} | {5,-10:F2}", 
                        id, name, cat, sal, quant, price);
                }
            }
        }

        static void ProcessData(string path)
        {
            int overpaidWorkers = 0;           
            double totalVolume = 0;             
            double[] catVolumes = new double[5]; 
            
            string bestWorker = "";           
            double maxEfficiency = double.MinValue;

            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open), Encoding.UTF8))
            {
                while (reader.PeekChar() > -1)
                {
                    int id = reader.ReadInt32();
                    string name = reader.ReadString();
                    int cat = reader.ReadInt32();
                    double sal = reader.ReadDouble();
                    int quant = reader.ReadInt32();
                    double price = reader.ReadDouble();

                    double productionValue = quant * price;

                    // 1. Рабочие, получающие больше, чем вырабатывают
                    if (sal > productionValue) overpaidWorkers++;

                    // 2. Суммарный объем всего
                    totalVolume += productionValue;

                    // 3. Объем по категориям
                    if (cat >= 1 && cat <= 4) catVolumes[cat] += productionValue;

                    // 4. Самый эффективный (разница прод. продукт - з/п)
                    double efficiency = productionValue - sal;
                    if (efficiency > maxEfficiency)
                    {
                        maxEfficiency = efficiency;
                        bestWorker = name;
                    }
                }
            }

            // Вывод результатов
            Console.WriteLine("\n--- РЕЗУЛЬТАТЫ ОБРАБОТКИ ---");
            Console.WriteLine($"1. Рабочих, чья з/п превышает выработку: {overpaidWorkers}");
            Console.WriteLine($"2. Общий объем произведенной продукции: {totalVolume:F2} руб.");
            
            Console.WriteLine("3. Объем по категориям:");
            for (int i = 1; i <= 4; i++)
            {
                Console.WriteLine($"   Категория {i}: {catVolumes[i]:F2} руб.");
            }

            Console.WriteLine($"4. Самый эффективный сотрудник: {bestWorker} (профит: {maxEfficiency:F2} руб.)");
        }
    }
}