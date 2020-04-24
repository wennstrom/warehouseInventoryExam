using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public class WareHouseLocation
    {
        public readonly List<Box> Boxes = new List<Box>();
        public List<Box> boxes
        {
            get { return Boxes; }
        }
        
        public WareHouseLocation()
        {

            MaxVolume = Height * Width * Debth;
        }
        public int Height { get;  } = 250;
        public int Width { get;  } = 250;
        public int Debth { get;  } = 250;
        public double MaxWeight { get;  } = 1000;
        public double MaxVolume { get; }
        public WareHouseLocation Content()
        {
            return (WareHouseLocation)this.MemberwiseClone();
        }
        public Box GetBox(int id)
        {
            foreach (var box in Boxes)
            {
                if (box.ID == id)
                    return box;
            }

            return null;
        }
        public void AddBox(Box b)
        {
            if (Addable(b))
            {
                boxes.Add(b);
            }
            else
            {
                Console.WriteLine("The box could not be ");
            }


            
        }
        
        public bool Addable(Box b)
        {
            double newWeight = CalculateCurrentWeight() + b.Weight;
            double newVolume = CalculateCurrentVolume() + b.Volume;

            if (newWeight < MaxWeight && MaxVolume > newVolume)
                return true;
            else
                return false;
        }
        double CalculateCurrentWeight()
        {
            double weight = 0;

            foreach (var b in Boxes)
                weight += b.Weight;

            return weight; 
            
        }

        double CalculateCurrentVolume()
        {
            double volume = 0;

            foreach (var b in Boxes)
                volume += b.Volume;

            return volume;

        }
    }
}
