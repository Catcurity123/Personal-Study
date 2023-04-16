using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class Program
    {
        static void Main(string[] args)
        {
            var Person = TupleTest.GetPerson();
            Console.WriteLine($"The person is {Person.Item1}. He is {Person.Item2}. He is a {Person.Item3}");
            Console.ReadLine();
        }

    }

    public class TupleTest
    {
        public static Tuple<string, int, string> GetPerson()
        {
            var Person = new Tuple<string, int, string>("Martin Dew", 42, "Soteware Developer");
            return Person;
        }
    }


}