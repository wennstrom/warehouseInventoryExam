using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;


namespace Classes
{
    public class WareHouse
    {
        private static WareHouseLocation[] location;
        public void Generate()
        {
            location = new WareHouseLocation[300];
            //generates 300 empty locations
            // <100 = floor 1, 100-199 = floor 2, 200-300 = floor 3
            for (int i = 0; i < 299; i++)
                location[i] = new WareHouseLocation();
            
        }
        public I3DStorageObject CreateCube(int id, string description, double weight, bool isFragile, int side)
        {
            return new Cube(id, description, isFragile, weight, side);
        }
        public I3DStorageObject CreateCubeoid(int id, string description, double weight, bool isFragile, int xSide, int ySide, int zSide)
        {
            return new Cubeoid(id, description, weight, xSide, ySide, zSide, isFragile);
        }
        public I3DStorageObject CreateSphere(int id, string description, double weight, bool isFragile, int radius)
        {
            return new Sphere(id, description, weight, radius, isFragile);

        }
        public I3DStorageObject CreateBlob(int id, string description, double weight, int side)
        {
            Blob b = new Blob(id, description, weight, side);
            return b;
        }
        public void StoreBox(I3DStorageObject b, int pos)
        {
            location[pos].Add(b);

        }
        public void MoveBox(int id, int newPos)
        {
            var b = GetBoxByID(id);
            int oldPos = GetLocationByID(b.ID);

            if (newPos == oldPos)
                throw new Exception("The Box is already located at that position. Please try agian with a new position.");

            if (location[newPos].AddableLocation(b))
            {
                location[oldPos].Remove(b);
                location[newPos].Add(b);
            }
            

            throw new InvalidOperationException("Could not add a new box because the desired position is not available.");
        }
        public void RemoveBox(int id)
        {
            //Gets the position and box and then removes it if no exceptions occurs.
            int pos = GetLocationByID(id);
            I3DStorageObject b = GetBoxByID(id);
            location[pos].Remove(b);

        }
        public string WarehouseOverview()
        {
            var result = new StringBuilder();
            result.AppendLine("Warehouse Overview");
            result.AppendLine("------------------");
            
            for (int i = 0; i < location.Length; i++)
            {
                result.AppendLine(GetFrontedLocation(i));
                result.AppendLine(location[i].LocationOverview());
            }

            return result.ToString();
        }
        public int GetLocationByID(int id)
        {
            //returns location if found otherwise an exception will be thrown. 
            for (int i = 0; i < location.Length; i++)
                if (location[i].LocationContainsID(id))
                    return i;

            throw new ArgumentOutOfRangeException("Could not find a box with that id.");
        }
        public I3DStorageObject GetBoxByID(int id)
        {
            I3DStorageObject b = null;

            for (int i = 0; i < location.Length; i++)
                if (location[i].LocationContainsID(id))
                    b = location[i].GetByID(id);

            if (b is null)
                throw new ArgumentOutOfRangeException("Could not find a box with that ID");

            return b;
        }
        public int GetFreeLocation(I3DStorageObject b)
        {
            for (int i = 0; i < location.Length; i++)
                if (location[i].AddableLocation(b))
                    return i;

            throw new ArgumentOutOfRangeException("Could not find an addable location for your box.");
        }
        public string GetFrontedLocation(int index)
        {
            var result = string.Empty;

            if (index < 100)
                result = $"Floor 1, Position {index + 1}";
            else if (index < 200)
                result = $"Floor 2, Position {index - 99}";
            else
                result = $"Floor 3, Postion {index - 199}";

            return result;
        }
        public bool UniqueID(int id)
        {
            for (int i = 0; i < location.Length; i++)
                if (location[i].LocationContainsID(id))
                    return false;

            return true;

        }
        public void TestData()
        {
            //populates the warehouse with testdata samples.

            var one = new Blob(15, "Para", 1, 2);
            location[13].Add(one);

            var two = new Cube(127, "Rubik's Cube", false, 22.3, 4);
            location[77].Add(two);

            var three = new Cube(111, "PS4", true, 3, 2);
            location[80].Add(three);

            var four = new Cube(177, "PC", true, 5, 4);
            location[177].Add(four);

            var five = new Cube(132, "AirPods", false, 1, 2);
            location[233].Add(five);

            var six = new Cube(22, "Cake", true, 2, 5);
            location[255].Add(six);

            var seven = new Cubeoid(179, "Mona Lisa", 1.2, 1, 2, 2, true);
            location[288].Add(seven);

            var eight = new Sphere(28, "Whiskey", 0.75, 2, true);
            location[66].Add(eight);

            var nine = new Sphere(55, "Football", 1.5, 1, false);
            location[235].Add(nine);

            var ten = new Sphere(19, "Disco Ball", 5, 3, true);
            location[6].Add(ten);

        }

    }
}

