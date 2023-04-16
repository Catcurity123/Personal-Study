# Chapter 1 - OOP Classes and Objects
## Description

Object-oriented programming (OOP) is something special. If you search the internet for
books on OOP, you'll find hundreds of books on this topic. But still this topic will never
become stale as it is the most efficient and most commonly used programming
methodology in the industry
## 1. Classes in OOP

**A class is like a template or blueprint** that tells us what properties and behaviors an
instance of this class will have. In most circumstances, a class itself can't actually do
anything—it is just used to create objects.

Suppose we have a `human` class, a class can have `properties` and `behavioral properties`:

+ **Properties**: Height, Weight, Age,...

+ **Behavioral Properties**: Walk, Talk, Eat,...

**1.1 The general form of a class**

``` C#
class class-name{
    //this is a class body
}
```

A simple class can look like this.

``` C#
class Customer{
    public string firstName;
    public string lastName;
    public string phoneNumber;
    public string emailAddress;
    public string GetFullName(){
        return firstName + " " + lastName;
    }
}
```

## 2. Objects in OOP

**An object is an instance of a class**. In other words, an object is an implementation of a class.
For example, in our banking application, we have a `Customer` class, but that doesn't mean
that we actually have a customer in our application. To create a customer, we have to create
an object of the `Customer` class.

**2.1 How to create objects**

``` C#
Customer customer1 = new Customer();
```

## 3. Variables in C#

**A variable is
something that varies, which means it is not constant**. In programming, when we create a
variable, the computer actually allocates a space in memory for it so that a value of the
variable can be stored there.

## 4. Methods in a class

Let's talk about another important topic—namely methods. **A method is a piece of code
that is written in the code file and can be reused**. A method can hold many lines of code,
which will be executed when it is called. Let's take a look at the general form of a method:

``` C#
access-modifier return-type method-name(parameter-list) {
   // method body
}
```

We can see that the first thing in the method declaration is an access-modifier. This will
set the access permission of the method. Then, we have the return-type of the method,
which will hold the type that the method will return, such as string, int, double, or
another type. After that, we have the method-name and then brackets, (), which indicate
that it is a method. In the brackets, we have the parameter-list. This can either be empty
or can contain one or more parameters

**4.1 Creating a method**

``` C#
public string GetFullName(string firstName, string lastName){
  return firstName + lastName;
}
```

## 5. Constructor of a class
**A constructor is a method that gets triggered when an object of a class is created. A
constructor is mainly used to set the prerequisites of the class**. For example, if you are
creating an object of the Human class, that human object must have a date of birth.
Without a date of birth, no human would exist. You can set this requirement in the
constructor. You can also configure the constructor to set the date of birth as today if
no date of birth is given.

```C#
access-modifier class-name(parameter-list){
    // constructor body
}
```

**Here, we can see that there is a difference between a constructor and a normal method,
namely that a constructor doesn't have a return type**. This is because a constructor can't
return anything; it's for initialization, not for any other type of action.

```C#
class BankAccount(){
    public string bankOwner;
    public BankAccount (string theOwner){
        bankOwner = theOwner;
    }
}
```

**Another interesting thing is that you can have multiple constructors in a class. You might
have one constructor that takes one argument and another that doesn't take any arguments**.
Depending on the way in which you are initializing the object, the respective constructor
will be called.

**This can be helpful if we want the object to have a default value if no input is specified**

``` C#
public string BankAccount(){
    public string bankOwner;

    public BankAccount(){
        bankOwner = "Some people";      //Default value
    }

    public BankAccount(string theOwner){
        bankOwner = theOwner;       //Specified Value
    }
}
```

## 6. The Four Pillars of OOP


### **6.1 Inheritance**
In programming, when one class is derived from another class,
this is called inheritance. **This means that the derived class will have the same properties as
the parent class**. In programming terminology, the class from which another class is
derived is called the parent class, while the classes that inherit from these are called child
classes.

```C#
public class Fruit{
    public string Name{get; set;}
    public string Color{get; set;}
}

public class Apple : Fruit{
    public int NumberOfSeeds {get; set;}
}
```

