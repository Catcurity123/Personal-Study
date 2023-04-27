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

            InsertionSort.Sort(UnsortedArr);
            Console.WriteLine(string.Join(" | ", UnsortedArr));
        }
    }

}