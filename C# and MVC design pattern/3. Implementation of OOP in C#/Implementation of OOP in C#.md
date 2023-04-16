# Chapter 2 - Implementation of OOP in C#
## Description

In
Chapter 2, Hello OOP - Classes and Objects, we learned that abstraction, inheritance,
encapsulation, and polymorphism are the four basic principles of OOP, but we haven't yet
learned how the C# language can be used to fulfill these principles. 
## 1. Interfaces
A class is a blueprint, which means it contains the members and methods that the
instantiated objects will have. An interface can also be categorized as a blueprint, but
unlike a class, an interface doesn't have any method implementation. Interfaces are more
like a guideline for classes that implement the interface.

The main features of interfaces in C# are as follows:

**1. Interfaces cant have a method body; they can only have the method signature**

**2. Interfaces can have methods, properties, events, and indexes.**

**3. An interface can't be instantiated, so no object of an interface can be created.**

**4. One class can extend multiple interfaces.**

``` C#
interface IBankAccount{
    void Debit(double amount);
    void Credit(double amount);
}

class BankAccount : IBankAccount{
    public void Debit(double amount){
        Console.WriteLine($"${amount} has been debited from your amount!");
    }
    public void Credit(double amount){
        Console.WriteLine($"${amount} has been credited to your account!");
    }
}
```
In the preceding example, we can see that we have one interface, called
IBankAccount, that has two members: Debit and Credit. Both of these methods have no
implementations in the interface. In the interface, the method signatures are more like
guidelines or requirements for the classes that will implement this interface.

If any class
implements this interface, then the class has to implement the method body. This is a great
use of the OOP concept of inheritance. The class will have to give an implementation of the 
methods that are mentioned in the interface.

## 2. Abstract class

When a class
implements an abstract class, the class has to override the abstract methods of the abstract
class. One of the main characteristics of an abstract class is that it can't be instantiated. An
abstract class can only be used for inheritance. It might or might not have abstract methods
and assessors.

```C#
abstract class Animal{
    public string name;
    public int ageInMonth;
    public abstract void Move();
    public void Eat(){
        Console.WriteLine("Eating");
    }
}

class Dog : Animal{
    public override void Move(){
        Console.WriteLine("Moving");
    }
}
```

## 3. The partial class
You can split a class, a struct, or an interface into smaller portions that can be placed in
different code files. If you want to do this, you have to use the keyword partial. Even
though the code is in separate code files, when complied, they will be treated as one class
altogether.

The partial class has a few requirements, one of which is that all classes must have the
keyword partial in their signatures. All the partial classes also have to have the same
name, but the file names can be different. The partial classes also have to have the same
accessibility, such as public, private, and so on.


``` C#
// File name: Animal.cs 
using System;
namespace AnimalProject{
    public partial class Animal{
        public string name;
        public int ageInMonth;
        public void Eat(){
            Console.WriteLine("Eating");
        }
    }
}

//File name: AnimalMoving.cs
using System;
namespace AnimalProject{
    public partial class Animal{
        public void Move(){
            Console.WriteLine("Moving");
        }
    }
}
```

## 4. The Sealed class
One of the principles of OOP is inheritance, but sometimes you may need to restrict
inheritance in your code for the sake of your application's architecture. C# provides a
keyword called sealed. If this keyword is placed before a class's signature, the class is
considered a sealed class.

```C#
sealed class Animal{
    public string Name;
    public int ageInMonth;
    public void Move(){
        Console.WriteLine("Moving");
    }
}

public static void Main(){
    Animal dog = new Animal()
    dog.name = "Doggy";
    dog.ageInMonth = 4;
    dog.Move();
}


// However if we want to add another properties in to the class dog

class Dog : Animal{
    public char gender;
}

// The compiler will throw an error
```

## 5. Tuples
A tuple is a data structure that holds a set of data. Tuples are mainly helpful when you
want to group data and use it. Normally, a C# method can only return one value. By using
a tuple, it is possible to return multiple values from a method. The Tuple class is available
under the System.Tuple namespace. A tuple can be created using the Tuple<>
constructor or by an abstract method named Create that comes with the Tuple class.

```C#
var person = new Tuple<string, int, string>("Martin Dew", 42, "Software Developer"); //name, age, occupation
```

``` C#
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
```

## 6. Properties 
Underneath the properties are special methods that are called accessors. A
property contains two accessors: get and set. The get accessor gets values from the field
while the set accessor sets values to the field. There is a special keyword for a property,
named value. This represents the value of a field.

By using access modifiers, properties can have different access levels. A property can be
public, private, read only, open for read and write, and write only. If only the
set accessor is implemented, this means that the only write permission is given. If both
set and get accessors are implemented, this means that both read and write permissions
are open for that property.

```C#
class Animal{
    public string Name {get; set;}
    public int ageInMonth {get; set;}
}

