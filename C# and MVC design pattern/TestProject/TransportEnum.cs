using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public enum TransportEnum
    {
        CAR,
        BUS,
        SUBWAY,
        BIKE,
        WALK
    }

    public static class TransportEnumExtensions
    {
        public static char GetChar(this TransportEnum transportEnum) 
        {
            switch (transportEnum)
            {
                case TransportEnum.CAR: return 'C';
                case TransportEnum.BUS: return 'B';
                case TransportEnum.SUBWAY: return 'S';
                case TransportEnum.BIKE: return 'B';
                case TransportEnum.WALK: return 'W';
                default: throw new Exception("Unknown Transport");
            }
        }

        public static ConsoleColor GetColor(this TransportEnum transportEnum)
        {
            switch (transportEnum)
            {
                case TransportEnum.CAR: return ConsoleColor.Blue;
                case TransportEnum.BUS: return ConsoleColor.DarkGreen;
                case TransportEnum.SUBWAY: return ConsoleColor.Red;
                case TransportEnum.BIKE: return ConsoleColor.DarkMagenta;
                case TransportEnum.WALK: return ConsoleColor.DarkYellow;
                default: throw new Exception("Unknown Transport");
            }
        }

    }
}
