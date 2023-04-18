# Chapter 7 - Generics in C#



## What are generics
In C#, generics are used to create classes, methods, structs and other components that are
not specific, but general. This allows us to use the generic component for different reasons.

```C#
class Price<T>
    {
        T price;
        public Price(T price)
        {
            this.price = price;
        }
        public void Printtype()
        {
            Console.WriteLine("The type is " + typeof(T));
        }
        public T GetPrice()
        {
            return price;          
        }
    }
   
    class Program
    {
        static void Main(string[] args)
        {
            Price<int> price = new Price<int>(55);
            price.Printtype();
            int a = price.GetPrice();
            Console.WriteLine("The price is " + a);
            Console.ReadKey();
        }
    }
```

If you are totally new to the syntax of generics, you might be surprised to see the angle
brackets, <>, next to the Price class. You also might be wondering what the T inside <> is.
This is the syntax of generics in C#. By putting <> next to the class name, we are telling the
compiler that this is a generic class.

## Different constraints of generics

**1. Base class constraints**

The idea of this constraint is that only the classes that extend a base class can be used as 
generic type. For example, if you have a class named Person and you use this Person class
as a base for the Generic constraint, only the Person class or any other class that inherits
the Person class can be used as the type argument for that generic class.

```C#
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

            /* Not allowed
            Human<Toy> toyHumanType = new Human<Toy>(toy);
            toyHumanType.MustPrint();
            */

            Console.ReadKey();
        }
    }
```

**2. Interface constraints**

Similar to the Base class constraint, we see the interface constraint when your generic class
constraint is set as an Interface. Only those classes can be used in the generic method that
implements that interface.

**3. Reference type and value type constraints**
When you want to differentiate between your generic class and reference types and value
types, you need to use this constraint. When you use a Reference type constraint, the
generic class will only accept the Reference type objects. To achieve that, you have to
extend your generic class with a class keyword:

``` C#
...where T : class

... where T : struct
```

As we know, class is a reference type and struct is a value type. So, when you make a
value type constraint, this means that the generic will only work for value types such as
int or double. No reference type, such as string or any other custom class, will work