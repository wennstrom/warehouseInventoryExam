using System;
using System.Threading;
using Classes;

namespace OOPTenta
{
    public class Program
    {
        private static WareHouse _wareHouse = new WareHouse();
        static void Main()
        {
            _wareHouse.Generate();
            _wareHouse.TestData();

            Console.WriteLine("Welcome to höglarets warehouse.");
            Thread.Sleep(1000);

            MainMenu();

            Console.Clear();
            Console.WriteLine("Thank you for using höglagrets warehouse.");
            Thread.Sleep(1000);
        }
        static void MainMenu()
        {
            ConsoleKey userinput;
            do
            {
                Console.Clear();
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("--------------------------");
                Console.WriteLine("1. Store Box");
                Console.WriteLine("2. Move Box");
                Console.WriteLine("3. Remove Box");
                Console.WriteLine("4. Show box");
                Console.WriteLine("5. Get Overview of warehouse");
                Console.WriteLine("0. Exit");

                userinput = Console.ReadKey(true).Key;
                switch (userinput)
                {
                    case ConsoleKey.D1:
                        AddBox(GetBoxType());
                        break;
                    case ConsoleKey.D2:
                        MoveBox();
                        break;
                    case ConsoleKey.D3:
                        RemoveBox();
                        break;
                    case ConsoleKey.D4:
                        ShowBox();
                        break;
                    case ConsoleKey.D5:
                        GetOverview();
                        break;
                    case ConsoleKey.D0:
                        break;
                    default:
                        break;
                }
            } while (userinput != ConsoleKey.D0);
        }
        static void AddBox(string boxType)
        {
            I3DStorageObject box = null;
            int id = GetId(true);
            string description = GetDescription();
            double weight = GetWeight();
            bool isFragile = GetFragile();
            try
            {
                switch (boxType)
                {
                    case "cube":
                        int side = GetSide(string.Empty);
                        box = _wareHouse.CreateCube(id, description, weight, isFragile, side);
                        break;
                    case "cubeoid":
                        int xSide = GetSide("x");
                        int ySide = GetSide("y");
                        int zSide = GetSide("z");
                        box = _wareHouse.CreateCubeoid(id, description, weight, isFragile, xSide, ySide, zSide);
                        break;
                    case "sphere":
                        int radius = GetRadius();
                        box = _wareHouse.CreateSphere(id, description, weight, isFragile, radius);
                        break;
                    case "blob":
                        side = GetSide("");
                        box = _wareHouse.CreateBlob(id, description, weight, side);
                        break;
                }
                int pos = -1;

                if (ManualPos())
                    pos = GetManualLocation();
                else
                    pos = GetAutomaticLocation(box);

                _wareHouse.StoreBox(box, pos);
                Console.WriteLine("The box was added succesfully");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }

        }
        static void MoveBox()
        {
            try
            {
                int id = GetId(false);
                int position = -1;
                Console.WriteLine("Enter the new location for your box.");
                if (ManualPos())
                    position = GetManualLocation();
                else
                {
                    var box = _wareHouse.GetBoxByID(id);
                    position = GetAutomaticLocation(box);
                }



                _wareHouse.MoveBox(id, position);
                Console.Clear();
                Console.WriteLine("The box was moved succesfully");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
        static void RemoveBox()
        {
            int id = GetId(false);

            try
            {
                _wareHouse.RemoveBox(id);
                Console.Clear();
                Console.WriteLine("The box was removed succesfully");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
        static void ShowBox()
        {
            bool correctinput = true;
            I3DStorageObject box = null;
            int id = -1;
            do
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Enter id to see content of a box:");
                    id = int.Parse(Console.ReadLine());
                    correctinput = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                    correctinput = false;
                }
            } while (correctinput == false);
            try
            {
                box = _wareHouse.GetBoxByID(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            if (box != null)
            {
                Console.Clear();
                int pos = _wareHouse.GetLocationByID(id);
                string frontendpos = _wareHouse.GetFrontedLocation(pos);
                Console.WriteLine($"Box Location: {frontendpos}");
                Console.WriteLine(box.ToString());
                Console.ReadLine();
            }

        }
        static void GetOverview()
        {
            Console.Clear();
            Console.WriteLine(_wareHouse.WarehouseOverview());
            Console.ReadLine();
        }
        static int GetId(bool newID)
        {
            int id = 0;
            do
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Enter id (1 - 9999): ");
                    id = int.Parse(Console.ReadLine());

                    if (id < 1 || 9999 < id)
                        throw new Exception();

                    //when adding a new id it has to be unique
                    if (_wareHouse.UniqueID(id) == false && newID == true)
                        throw new InvalidOperationException();

                }
                catch (InvalidOperationException)
                {
                    id = -1; //to make the loop continue
                    Console.WriteLine("The id has to be unique! \n Try again with a new id.");
                    Console.ReadLine();
                }
                catch
                {
                    Console.WriteLine("ID has to be entered as an integer between 1-9999. \nPress any key to try again.");
                    Console.ReadLine();
                }

            } while (id < 1 || 9999 < id);

            return id;
        }
        static string GetDescription()
        {
            string description = "";

            do
            {
                Console.Clear();
                Console.WriteLine("Enter a description (min 1 character and max 50): ");
                description = Console.ReadLine();

                if (description.Length < 0 || 50 < description.Length)
                {
                    Console.WriteLine("Invalid description length. The description has to be atleast 1 character and max 50. \nPress any key to try again.");
                    Console.ReadLine();
                }
            } while (description.Length < 0 || 50 < description.Length);

            return description;
        }
        static double GetWeight()
        {
            double weight = 0;
            do
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Enter weight (max 1000 kg)");
                    weight = int.Parse(Console.ReadLine());

                    if (weight < 1 || 1000 < weight)
                        throw new Exception();
                }
                catch
                {
                    Console.WriteLine("Weight has to be entered as a number and have a weight thats bigger than 0 kg and lesser than 1000 kg.");
                    Console.ReadLine();
                }
            } while (weight < 1 || 100 < weight);

            return weight;
        }
        static string GetBoxType()
        {
            bool correctInput = false;
            string boxType = string.Empty;
            do
            {
                Console.Clear();
                Console.WriteLine("Enter Boxtype");
                Console.WriteLine("-------------");
                Console.WriteLine("1. Cube");
                Console.WriteLine("2. Cubeoid");
                Console.WriteLine("3. Sphere");
                Console.WriteLine("4. Blob");

                ConsoleKeyInfo userinput = Console.ReadKey(true);

                switch (userinput.Key)
                {
                    case ConsoleKey.D1:
                        boxType = "cube";
                        correctInput = true;
                        break;
                    case ConsoleKey.D2:
                        boxType = "cubeoid";
                        correctInput = true;
                        break;
                    case ConsoleKey.D3:
                        boxType = "sphere";
                        correctInput = true;
                        break;
                    case ConsoleKey.D4:
                        boxType = "blob";
                        correctInput = true;
                        break;
                }

            } while (correctInput == false);

            return boxType;
        }
        static bool ManualPos()
        {
            bool result = false, correctinput = false;
            ConsoleKey input;
            do
            {
                Console.Clear();
                Console.WriteLine("Would you like to store the box with a manual position or automatic?");
                Console.WriteLine("1. Manual");
                Console.WriteLine("2. Automatic");
                input = Console.ReadKey(true).Key;

                if (input == ConsoleKey.D1)
                {
                    result = true;
                    correctinput = true;
                }
                if (input == ConsoleKey.D2)
                {
                    result = false;
                    correctinput = true;
                }

            } while (correctinput == false);

            return result;
        }
        static int GetRadius()
        {
            int radius = 0;
            do
            {
                try
                {
                    Console.WriteLine($"Enter length of the radius: ");
                    radius = int.Parse(Console.ReadLine());

                    if (radius < 0)
                        throw new Exception();
                }
                catch
                {
                    Console.WriteLine("Length has to be entered as a number that's bigger than 0");
                    Console.ReadLine();
                }
            } while (radius < 0);
            return radius;
        }
        static int GetSide(string sideType)
        {
            int side = -1;
            do
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine($"Enter length of the {sideType}Side: ");
                    side = int.Parse(Console.ReadLine());

                    if (side < 0)
                        throw new Exception();
                }
                catch
                {
                    Console.WriteLine("Length has to be entered as a number that's bigger than 0");
                    Console.ReadLine();
                }
            } while (side < 1);

