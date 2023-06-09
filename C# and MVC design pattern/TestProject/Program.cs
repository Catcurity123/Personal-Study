﻿using System;
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
using System.Collections.Concurrent;
using System.Collections;

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

        public static void Sort<T>(T[] Array) where T : IComparable
        {
            //iterating through the whole array
            for (int i = 1; i < Array.Length; i++)
            {
                //Comparing each element with the element before it, if the element is smaller then swap its position
                //Do that to the whole length of the current array
                int j = i;
                while (j > 0 && Array[j].CompareTo(Array[j - 1]) < 0)
                {
                    Swap(Array, j, j - 1);
                    j--;
                }
            }
        }
    }

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




















































    class Program
    {
        static void Main(string[] args)
        {
            ArrayList arrayList = new ArrayList();
            arrayList.Add(5);
            arrayList.AddRange(new int[] { 6, -7, 8 });
            arrayList.AddRange(new object[] { "Marcin", "Mary" });
            arrayList.Insert(5, 7.8);


            bool containsMary = arrayList.Contains("Mary");
            int minusIndex = arrayList.IndexOf(-7);
            arrayList.Remove(5);

            Console.WriteLine(containsMary);
            Console.WriteLine(minusIndex);
            foreach (object element in arrayList)
            {
                Console.WriteLine(element);
            }
        }
    }

}