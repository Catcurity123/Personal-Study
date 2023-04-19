using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Runtime.Remoting.Services;
using System.Threading;
using System.Security.Cryptography;
using System.Reflection;

namespace TestProject
{
    class Program {

        static void Main(string[] args)
        {
            //Get user input for length and width
            Console.Write("Enter the width and length of the terrain: ");
            string input = Console.ReadLine();
            string[] values = input.Split(' ');
            int row = int.Parse(values[0]);
            int col = int.Parse(values[1]);


            //Prompt
            Console.WriteLine("Populating the Terrain");
            Thread.Sleep(1000);

            //Randomly populate the map
            TerrainEnum[,] map = new TerrainEnum[row, col];
            for (int i = 0; i < row * col; i++)
            {
                TerrainEnum random = new TerrainEnum();
                int RowIndex = i / col;
                int ColIndex = i % col;
                map[RowIndex, ColIndex] = random.GetRandomTerrain();
            }

            Console.OutputEncoding = UTF8Encoding.UTF8;
            
            for(int i = 0; i < (map.GetLength(0) * map.GetLength(1)); i++)
            {
                //Initialize variable
                int RowIndex = i / map.GetLength(1);
                int ColIndex = i % map.GetLength(1);
                TerrainEnum position = map[RowIndex, ColIndex];
                bool checkEndOfLine = (i + 1) % map.GetLength(1) == 0;

                //Add char and color
                Console.ForegroundColor = position.GetColor();
                Console.Write(position.GetChar() + (checkEndOfLine ? "\n" : " "));
            }
            
        }
    }
}