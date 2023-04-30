# Sorting

## Selection Sort 

**Basically, what we are doing is** in the first loop, take the first index and its value and assume that it is the minimum value. After that in the second loop, we will find the actually minimum value in the array by comparing the first min value to the rest of the array. We will do this until we find the minimum value of the array. Then we will swap the minimum value for the value in the first loop.

```C#
namespace TestProject
{
    public static class SelectionSort
    {
        //THis declare a static generic method (Swap <T>) that takes T[] array, int firstInex
        //and int MinIndex as parameters
        public static void Swap<T>(T[] array, int firstIndex, int MinIndex)
        {
            //Store the value in tempt and swap it with the second that swap the tempt with the
            //second, effectively swaping the value of first and second
            T temp = array[firstIndex];
            array[firstIndex] = array[MinIndex];
            array[MinIndex] = temp;
        }

        //This declare a static generic method (Sort <T>) which takes (T[] Array) as parameter
        // And all the <T> will be inherited from IComparable which is a system-bulit-in interface
        public static void Sort <T>(T[] Array) where T : IComparable
        {
            //The first loop will find the initial min value and index
            for (int FirstLoopIndex = 0;  FirstLoopIndex < Array.Length; FirstLoopIndex++)
            {
                //index is int but the value in the array can be Generic
                int minIndex = FirstLoopIndex;
                T minValue = Array[FirstLoopIndex];

                //The second loop will find the min value and index in the array
                //by comparing each value with the value of the initial min value
                for(int SecondLoopIndex = FirstLoopIndex + 1; SecondLoopIndex < Array.Length; SecondLoopIndex++)
                {
                    if (Array[SecondLoopIndex].CompareTo(minValue) < 0)
                    {
                        minIndex = SecondLoopIndex;
                        minValue = Array[SecondLoopIndex];
                    }
                }
                //After that it will swap the value of the real min index and the initial
                Swap(Array, FirstLoopIndex, minIndex);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the values separated by spaces: ");
            string input = Console.ReadLine();
            string[] values = input.Split(' ');

            int[] UnsortedArr = new int[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                UnsortedArr[i] = int.Parse(values[i]);
            }

            SelectionSort.Sort(UnsortedArr);
            Console.WriteLine(string.Join(" | ", UnsortedArr));
        }
    }

}
```

## Insertion Sort

**Basically, what we are doing is**  iterating through the array and comparing each element with the elements before it. If the element is smaller than the previous element, it is moved to the correct position in the sorted part of the array.

```C#
    public static class InsertionSort
    {
        public static void Swap<T>(T[] array, int firstIndex, int MinIndex)
        {
            //Store the value in tempt and swap it with the second that swap the tempt with the
            //second, effectively swaping the value of first and second
            T temp = array[firstIndex];
            array[firstIndex] = array[MinIndex];
            array[MinIndex] = temp;
        }
        public static void Sort <T>(T[] Array) where T : IComparable
        {
            for(int i = 1; i < Array.Length; i++)
            {
                int j  = i;
                while(j > 0 && Array[j].CompareTo(Array[j - 1]) < 0)
                {
                    Swap(Array, j, j - 1);
                    j--;
                }
            }
        }
    }
```

## Bubble Sort

**Basically, what we are doing is** repeatedly stepping through the list, comparing adjacent elements and swapping them if they are in the wrong order. The pass through the list is repeated until the list is sorted.


```C#
    public static class BubbleSort
    {
        public static void Swap<T>(T[] array, int LargerIndex, int SmallerIndex) where T : IComparable
        {
            T temp = array[LargerIndex];
            array[LargerIndex] = array[SmallerIndex];
            array[SmallerIndex] = temp;
        }

        public static void Sort <T>(T[] array) where T : IComparable
        {
            for(int ArrayLoopIndex = 0; ArrayLoopIndex <  array.Length; ArrayLoopIndex++)
            {
                bool SwapHappened = false;
                for(int SwapLoopIndex = 0; SwapLoopIndex < array.Length - 1; SwapLoopIndex++)
                {
                    if (array[SwapLoopIndex].CompareTo(array[SwapLoopIndex + 1]) > 0)
                    {
                        Swap(array, SwapLoopIndex, SwapLoopIndex + 1);
                        SwapHappened = true;
                    }
                }
                if (SwapHappened == false)
                {
                    break;
                }
            }
        }
    }

```

## Quick Sort


```C#
    public static class QuickSort
    {
        public static void Swap<T>(T[] array, int i, int j) where T : IComparable
        {
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        private static int MedianOfThree<T>(T[] array, int left, int right) where T : IComparable
        {
            int mid = (left + right) / 2;
            if (array[left].CompareTo(array[mid]) > 0)
                Swap(array, left, mid);
            if (array[left].CompareTo(array[right]) > 0)
                Swap(array, left, right);
            if (array[mid].CompareTo(array[right]) > 0)
                Swap(array, mid, right);
            return mid;
        }

        private static int Partition<T>(T[] array, int left, int right) where T : IComparable
        {
            int pivotIndex = MedianOfThree(array, left, right);
            T pivot = array[pivotIndex];
            Swap(array, pivotIndex, right);
            int i = left;
            for (int j = left; j < right; j++)
            {
                if (array[j].CompareTo(pivot) <= 0)
                {
                    Swap(array, i, j);
                    i++;
                }
            }
            Swap(array, i, right);
            return i;
        }

        public static void Sort<T>(T[] array) where T : IComparable
        {
            Sort(array, 0, array.Length - 1);
        }

        private static void Sort<T>(T[] array, int left, int right) where T : IComparable
        {
            if (left < right)
            {
                int p = Partition(array, left, right);
                Sort(array, left, p - 1);
                Sort(array, p + 1, right);
            }
        }
    }
```

