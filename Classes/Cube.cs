using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public class Cube : Box
    {
        readonly int id;
        readonly string description;
        readonly double weight;
        readonly int side;
        readonly bool fragile;

        public Cube(int id, string description, double weight, int side, bool fragile)
        {

            ID = id;
            Description = description;
            Side = side;
            Weight = weight;
            IsFragile = true;
            IsFragile = fragile;
            InsuranceValue = 1;

            Area = (side * side);
            Volume = (side * side * side);
            Maxdimension = Convert.ToInt32((Math.Round(Math.Sqrt((side * side) + (side * side))))); //räknar ut hypotennusan och avrundar till heltal
        }

        public int Side { get; set; }
    }
}
