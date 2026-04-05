using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LR_Two
{
    class Program
    {
        static void Main(string[] args)
        {
        

         Console.WriteLine("Лабораторная работа №2");
         Console.WriteLine("Выполнил: Брылько Даниил Игоревич>");
         Console.WriteLine("далее выполнено задание с функцией:");

         double apo, boo, css, ddd, ew, f, g;
    

         Console.WriteLine("Введите значение переменной apo: ");
         apo = Convert.ToDouble(Console.ReadLine());
         Console.WriteLine("Введите значение переменной boo: ");
         boo = Convert.ToDouble(Console.ReadLine());
         Console.WriteLine("Введите значение переменной css: ");
         css = Convert.ToDouble(Console.ReadLine());
         Console.WriteLine("Введите значение переменной ddd: ");
         ddd = Convert.ToDouble(Console.ReadLine());
         Console.WriteLine("Введите значение переменной ew: ");
         ew = Convert.ToDouble(Console.ReadLine());

         if ((apo < 0) || (apo > 100000) || (boo < 0) || (boo > 100000) || (css < 0) || (css > 100000) || (ddd < 0) || (ddd > 100000) ||(ew < 0) || (ew > 100000))
            Console.WriteLine("ERROR");
         else
            {
                f = ((1 / 100) - (1 / apo) - (1 / (boo * boo)));
                g = ((1 / (css * css)) + (Math.Sqrt(ew) / (ddd * ddd * ddd)));
                Console.WriteLine(String.Format("Занчение выражения f: {0:0.000}", f));
                Console.WriteLine(String.Format("Занчение выражения g: {0:0.000}", g));
            }

        Console.WriteLine("для завершения работы программы нажмите любую клавишу...");
        Console.ReadKey();
        }
    }
}