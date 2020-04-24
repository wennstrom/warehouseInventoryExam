using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public class Sphere : Box
    {
        readonly int id;
        readonly string description;
        readonly double weight;
        readonly int side;
        readonly int radius;
        readonly bool fragile;
        public Sphere(int id, string description, double weight, int radius, bool fragile)
        {
            ID = id;
            Description = description;
            Weight = weight;
            Radius = radius;
            Maxdimension = radius + radius;
            IsFragile = fragile;

            Area = Convert.ToInt32(Math.Round(4 * Math.PI * (radius * radius)));    //räknar ut arean och konventerar double till int
            Volume = Convert.ToInt32(Math.Round((4 / 3) * Math.PI * (radius * radius * radius)));

        }
        public int Radius { get; set; }
    }
}
