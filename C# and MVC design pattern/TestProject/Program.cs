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
            Customer customer1 = new Customer();
            customer1.firstName = "Dang";
            customer1.lastName = "Vi Luan";
            customer1.phoneNumber = "03934123";
            customer1.emailAddress = "@";

            string fullname = customer1.GetFullName();
            Console.WriteLine("My Name is " + fullname);

            BankAccount bankAccount1 = new BankAccount(customer1.lastName);
            Console.WriteLine("The owner is "+ bankAccount1.bankOwner);

            MyCalc calc1 = new MyCalc(2, 3);
            MyCalc calc2 = new MyCalc(4, 5);
            MyCalc result = calc1 + calc2;
            Console.WriteLine(result.a + " " + result.b);
            Console.ReadLine();
        }

    }

    public class Customer
    {
        public string firstName;
        public string lastName;
        public string phoneNumber;
        public string emailAddress;

        public string GetFullName(){
            return firstName + " " + lastName;
        }
    }

    
    class BankAccount
    {
        public string bankOwner;
        public BankAccount(string theOwner)
        {
            bankOwner = theOwner;
        }

        public BankAccount()
        {
            bankOwner = "Some People";
        }
    }

    public class MyCalc
    {
        public int a;
        public int b;
        public MyCalc(int a , int b)
        {
            this.a = a;
            this.b = b;
            Console.WriteLine(this.a + " " + this.b);
        }

        public static MyCalc operator + (MyCalc a, MyCalc b)
        {
            return new MyCalc(a.a * 3, b.b * 3);
        }
    }
    
}
