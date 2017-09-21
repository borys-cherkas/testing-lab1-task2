﻿using System;
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

            bool parseSuccessfull = ParseDataFromArgs(args);
            if (!parseSuccessfull)
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
            string[] splittedArr = ValidateAndSplitArgs(args);
            if (splittedArr == null)
                return false;
            
            string sideLengthString = splittedArr[0];
            bool parseRes = TryParseLengthFromString(sideLengthString);
            if (!parseRes)
                return false;

            string alphaString = splittedArr[1];
            parseRes = TryParseAngleAlphaFromString(alphaString);
            if (!parseRes)
                return false;

            string betaString = splittedArr[2];
            parseRes = TryParseAngleBetaFromString(betaString);
            if (!parseRes)
                return false;

            return true;
        }

        private static string[] ValidateAndSplitArgs(string[] args)
        { 
            if (args.Length == 0)
                return null;

            string[] splittedArr = args[0].Split(',');
            string errorMessage = " ---- Invalid data in arguments :) ---- ";
            if (splittedArr.Length != 3)
            {
                Console.WriteLine(errorMessage);
                return null;
            }
            return splittedArr;
        }

        private static bool TryParseLengthFromString(string sideLengthString)
        {
            string errorMessage = " ---- Invalid triangle side length in arguments :) ---- ";
            bool parseRes = ParseInt(sideLengthString, out length, errorMessage);
            if (!parseRes)
                return false;
            if (length < 0)
            {
                errorMessage = " ---- Invalid triangle side length in arguments. Length must be not less than 0. :) ---- ";
                Console.WriteLine(errorMessage);
                return false;
            }
            return true;
        }

        private static bool TryParseAngleAlphaFromString(string alphaString)
        {
            string errorMessage = " ---- Invalid triangle angle 1 in arguments :) ---- ";
            bool parseRes = ParseInt(alphaString, out alpha, errorMessage);
            if (!parseRes)
                return false;

            return true;
        }

        private static bool TryParseAngleBetaFromString(string betaString)
        {
            string errorMessage = " ---- Invalid triangle angle 2 in arguments :) ---- ";
            bool parseRes = ParseInt(betaString, out beta, errorMessage);
            if (!parseRes)
                return false;

            return true;
        }

        private static void AskUserForData()
        {
            GetSideLengthFromUser();
            GetAngleAlphaFromUser();
            GetAngleBetaFromUser();
        }

        private static void GetSideLengthFromUser()
        {
            bool res;
            do
            {
                Console.WriteLine("Enter triangle side length: ");
                string inputStr = Console.ReadLine();
                res = TryParseLengthFromString(inputStr);
            } while (!res);
        }
        private static void GetAngleAlphaFromUser()
        {
            bool res;
            do
            {
                Console.WriteLine("Enter triangle angle 1: ");
                string inputStr = Console.ReadLine();
                res = TryParseAngleAlphaFromString(inputStr);
            } while (!res);
        }
        private static void GetAngleBetaFromUser()
        {
            bool res;
            do
            {
                Console.WriteLine("Enter triangle angle 2: ");
                string inputStr = Console.ReadLine();
                res = TryParseAngleBetaFromString(inputStr);
            } while (!res);
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
            TransformAnglesIfNeeded();

            bool res = CheckAndPrintIfTriangle();
            if (!res)
                return;

            PrintTriangleCharacteristics();
        }

        private static void TransformAnglesIfNeeded()
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
        }

        private static bool CheckAndPrintIfTriangle()
        {
            if (length != 0 && (alpha > 180 && beta < 180) || (alpha < 180 && beta > 180))
            {
                Console.WriteLine("Фигура не является треугольником! :(");
                return false;
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
                return false;
            }

            Console.WriteLine("Фигура является треугольником! :)");

            return true;
        }

        private static void PrintTriangleCharacteristics()
        {
            if (beta == alpha || (180 - beta == alpha) || (180 - alpha == beta))
            {
                Console.WriteLine("Кроме того, треугольником равнобедренным! :)");
            }

            if (beta == 90 || alpha == 90 || (beta == 45 && alpha == 45))
            {
                Console.WriteLine("Кроме того, треугольником прямоугольным! :)");
            }

            if (beta == 60 && alpha == 60)
            {
                Console.WriteLine("Кроме того, треугольником равносторонним! :)");
            }
            else
            //if (beta > 90 || alpha > 90 || (beta + alpha < 90))
            {
                Console.WriteLine("Кроме того, треугольником неправильным! :)");
            }
        }
    }
}
