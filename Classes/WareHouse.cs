using System;
using System.Collections.Generic;
using System.Text;


namespace Classes
{
    public class WareHouse
    {
        static WareHouseLocation wareHouseLocation = new WareHouseLocation();
        public static WareHouseLocation[] location = new WareHouseLocation[300];
        public void Generate()
        {
            for (int i = 0; i < 300; i++)
            {
                location[i] = new WareHouseLocation();
            }
        }
        public void TestData()
        {
            Blob one = new Blob(15, "Para", 1, 2);
            location[13].Boxes.Add(one);

            Cube two = new Cube(127, "Rubik's Cube", 2, 5, false);
            location[77].Boxes.Add(two);

            Cube three = new Cube(111, "PS4", 1, 3, true);
            location[80].Boxes.Add(three);

            Cube four = new Cube(177, "PC", 5, 4, true);
            location[177].Boxes.Add(four);

            Cube five = new Cube(132, "AirPods", 1, 2, false);
            location[233].Boxes.Add(five);

            Cube six = new Cube(22, "Cake", 2, 5, true);
            location[255].Boxes.Add(six);

            Cubeoid seven = new Cubeoid(179, "Mona Lisa", 1.2, 1, 2, 2, true);
            location[288].Boxes.Add(seven);

            Sphere eight = new Sphere(28, "Whiskey", 0.75, 2, true);
            location[66].Boxes.Add(eight);

            Sphere nine = new Sphere(55, "Football", 1.5, 1, false);
            location[235].Boxes.Add(nine);

            Sphere ten = new Sphere(19, "Disco Ball", 5, 3, true);
            location[6].Boxes.Add(ten);

        }
        public bool AddableLocation(Box b, int pos)
        {
            if (b.IsFragile)
            {
                if (CurrentWeight(pos) == 0)
                {
                    return true;
                }
            }
            else
            {
                if (b.Weight + CurrentWeight(pos) <= wareHouseLocation.maxweight && b.Volume + CurrentVolume(pos) <= wareHouseLocation.maxvolume)
                {
                    return true;
                }
            }
            return false;

        }
        public void AddBox(Box b, int pos)
        {
            location[pos].boxes.Add(b);
        }

        public bool RemoveBox(Box c)
        {
            bool result = false;

            for (int i = 0; i < location.Length; i++)
            {
                foreach (Cube current in location[i].boxes)
                {
                    if (current.ID == c.ID)
                    {
                        location[i].boxes.Remove(c);
                        return true;
                    }

                }
            }
            return result;
        }

        public int GetAvailableSpot(Box c)
        {
            for (int i = 0; i < location.Length; i++)
            {
                if (CurrentWeight(i) == 0)
                {
                    return i;
                }
            }
            for (int i = 0; i < location.Length; i++)
            {
                if (CurrentWeight(i) + c.Weight <= 1000 && !c.IsFragile)
                {
                    return i;
                }
            }
            return -1;
        }
        public int GetLocation(Box c)
        {
            for (int i = 0; i < location.Length; i++)
            {
                foreach (Box check in location[i].boxes)
                {
                    if (c.ID == check.ID)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }
        public string GetFrontEndLocation(int pos)
        {
            if (pos < 100)
            {
                return $"Floor: 1, Pos: {pos + 1}";
            }
            else if (pos > 99 && 200 > pos)
            {
                return $"Floor: 2, Pos: {(pos - 100) + 1}";
            }
            else
            {
                return $"Floor: 3, Pos: {(pos - 200) + 1}";
            }
        }
        public Box GetBox(int id)
        {
            for (int i = 0; i < 299; i++)
            {
                foreach (Box b in location[i].boxes)
                {
                    if (b.ID == id)
                    {
                        return (b);
                    }
                }
            }

            return null;
        }
        public string GetShape(Box b)
        {
            if (b is Cube)
            {
                return "Cube";
            }
            else if (b is Cubeoid)
            {
                return "Cubeoid";
            }
            else if (b is Sphere)
            {
                return "Sphere";
            }
            else
            {
                return "Blob";
            }
        }
        double CurrentVolume(int pos)
        {
            double result = 0;

            foreach (Box box in location[pos].boxes)
            {
                result += box.Volume;
            }

            return result;
        }
        double CurrentWeight(int pos)
        {
            double result = 0;

            foreach (Box box in location[pos].boxes)
            {
                result += box.Weight;
            }

            return result;
        }
        public bool AvailableID(int id)
        {
            for (int i = 0; i < location.Length; i++)
            {
                foreach (Box b in location[i].Boxes)
                {
                    if (b.ID == id)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public string DisplayBox(Box b)
        {
            int p = GetLocation(b);
            string floorAndPos = GetFrontEndLocation(p), shape = GetShape(b);
            string fragile = "";

            if (b.IsFragile)
            {
                fragile = "Yes";
            }
            else
            {
                fragile = "No";
            }


            StringBuilder sb = new StringBuilder();
            sb.AppendLine("     Box Information");
            sb.AppendLine("--------------------------");
            sb.AppendLine($"ID: {b.ID}");
            sb.AppendLine($"Location: {floorAndPos}");
            sb.AppendLine($"Shape: {shape}");
            sb.AppendLine($"Description: {b.Description}");
            sb.AppendLine($"Weight: {b.Weight}KG");
            sb.AppendLine($"Area: {b.Area}");
            sb.AppendLine($"Volume: {b.Volume}");
            sb.AppendLine($"Fragile: {fragile}");
            sb.AppendLine($"Insurance Value: {b.InsuranceValue}");


            return sb.ToString();
        }
        public string DisplayWarehouse()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("Höglagrets warehouse");
            result.AppendLine("--------------------");

            for (int i = 0; i < 300; i++)
            {
                string pos = GetFrontEndLocation(i);
                result.AppendLine(pos);

                foreach (Box b in location[i].boxes)
                {
                    result.AppendLine($"[ID: {b.ID} Shape: {GetShape(b)}]");
                }

            }

            return result.ToString();
        }



    }
}

