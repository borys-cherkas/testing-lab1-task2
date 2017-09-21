using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class Program
    {
        /*
         * 
Індивідуальне завдання 2
Скласти вандалостійку програму на будь-якій мові програмування.
Програма приймає на вхід три числа: перше число — це довжина сторони трикутника, друге та третє — величини прилеглих кутів у градусах. 
Програма повинна повідомляти, чи є ця фігура трикутником і, якщо так, вказувати на його тип: неправильний, рівнобедрений, рівносторонній, прямокутний.
Додаткові вимоги:
1. Консольний додаток.
2. Програма може приймати необхідні значення через параметри командного рядка.
3. Якщо через командний рядок параметри не передано, то програма запитує їх у користувача через стандартний ввід.
4. При запуску програма видає користувачеві:
10
1) Данні про автора: ім’я, групу.
2) Текст завдання: три числа.
3) Якщо значення передаються як параметри командного рядку — то вони розділюються комою. Якщо параметри опущені, то програма повинна запитати у користувача введення даних.
         */
        private static int length, alpha, beta;

        static void Main(string[] args)
        {
            PrintGreeting();

            bool parseRes = ParseDataFromArgs(args);
            if (!parseRes)
            {
                AskUserForData();
            }

            PrintCollectedData();

            CalculateAndPrintResults();
            Console.ReadLine();
        }

        private static void PrintGreeting()
        {
            Console.WriteLine("------- Автор: Борис Черкас, группа: ПИ-15-1 -----------");
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("-------------- Індивідуальне завдання 2 ----------------");
            Console.WriteLine("Скласти вандалостійку програму на будь-якій мові програмування.\n" +
                              "Програма приймає на вхід три числа: перше число — це довжина сторони трикутника, \n" +
                              "друге та третє — величини прилеглих кутів у градусах. Програма повинна повідомляти, \n" +
                              "чи є ця фігура трикутником і, якщо так, вказувати на його тип: неправильний, рівнобедрений, рівносторонній, прямокутний.");
            Console.WriteLine("--------------------------------------------------------");
        }

        private static bool ParseDataFromArgs(string[] args)
        {
            if (args.Length == 0)
                return false;

            string[] splittedArr = args[0].Split(',');
            string errorMessage = " ---- Invalid data in arguments :) ---- ";
            if (splittedArr.Length != 3)
            {
                Console.WriteLine(errorMessage);
                return false;
            }

            string sideLengthString = splittedArr[0];
            errorMessage = " ---- Invalid triangle side length in arguments :) ---- ";
            bool parseRes = ParseInt(sideLengthString, out length, errorMessage);
            if (!parseRes)
                return false;
            if (length < 0)
            {
                errorMessage = " ---- Invalid triangle side length in arguments. Length must be not less than 0. :) ---- ";
                Console.WriteLine(errorMessage);
                return false;
            }

            string alphaString = splittedArr[1];
            errorMessage = " ---- Invalid triangle angle 1 in arguments :) ---- ";
            parseRes = ParseInt(alphaString, out alpha, errorMessage);
            if (!parseRes)
                return false;
            if (alpha < 0)
            {
                errorMessage = " ---- Invalid triangle angle 1 in arguments. Angle must be not less than 0 and not more than 180. :) ---- ";
                Console.WriteLine(errorMessage);
                return false;
            }

            string betaString = splittedArr[2];
            errorMessage = " ---- Invalid triangle angle 2 in arguments :) ---- ";
            parseRes = ParseInt(betaString, out beta, errorMessage);
            if (!parseRes)
                return false;
            if (beta < 0)
            {
                errorMessage = " ---- Invalid triangle angle 2 in arguments. Angle must be not less than 0 and not more than 180. :) ---- ";
                Console.WriteLine(errorMessage);
                return false;
            }

            return true;
        }

        private static bool ParseInt(string strToParse, out int integer, string errorMessage)
        {
            bool parseRes = Int32.TryParse(strToParse, out integer);

            if (!parseRes)
            {
                Console.WriteLine(errorMessage);
                integer = 0;
                return false;
            }

            return true;
        }

        private static void AskUserForData()
        {
            bool res;
            do
            {
                Console.WriteLine("Enter triangle side length: ");
                string inputStr = Console.ReadLine();
                res = ParseInt(inputStr, out length, " ---- Invalid triangle side length. Try again :) ----- ");

                if (length < 0)
                {
                    Console.WriteLine(" ---- Invalid triangle side length in arguments. Length must be not less than 0. :) ---- ");
                    res = false;
                }
            } while (!res);

            do
            {
                Console.WriteLine("Enter triangle angle 1: ");
                string inputStr = Console.ReadLine();
                res = ParseInt(inputStr, out alpha, " ---- Invalid triangle angle. Try again :) ----- ");
            } while (!res);

            do
            {
                Console.WriteLine("Enter triangle angle 2: ");
                string inputStr = Console.ReadLine();
                res = ParseInt(inputStr, out beta, " ---- Invalid triangle angle. Try again :) ----- ");
            } while (!res);
        }

        private static void PrintCollectedData()
        {
            Console.WriteLine(" ---------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Итак, у нас есть следующие данные: ");
            Console.WriteLine($"Длина стороны треугольника: {length}");
            Console.WriteLine($"Прилегающий угол 1: {alpha}");
            Console.WriteLine($"Прилегающий угол 2: {beta}");
            Console.WriteLine();
            Console.WriteLine(" ---------------------------------------------");
        }

        private static void CalculateAndPrintResults()
        {
            if (alpha >= 360 || alpha <= -360)
            {
                alpha = alpha % 360;
            }

            if (beta >= 360 || beta <= -360)
            {
                beta = beta % 360;
            }

            if (alpha < 0)
            {
                alpha = 360 + alpha;
            }

            if (beta < 0)
            {
                beta = 360 + beta;
            }

            if (length != 0 && (alpha > 180 && beta < 180) || (alpha < 180 && beta > 180))
            {
                Console.WriteLine("Фигура не является треугольником! :(");
                return;
            }

            if (alpha > 180 && beta > 180)
            {
                alpha = 360 - alpha;
                beta = 360 - beta;
            }

            int angleSum = beta + alpha;
            if (angleSum == 0 || angleSum >= 180)
            {
                Console.WriteLine("Фигура не является треугольником! :(");
                return;
            }

            Console.WriteLine("Фигура является треугольником! :)");

            if (beta == alpha)
            {
                Console.WriteLine("Кроме того, треугольником равнобедренным! :)");
            }

            if (beta == 60 && alpha == 60)
            {
                Console.WriteLine("Кроме того, треугольником равносторонним! :)");
            }

            if (beta == 90 || alpha == 90 || (beta == 45 && alpha == 45))
            {
                Console.WriteLine("Кроме того, треугольником прямоугольным! :)");
            }

            if (beta > 90 || alpha > 90)
            {
                Console.WriteLine("Кроме того, треугольником неправильным! :)");
            }
        }
    }
}
