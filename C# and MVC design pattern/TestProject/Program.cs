using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class Person
    {
        public void PrintName()
        {
            Console.WriteLine("My Name is Josh");
        }
    }

    public class Boy : Person
    {

    }

    public class Toy
    {

    }
    public class Human<T> where T : Person
    {
        T objec;
        public Human(T objec)
        {
            this.objec = objec;
        }

        public void MustPrint()
        {
            objec.PrintName();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person();
            Boy boy = new Boy();
            Toy toy = new Toy();

            Human<Person> personTypeHuman = new Human<Person>(person);
            personTypeHuman.MustPrint();

            Human<Boy> boyTypeHuman = new Human<Boy>(boy);
            boyTypeHuman.MustPrint();

            Console.ReadKey();
        }
    }
}