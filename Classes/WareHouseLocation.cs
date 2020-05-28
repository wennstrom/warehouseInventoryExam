using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace Classes
{
    public class WareHouseLocation
    {
        //private because of encapsulation
        private List<I3DStorageObject> _boxes = new List<I3DStorageObject>();
        public WareHouseLocation()
        {           
            MaxVolume = Height * Width * Debth;

        }
        public int Height { get; } = 250;
        public int Width { get; } = 250;
        public int Debth { get; } = 250;
        public double MaxWeight { get; } = 1000;
        public double MaxVolume { get; }

        internal void Add(I3DStorageObject b)
        {
            if (b is null)
                throw new ArgumentNullException("Can't add a box that is null.");
            else if (LocationContainsBox(b))
                throw new InvalidOperationException($"Box {b} is already added");
            else if (b.Weight + CalculateCurrentLocationWeight() > MaxWeight)
                throw new ArgumentOutOfRangeException("Box could not be added because the location would surpass it's max weight.");

            _boxes.Add(b);
        }
        internal void Remove(I3DStorageObject b)
        {

            if (!LocationContainsBox(b))
                throw new Exception("Could not find a box to remove.");

            _boxes.Remove(b);
        }

        public I3DStorageObject GetByID(int id)
        {
            foreach (var box in _boxes)
                if (box.ID == id)
                    return box;

            throw new Exception($"Could not found a box with ID {id} in this location.");
        }
        internal bool LocationContainsID(int id)
        {
            int count = _boxes.Count;
            if (count == 0)
                return false;

            foreach (var b in _boxes)
            {
                if (b.ID == id)
                {
                    return true;
                }
            }

            return false;
        }
        internal bool LocationContainsBox(I3DStorageObject b)
        {

            foreach (var box in _boxes)
                if (box == b)
                    return true;

            return false;
        }
        internal bool AddableLocation(I3DStorageObject b)
        {
            
            double newWeight = b.Weight + CalculateCurrentLocationWeight();
            double newVolume = b.Volume + CalculateCurrentLocationVolume();
            //if a box is fragile it needs to be stored alone
            if (b.IsFragile == true && _boxes.Count > 0)
                return false;
            if (newWeight > MaxWeight || MaxVolume < newVolume)
                return false;

            return true;
        }
        internal double CalculateCurrentLocationWeight()
        {
            double totalWeight = 0;

            foreach (var b in _boxes)
                totalWeight += b.Weight;

            return totalWeight;
        }
        internal double CalculateCurrentLocationVolume()
        {
            double totalVolume = 0;

            foreach (var b in _boxes)
                totalVolume += b.Volume;

            return totalVolume;
        }
        public string LocationOverview()
        {
            var result = new StringBuilder();
            result.AppendLine($"Location Box count: {_boxes.Count}");
            foreach (var b in _boxes)
            {
                result.Append($"ID {b.ID} ");
            }

            return result.ToString();
        }
    }
}
