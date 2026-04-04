using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LR_One
{
    class Program
    {
        static void Main(string[] args)
        {
         Console.WriteLine("Лабораторная работа №1");
         Console.WriteLine(" Дата рождения студента: 12.03.2003");
         Console.WriteLine("Выполнил: Брылько Даниил Игоревич>");
         Console.WriteLine("Группа: ИДПО-ИСИТ-З-У-24/1");
         Console.WriteLine("Наименование ЛР: РАЗРАБОТКА КОНСОЛЬНОГО ПРИЛОЖЕНИЯ");
         Console.WriteLine("Населенный пункт постоянного места жительства студента: Ставрополь");
         Console.WriteLine("Любимый предмет в школе: управление дронами");
         Console.WriteLine("Краткое описание увлечений, хобби, интересов: спорт, киберспорт");
         Console.WriteLine("далее выполнено задание с функцией:");

         int g = 30;
         int h = 15;
         int d = 10;

         float ee = (((g * h) / (d * 17)) + (d * 17)) / h - ( (g + (d * 17) +h) / 4 );
         Console.WriteLine("Значение функции ее: {0}", ee);



        Console.WriteLine("для завершения работы программы нажмите любую клавишу...");
        Console.ReadKey();
        }
    }
}