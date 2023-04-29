# Lists

## Array Lists

The first data structure that can automatically increase its size is **array list**, represented by the ***ArrayList*** class from ***System.Collections***
. We can use this class to store big collections of data, to which we can easily add new elements when necessary.

```C#
ArrayList arrayList = new ArrayList();
arrayList.Add(5);
arrayList.AddRange(new int[] { 6, -7, 8 });
arrayList.AddRange(new object[] { "Marcin", "Mary" });
arrayList.Insert(5, 7.8);
foreach (object element in arrayList)
{
     Console.WriteLine(element);
}

//It is important to remember that element in ArrayList are objects.
```

Element in ArrayList can be cast, access, and use via:  

```C#
object first = arrayList[0];
int third = (int)arrayList[2];
```

**ArrayList** has two important properties **Count** and **Capacity**

```C#
int count = arrayList.Count;
int capacity = arrayList.Capacity;
Console.WriteLine(count);
Console.WriteLine(capacity);
```

**ArrayList** has default capacity of 4, it will automatically increase by creating an internal array and copy the current value into it. Count, on the other hand, is the current number of element in the ArrayList.

There are also 3 other method that are useful for dealing with ArrayList. **Contains**, **IndexOf**, and **Remove**.

```C#
bool containsMary = arrayList.Contains("Mary");
int minusIndex = arrayList.IndexOf(-7);
arrayList.Remove(5);

Console.WriteLine(containsMary);
Console.WriteLine(minusIndex);
foreach (object element in arrayList)
{
    Console.WriteLine(element);
}
```
Output are as follows:
```
True
2
6
-7
8
Marcin
7.8
Mary
```