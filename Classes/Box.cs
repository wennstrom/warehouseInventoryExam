using System;

namespace Classes
{
    public class Box : I3DStorageObject
    {

        public int ID { get; set; }
        public string Description { get; set; }
        public double Weight { get; set; }
        public bool IsFragile { get; set; }
        public double Volume { get; set; }
        public double Area { get; set; }
        public double Maxdimension { get; set; }
        public int InsuranceValue { get; set; } = 1;


    }
}
