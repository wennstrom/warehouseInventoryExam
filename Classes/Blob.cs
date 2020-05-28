using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    class Blob : Box
    {
        public Blob(int id, string description, double weight, int side)
            : base(id, description, true, weight, CalculateVolume(side), CalculateArea(side), CalculateMaxDimension(side))
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
            return Math.Round(Math.Sqrt((side * side) + (side * side))); //hypotenusan
        }
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            string fragile = IsFragile.ToString();

            result.AppendLine($"ID: {ID}");
            result.AppendLine("Type: Blob");
            result.AppendLine($"Volume: {Volume}");
            result.AppendLine($"Area: {Area}");
            result.AppendLine($"Maxdimension: {Maxdimension}");
            result.AppendLine($"Fragile: {fragile}");


            return result.ToString();
        }
    }
}
