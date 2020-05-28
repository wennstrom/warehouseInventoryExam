using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    class Cubeoid : Box
    {   
        public Cubeoid(int id, string description, double weight, int xSide, int ySide, int zSide, bool isFragile)
            : base(id, description, isFragile, weight, CalculateVolume(xSide, ySide, zSide), CalulateArea(xSide, ySide, zSide), CalculateMaxDimension(xSide, ySide, zSide))
        {            
            XSide = xSide;
            YSide = ySide;
            ZSide = zSide;
        }
        public int XSide { get; set; }
        public int YSide { get; set; }
        public int ZSide { get; set; }
        private static double CalulateArea(int xSide, int ySide, int zSide)
        {
           return  2 * (xSide * ySide) + (xSide * zSide) + (ySide * zSide);
        }
        private static double CalculateVolume(int xSide, int ySide, int zSide)
        {
            return xSide * ySide * zSide;
        }
        private static double CalculateMaxDimension(int xSide, int ySide, int zSide)
        {
            return Math.Round(Math.Sqrt(xSide * xSide + ySide * ySide + zSide * zSide));
        }
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            string fragile = IsFragile.ToString();

            result.AppendLine($"ID: {ID}");
            result.AppendLine("Type: Cubeoid");
            result.AppendLine($"Volume: {Volume}");
            result.AppendLine($"Area: {Area}");
            result.AppendLine($"Maxdimension: {Maxdimension}");
            result.AppendLine($"Fragile: {fragile}");


            return result.ToString();
        }
    }
}
