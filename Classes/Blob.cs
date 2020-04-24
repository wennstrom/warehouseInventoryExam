using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public class Blob : Box
    {
        private readonly int ID;

        public Blob(int id, string description, double weight, int side)
        {
            ID = id;
            Description = description;
            Side = side;
            Area = (side * side);
            Weight = weight;
            IsFragile = true;

            Volume = (side * side * side);
            Maxdimension = Convert.ToInt32((Math.Round(Math.Sqrt((side * side) + (side * side))))); //räknar ut hypotennusan och avrundar till heltal

        }
        public int Side { get; }
    }
}
