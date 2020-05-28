using System;

namespace Classes
{
    class Box : I3DStorageObject
    { 
        public Box (int id, string description, bool isFragile, 
            double weight, double volume, double area, double maxDimension)
        {
            ID = id;
            Description = description;
            IsFragile = isFragile;
            Weight = weight;
            Volume = volume;
            Area = area;
            Maxdimension = maxDimension;
        }

        public int ID { get; }
        public string Description { get; }      
        public bool IsFragile { get; }
        public double Weight { get; }
        public double Volume { get; }
        public double Area { get; }
        public double Maxdimension { get; }
        public int InsuranceValue { get; } = 1;   
    }
}
