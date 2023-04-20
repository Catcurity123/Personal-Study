using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Runtime.Remoting.Services;
using System.Threading;
using System.Security.Cryptography;
using System.Reflection;
using System.Drawing.Text;

namespace TestProject
{
    class Program {

        private static string[] GetMonthName()
        {
            string[] names = new string[12];
            for (int month = 1; month <= 12; month++)
            {
                DateTime firstday = new DateTime(DateTime.Now.Year, month, 1);
                string name = firstday.ToString("MMMM", CultureInfo.CreateSpecificCulture("en"));
                names[month - 1] = name;
            }
            return names;
        }
        static void Main(string[] args)
        {
            Random rnd = new Random();
            //Get the number of transport in TransportEnum
            int transportTypesCount = Enum.GetNames(typeof(TransportEnum)).Length;              //Enum.GetNames(typeof(TransportEnum)) will return a string array of all the transport method in enum
            //Init a jagged array
            TransportEnum[][] transportEnums = new TransportEnum[12][];

            for(int month = 1; month <= 12; month++)
            {
                // -----------Populate the jagged array-----------
                //DaysInMonth returns the days of the current year according to index month
                int daysCount = DateTime.DaysInMonth(DateTime.Now.Year, month);
                //Create an array of TransportEnum with the daysCount value and store it into according transportEnums array
                transportEnums[month - 1] = new TransportEnum[daysCount];

                for(int day = 1; day <= daysCount; day++) 
                {
                    int randomType = rnd.Next(transportTypesCount);
                    //Store the random type of transport into each day
                    transportEnums[month - 1][day - 1] = (TransportEnum)randomType;
                }
            }

            //Get all the month names
            string[] monthNames = GetMonthName();

            //Find the maximum value in monthNames using n with n is the string length of each member in the array
            int monthNamesPart = monthNames.Max(n => n.Length) + 2;

            //Display the calender
            for (int month = 1; month < transportEnums.Length; month++)
            {
                Console.Write($"{monthNames[month - 1]}:".PadRight(monthNamesPart));
                for (int day = 1; day < transportEnums[month - 1].Length; day++)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = transportEnums[month - 1][day - 1].GetColor();
                    Console.Write(transportEnums[month - 1][day - 1].GetChar());
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(" ");
                }
                Console.WriteLine();
            }

        }
    }
}