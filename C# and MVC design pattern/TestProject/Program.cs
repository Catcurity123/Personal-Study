using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;


namespace TestProject
{


    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo cultureInfo = new CultureInfo("en-US");
            Console.Write("The table number: ");
            string table = Console.ReadLine();
            Console.Write("The number of people: ");
            string countString = Console.ReadLine();
            int.TryParse(countString, out int count);
            Console.Write("The reservation date (MM/dd/yyyy): ");
            string dateTimeString = Console.ReadLine();
            if (!DateTime.TryParseExact(
            dateTimeString,
            "M/d/yyyy HH:mm",
            cultureInfo,
            DateTimeStyles.None,
            out DateTime dateTime))
            {
                dateTime = DateTime.Now;
            }
            Console.WriteLine(
            "Table {0} has been booked for {1} people on {2}at { 3}",
table,
count,
dateTime.ToString("M/d/yyyy", cultureInfo),
dateTime.ToString("HH:mm", cultureInfo));

        }
    }
}