            return side;
        }
        static int GetAutomaticLocation(I3DStorageObject box)
        {
            return _wareHouse.GetFreeLocation(box);
        }
        static int GetManualLocation()
        {
            //returns the index position. 
            return (GetFloor() * 100) + (GetPos());
        }
        static int GetFloor()
        {
            int floor = -1;
            do
            {
                try
                {
                    Console.Clear();
                    Console.Write("Enter floor (1-3) : ");
                    floor = int.Parse(Console.ReadLine());

                    if (floor < 1 || 3 < floor)
                        throw new Exception();
                }
                catch
                {
                    Console.WriteLine("Floor has to be entered as an integer between 1-3");
                    Console.ReadLine();
                }
                Console.Clear();
            } while (floor < 1 || 3 < floor);

            return floor - 1;
        }
        static int GetPos()
        {
            int pos = -1;
            do
            {
                try
                {
                    Console.Clear();
                    Console.Write("Enter position (1-100): ");
                    pos = int.Parse(Console.ReadLine());

                    if (pos < 1 || 100 < pos)
                        throw new Exception();
                }
                catch
                {
                    Console.WriteLine("Position has to be entered as an integer between 1-100");
                    Console.ReadLine();
                }
            } while (pos < 1 || 100 < pos);

            return pos - 1;
        }
        static bool GetFragile()
        {
            bool correctinput = false, result = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Is the box fragile?");
                Console.WriteLine("1. Yes \n2. No");
                ConsoleKeyInfo input = Console.ReadKey(true);


                if (input.Key == ConsoleKey.D1)
                {
                    result = true;
                    correctinput = true;
                }
                if (input.Key == ConsoleKey.D2)
                {
                    result = false;
                    correctinput = true;
                }

            } while (correctinput != true);

            return result;
        }
    }  
}
