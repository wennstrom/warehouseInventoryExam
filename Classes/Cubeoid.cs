using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public class Cubeoid : Box
    {
        readonly int id;
        readonly string description;
        readonly double weight;
        readonly int xside;
        readonly int yside;
        readonly int zside;
        readonly bool fragile;

        public Cubeoid(int id, string description, double weight, int xSide, int ySide, int zSide, bool fragile)
        {
            ID = this.id;
            Description = this.description;
            Weight = this.weight;
            xSide = this.xside;
            ySide = this.yside;
            zSide = this.zside;
            IsFragile = this.fragile;

            Area = 2 * ((xSide * ySide) + (xSide * zSide) + (ySide + zSide));
            Volume = xSide * ySide * zSide;

        }
        public int xSide { get; set; }
        public int ySide { get; set; }
        public int zSide { get; set; }
    }
}