In the preceding example, we used inheritance. We have a parent class, called Fruit. This
class holds the common properties that every fruit has: a Name and a Color. We can use
this Fruit class for all fruits.

### **6.2 Encapsulation**
Encapsulation means hiding or covering. In C#, encapsulation is achieved by access
modifiers. The access modifiers that are available in C# are the following:

**- Public**: A public member is accessible from any code in the same program, including code in other classes and assemblies. This is the most permissive access level.

**- Private**: A private member is only accessible from within the same class. This mean that other code in the same program, including code in other classes, cannot access the member.

**- Protected**: A protected member is accessible from within the same class and any derived classes (subclasses), but not from other code in the same program.

**- Internal**: An internal member is accessible from any code in the same assembly, but not from code in other assembly.

**- Internal Protected**: A protected internal member is accessible from within the same assembly and any derived classes (subclasses) in other assembly.

**Encapsulation is when you want to control other classes' access to a certain class**. Let's say
you have a BankAccount class. For security reasons, it isn't a good idea to make that class
accessible to all classes. It's better to make it Private or use another kind of access
specifier

```C#
public class BankAccount{
 private double AccountBalance { get; set; }

 private double TaxRate { get; set; }

 public double GetAccountBalance() {
    double balanceAfterTax = GetBalanceAfterTax();
    return balanceAfterTax;
 }
 
 private double GetBalanceAfterTax(){
    return AccountBalance * TaxRate;
 }
}
```

### **6.3 Abstraction**
 In C#, we have `abstract` classes, which implement the concept
of abstraction. Abstract classes are classes that don't have any instances, classes that
implement the abstract class will implement the properties and methods of that
abstract class.

```C#
public abstract class Vehicle(){
    public abstract int GetNumberOfTyres();
}

public class Bicycle : Vehicle{
    public string Company {get; set;}
    public string Model {get; set;}
    public int NumberOfTyres {get; set;}

    public override int GetNumberOfTyres(){
        return NumberOfTyres;
    }
}

public class Car : Vehicle{
    public string Company {get; set;}
    public string Model {get; set;}
    public int FrontTyres {get; set;}
    public int BackTyres {get; set;}

    public override int GetNumberOfTyres(){
        return FrontTyres + BackTyres;
    }
}
```


### **6.4 Polymorphism**
In C#, there are two kind of polymorphism: **static polymorphism** and **dynamic
polymorphism**. **Static polymorphism** is a kind of polymorphism where the role of a
method is determined at compilation time, whereas, in **dynamic polymorphism**, the role of
a method is determined at runtime. Examples of static polymorphism include **method
overloading** and **operator overloading**. Let's take a look at an example of **method
overloading**:

```C#
public class Calculator{
    public int AddNumber(int FirstNumber, int SecondNumber){
        return FirstNumber + SecondNumber;
    }

    public double AddNumber(double FirstNumber, double SecondNumber){
        return FirstNumber + SecondNumber;
    }
}
```

Here, we can see that we have two methods with the same name, AddNumbers. Normally,
we can't have two methods that have the same name; however, as the parameters of those
methods are different, methods are allowed to have the same name by the compiler.
**Writing a method with the same name as another method, but with different parameters, is
called method overloading**. This is a kind of polymorphism.

Like method overloading, operator overloading s also a static polymorphism.

```C#
public class MyCalc
{
 public int a;
 public int b;
 public MyCalc(int a, int b)
 {
 this.a = a;
 this.b = b;
 }
 public static MyCalc operator +(MyCalc a, MyCalc b)
 {
 return new MyCalc(a.a * 3 ,b.b * 3);
 }
}
```

**Dynamic polymorphism** refers to the use of the abstract class. When you write an abstract
class, no instance can be created from that abstract class. When any other class uses or
implements that abstract class, **the class also has to implement the abstract methods of that
abstract class**. As different classes can implement the abstract class and can have different
implementations of abstract methods, polymorphic behavior is achieved. In this case, we
have **methods with the same name but different implementations**.





