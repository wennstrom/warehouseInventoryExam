using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    class Sphere : Box
    {
        
        public Sphere(int id, string description, double weight, int radius, bool fragile)
            : base(id, description, fragile, weight, CalculateVolume(radius), CalculateArea(radius), CalculateMaxDimension(radius))
        {
            Radius = radius;
        }
        public int Radius { get; set; }
        private static double CalculateArea(int radius)
        {
            return Math.Round(4 * Math.PI * (radius * radius));
        }
        public static double CalculateVolume(int radius)
        {
            return Math.Round((4 / 3) * Math.PI * (radius * radius * radius));
        }
        public static double CalculateMaxDimension(int radius)
        {
            return radius + radius;
        }
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            string fragile = IsFragile.ToString();

            result.AppendLine($"ID: {ID}");
            result.AppendLine("Type: Sphere");
            result.AppendLine($"Volume: {Volume}");
            result.AppendLine($"Area: {Area}");
            result.AppendLine($"Maxdimension: {Maxdimension}");
            result.AppendLine($"Fragile: {fragile}");


            return result.ToString();
        }
    }
}
