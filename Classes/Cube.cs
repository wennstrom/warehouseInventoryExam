using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;

namespace Classes
{
    class Cube : Box
    {
        public Cube(int id, string description, bool fragile, double weight, int side)
            : base(id, description, fragile, weight, CalculateVolume(side), CalculateArea(side), CalculateMaxDimension(side))
        {
            Side = side;

        }
        public int Side { get; }
        private static double CalculateArea(int side)
        {
            return side * side;
        }
        private static double CalculateVolume(int side)
        {
           return side * side * side;
        }
        private static double CalculateMaxDimension(int side)
        {
            return (int)Math.Round(Math.Sqrt((side * side) + (side * side))); //räknar ut hypotennusan och avrundar till heltal
        }
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            string fragile = IsFragile.ToString();

            result.AppendLine($"ID: {ID}");
            result.AppendLine("Type: Cube");
            result.AppendLine($"Volume: {Volume}");
            result.AppendLine($"Area: {Area}");
            result.AppendLine($"Maxdimension: {Maxdimension}");
            result.AppendLine($"Fragile: {fragile}");
            

            return result.ToString();
        }
    }
}
