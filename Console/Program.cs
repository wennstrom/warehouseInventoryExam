using System;
using System.Collections.Generic;

using Classes;

namespace OOPTenta
{
    public class Program
    {

        static WareHouse wareHouse = new WareHouse();
        static bool runmenu = true;
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to höglagrets inventory system.");
            Console.ReadLine();
            wareHouse.Generate();
            wareHouse.TestData();
            do
            {
                try
                {
                    Menu();
                }
                catch
                {
                    Console.WriteLine("Wrong Input!");
                }
            } while (runmenu);

            Console.WriteLine("Thank you for using Höglagrets inventory system");
            Console.ReadLine();
        }
        static void Menu()
        {
            Console.Clear();

            Console.WriteLine("What would you like to do?");
            Console.WriteLine("--------------------------");
            Console.WriteLine("   [1] Store Box");
            Console.WriteLine("   [2] Remove Box");
            Console.WriteLine("   [3] Find Box");
            Console.WriteLine("   [4] Move Box");
            Console.WriteLine("   [5] Show Höglagret");
            Console.WriteLine("   [0] End Program");

            int selection = int.Parse(Console.ReadLine());

            switch (selection)
            {
                case 1:
                    StoreBox();
                    break;
                case 2:
                    RemoveBox();
                    break;
                case 3:
                    PrintBox();
                    break;
                case 4:
                    MoveBox();
                    break;
                case 5:
                    PrintWareHouse();
                    break;
                case 0:
                    runmenu = false;
                    break;
                default:
                    break;

            }


        }
        static void StoreBox()
        {
            int id = -1;

            try
            {
                Console.Clear();
                Console.WriteLine("Enter the box ID (1-999)");
                id = int.Parse(Console.ReadLine());

                if (id < 1 || 999 < id)
                {
                    Console.Clear();
                    Console.WriteLine("Wrong length! The ID has to be between 1-999");
                    Console.ReadLine();

                }
                if (!wareHouse.AvailableID(id))
                {
                    Console.Clear();
                    Console.WriteLine("The ID is not available");
                    Console.ReadLine();
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Wrong input! The ID has to be entered with an integer");
                Console.ReadLine();
                id = 1;

            }

            Console.WriteLine("Enter the boxes description");
            string description = Console.ReadLine();

            if (description == "")
            {
                Console.WriteLine("ERROR! The description has to be atleast one character long");
                Console.ReadLine();
                return;
            }

            if (id != -1)
            {
                try
                {
                    Console.WriteLine("What Type of Box would you like to store?");
                    Console.WriteLine("[1] Cube \n[2] Cubeoid \n[3] Sphere \n[4] Blob\n Enter: ");
                    int option = int.Parse(Console.ReadLine());
                    switch (option)
                    {
                        case 1:
                            AddCube(id, description);
                            break;
                        case 2:
                            AddCubeoid(id, description);
                            break;
                        case 3:
                            AddCube(id, description);                           
                            break;
                        case 4:
                            AddCube(id, description);
                            break;
                        default:
                            Console.WriteLine("Wrong input! Press enter to go back to the main menu");
                            Console.ReadLine();
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine("ERROR! \n You did not enter a number!");
                    Console.ReadLine();
                    return;
                }
            }

        }
        static void AddCube(int id, string description)
        {
            int side = -1;
            double weight = -1;
            try
            {
                Console.WriteLine("Enter the size of the cubes side");
                side = int.Parse(Console.ReadLine());

                if (side < 0)
                {
                    Console.WriteLine("ERROR \nThe cubes side has to be greater than 0");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine("Enter the weight of the cube");
                weight = double.Parse(Console.ReadLine());

                if (weight < 0)
                {
                    Console.WriteLine("ERROR \nThe cubes weight has to be grater than 0");
                    Console.ReadLine();
                    return;
                }
                if (weight > 1000)
                {
                    Console.WriteLine("ERROR \nThe maximum weight is 1000");
                    Console.ReadLine();
                    return;
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("ERROR \nYou did not enter a number");
                Console.ReadLine();
                return;
            }

            bool fragile = IsItFragile("cube");

            Cube c = new Cube(id, description, weight, side, fragile);

            int location = -1, floor = -1, position = -1;

            if (Manual())
            {
                try
                {
                    Console.WriteLine("Enter the desired floor for the box (integer 1-3)");
                    floor = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("ERROR! \nYou did not enter a number");
                    Console.ReadLine();
                    return;
                }
                if (floor < 1 || 3 < floor)
                {
                    Console.WriteLine("You entered a floor that does not exists. \nPress enter to return to the main menu");
                    Console.ReadLine();
                    return;
                }

                try
                {
                    Console.WriteLine("Enter the desired location for the box (integer 1-100)");
                    position = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("ERROR! \nYou did not enter a number");
                    Console.ReadLine();
                    return;
                }
                if (position < 1 || 100 < position)
                {
                    Console.WriteLine("You entered a location that does not exists. \nPress enter to return to the main menu");
                    Console.ReadLine();
                    return;
                }
                else if (floor == 1)
                {
                    location = (position - 1);
                }
                else
                {
                    location = (floor - 1) * 100 + (position - 1);
                }

                if (!wareHouse.AddableLocation(c, location))
                {
                    Console.WriteLine("The entered location is not available.");
                    Console.ReadLine();
                    return;
                }

            }
            else
            {
                location = wareHouse.GetAvailableSpot(c);

                if (location == -1)
                {
                    Console.Clear();
                    Console.WriteLine("Could not find an available location for the cubeoid.");
                    Console.ReadLine();
                    return;
                }
            }

            wareHouse.AddBox(c, location);
            string pos = wareHouse.GetFrontEndLocation(location);

            Console.Clear();
            Console.WriteLine($"The box {c.ID} was added succesfully on {pos}. ");
            Console.ReadLine();



        }
        static void AddCubeoid(int id, string description)
        {
            int xside = -1, yside = -1, zside = -1;
            double weight = -1;
            try
            {
                Console.WriteLine("Enter the size of the cubeoids XSide");
                xside = int.Parse(Console.ReadLine());

                if (xside < 0)
                {

                    Console.WriteLine("ERROR! \nThe sides size has to be greater than 0");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine("Enter the size of the cubeoids YSide");
                yside = int.Parse(Console.ReadLine());

                if (yside < 0)
                {
                    Console.WriteLine("ERROR! \nThe sides size has to be greater than 0");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine("Enter the size of the cubeoids ZSide");
                zside = int.Parse(Console.ReadLine());
                if (zside < 0)
                {
                    Console.WriteLine("ERROR! \nThe sides size has to be greater than 0");
                    Console.ReadLine();
                    return;
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("ERROR! \nYou did not enter a number.");
                Console.ReadLine();
                return;
            }

            try
            {
                Console.WriteLine("Enter the weight of the cubeoid in KG (Max weight is 1000)");
                weight = double.Parse(Console.ReadLine());

                if (weight < 0)
                {
                    Console.WriteLine("ERROR! \nThe weight has to be greater than 0");
                    Console.ReadLine();
                    return;
                }
                if (weight > 1000)
                {
                    Console.WriteLine("ERROR! \nThe max weight is 1000");
                    Console.ReadLine();
                    return;
                }
            }
            catch
            {
                Console.WriteLine("ERROR! \nYou did not enter a number");
                Console.ReadLine();
                return;
            }

            bool fragile = IsItFragile("cubeoid");

            Cubeoid c = new Cubeoid(id, description, weight, xside, yside, zside, fragile);


            int location = -1;

            if (Manual())
            {
                int position = -1, floor = -1;

                Console.WriteLine("Enter the desired floor for the box (integer 1-3)");
                floor = int.Parse(Console.ReadLine());

                if (floor < 1 || 3 < floor)
                {
                    Console.WriteLine("You entered a floor that does not exists. \nPress enter to return to the main menu");
                    Console.ReadLine();
                    return;
                }
                try
                {
                    Console.WriteLine("Enter the desired location for the box (integer 1-100)");
                    position = int.Parse(Console.ReadLine());
                    if (position < 1 || 100 < position)
                    {
                        Console.WriteLine("You entered a location that does not exists. \nPress enter to return to the main menu");
                        Console.ReadLine();
                        return;
                    }
                }
                catch
                {
                    Console.WriteLine("ERROR! \nYou did not enter a number!");
                    Console.ReadLine();
                    return;
                }

                if (floor == 1)
                {
                    location = (position - 1);
                }
                else
                {
                    location = (floor - 1) * 100 + (position - 1);
                }

                if (!wareHouse.AddableLocation(c, location))
                {
                    Console.WriteLine("The entered location is not available.");
                    Console.ReadLine();
                    return;
                }

            }
            else
            {
                location = wareHouse.GetAvailableSpot(c);

                if (location == -1)
                {
                    Console.Clear();
                    Console.WriteLine("Could not find an available location for the cubeoid.");
                    Console.ReadLine();
                    return;
                }
            }

            wareHouse.AddBox(c, location);
            string pos = wareHouse.GetFrontEndLocation(location);

            Console.Clear();
            Console.WriteLine($"The box {c.ID} was added succesfully on {pos}. ");
            Console.ReadLine();

        }
        static void AddSphere(int id, string description)
        {
            int radius = -1;
            try
            {
                Console.WriteLine("Enter the size of the spheres radius (in cm)");
                radius = int.Parse(Console.ReadLine());

                if (radius < 0)
                {
                    Console.WriteLine("ERROR! \nThe radius has to be greater than 0 cm");
                    Console.WriteLine();
                    return;
                }
            }
            catch
            {
                Console.WriteLine("Wrong input! \nThe radius has to be entered as an integer (no decimals)");
                Console.ReadLine();
                return;
            }
            double weight = -1;
            try
            {
                Console.WriteLine("Enter the weight of the sphere in KG (Max weight is 1000)");
                weight = double.Parse(Console.ReadLine());
                if (weight < 0)
                {
                    Console.WriteLine("ERROR \nThe spheres weight has to be greater than 0 KG");
                    Console.ReadLine();
                    return;
                }
                if (weight > 1000)
                {
                    Console.WriteLine($"ERROR \nThe max weight is 1000 kg you entered {weight}");
                    Console.ReadLine();
                    return;
                }
            }
            catch
            {
                Console.WriteLine("Wrong input! \nThe radius has to be entered as a number");
                Console.ReadLine();
                return;
            }


            bool fragile = IsItFragile("sphere");

            Sphere s = new Sphere(id, description, weight, radius, fragile);

            int location = -1;

            if (Manual())
            {
                Console.WriteLine("Enter the desired floor for the box (integer 1-3)");
                int floor = int.Parse(Console.ReadLine());

                if (floor < 1 || 3 < floor)
                {
                    Console.WriteLine("You entered a floor that does not exists. \nPress enter to return to the main menu");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine("Enter the desired location for the box (integer 1-100)");
                int position = int.Parse(Console.ReadLine());
                if (position < 1 || 100 < position)
                {
                    Console.WriteLine("You entered a location that does not exists. \nPress enter to return to the main menu");
                    Console.ReadLine();
                    return;
                }
                else if (floor == 1)
                {
                    location = (position - 1);
                }
                else
                {
                    location = (floor - 1) * 100 + (position - 1);
                }

                if (!wareHouse.AddableLocation(s, location))
                {
                    Console.WriteLine("The entered location is not available.");
                    Console.ReadLine();
                    return;
                }

            }
            else
            {
                location = wareHouse.GetAvailableSpot(s);

                if (location == -1)
                {
                    Console.Clear();
                    Console.WriteLine("Could not find an available location for the cubeoid.");
                    Console.ReadLine();
                    return;
                }
            }

            wareHouse.AddBox(s, location);
            string pos = wareHouse.GetFrontEndLocation(location);
            Console.Clear();
            Console.WriteLine($"The box {s.ID} was added succesfully on {pos}. ");
            Console.ReadLine();

        }
        static void AddBlob(int id, string description)
        {
            int side = -1;
            try
            {
                Console.WriteLine("Enter the size of the blob side (in cm)");
                side = int.Parse(Console.ReadLine());

                if (side < 0)
                {
                    Console.WriteLine("The blobs side has to be greater than 0.");
                    Console.ReadLine();
                    return;
                }
            }
            catch
            {
                Console.WriteLine("Error the sides size has to be entered as an integer (No Decimals).");
                Console.ReadLine();
                return;
            }

            double weight = -1;
            try
            {
                Console.WriteLine("Enter the weight of the blob in KG (Max Weight is 1000 KG)");
                weight = double.Parse(Console.ReadLine());

                if (weight < 0)
                {
                    Console.WriteLine("ERROR! \nThe blobs weight has to be greater than 0 KG");
                    Console.ReadLine();
                    return;
                }
                if (weight > 1000)
                {
                    Console.WriteLine($"ERROR! \nThe max weight is 1000 you entered {weight}");
                    Console.ReadLine();
                    return;
                }
            }
            catch
            {
                Console.WriteLine("Error the blobs weight has to be entered as a number.");
                Console.ReadLine();
                return;
            }

            Blob b = new Blob(id, description, weight, side);

            int location = -1;

            if (Manual())
            {
                Console.WriteLine("Enter the desired floor for the box (integer 1-3)");
                int floor = int.Parse(Console.ReadLine());

                if (floor < 1 || 3 < floor)
                {
                    Console.WriteLine("You entered a floor that does not exists. \nPress enter to return to the main menu");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine("Enter the desired location for the box (integer 1-100)");
                int position = int.Parse(Console.ReadLine());
                if (position < 1 || 100 < position)
                {
                    Console.WriteLine("You entered a location that does not exists. \nPress enter to return to the main menu");
                    Console.ReadLine();
                    return;
                }
                if (floor == 1)
                {
                    location = (position - 1);
                }
                else
                {
                    location = (floor - 1) * 100 + (position - 1);
                }

                if (!wareHouse.AddableLocation(b, location))
                {
                    Console.WriteLine("The entered location is not available.");
                    Console.ReadLine();
                    return;
                }

            }
            else
            {
                location = wareHouse.GetAvailableSpot(b);

                if (location == -1)
                {
                    Console.Clear();
                    Console.WriteLine("Could not find an available location for the cubeoid.");
                    Console.ReadLine();
                    return;
                }
            }


            wareHouse.AddBox(b, location);
            string pos = wareHouse.GetFrontEndLocation(location);
            Console.Clear();
            Console.WriteLine($"The box {b.ID} was added succesfully on {pos}. ");
            Console.ReadLine();

        }
        static void RemoveBox()
        {
            Console.Clear();
            int id = -1;

            try
            {
                Console.WriteLine("Enter the ID of the cube that you wish to remove");
                id = int.Parse(Console.ReadLine());

            }
            catch
            {
                Console.WriteLine("ERROR \nYou did not enter a integer");
                Console.ReadLine();
                return;
            }

            if (wareHouse.AvailableID(id))
            {
                Console.WriteLine("ERROR \nCould not find the entered id");
                Console.ReadLine();
                return;
            }
            else
            {
                Box b = wareHouse.GetBox(id);
                wareHouse.RemoveBox(b);

                Console.WriteLine($"The Box with id {b.ID} has succesfully been removed from the warehouse");
                Console.ReadLine();
            }


        }
        static void MoveBox()
        {
            Console.Clear();
            int id = -1, location;
            try
            {
                Console.WriteLine("Enter id of the box you wish to move");
                id = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Wrong input! \nPlease enter id with an integer.");
                Console.ReadLine();
                return;
            }

            if (wareHouse.AvailableID(id))
            {
                Console.WriteLine("Could not find the entered ID");
                Console.ReadLine();
                return;
            }

            Console.Clear();
            Console.WriteLine("Now Enter the new position for the box");
            Console.WriteLine("--------------------------------------");
            Console.Write("\nEnter Floor (1-3): ");
            int floor = -1;
            try
            {
                floor = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Wrong input! The Floor has to be entered as a integer");
                Console.ReadLine();
                return;
            }
            if (floor < 1 || 3 < floor)
            {
                Console.WriteLine("The floor you entered does not exists.");
                Console.ReadLine();
                return;
            }
            int pos = -1;
            try
            {
                Console.Write("\nEnter Location (1-100)");
                pos = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Wrong input! The location has to be entered as a integer");
                Console.ReadLine();
            }
            if (pos < 1 || 100 < pos)
            {
                Console.WriteLine("The pos you entered does not exists");
                Console.ReadLine();
                return;
            }


            if (floor == 1)
            {
                location = (pos - 1);
            }
            else
            {
                location = (floor - 1) * 100 + (pos - 1);
            }
            Box b = wareHouse.GetBox(id);

            wareHouse.RemoveBox(b);
            wareHouse.AddBox(b, pos);


            Console.WriteLine($"The box {b.ID} is now located at {wareHouse.GetFrontEndLocation(location)}");
            Console.ReadLine();

        }
        static void PrintBox()
        {
            Console.Clear();
            int id = -1;
            try
            {
                Console.WriteLine("Enter the id of the box that you wish to find");
                id = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("ERROR! \nYou did not enter a number");
                Console.ReadLine();
                return;
            }

            if (wareHouse.AvailableID(id))
            {
                Console.WriteLine("Could not find the entered id... ");
                Console.ReadLine();
                return;
            }
            else
            {
                Console.Clear();
                var b = wareHouse.GetBox(id);
                string bOutput = wareHouse.DisplayBox(b);
                Console.ReadLine();
            }
        }
        static void PrintWareHouse()
        {
            string output = wareHouse.DisplayWarehouse();

            Console.Clear();
            Console.WriteLine(output);
            Console.ReadLine();
        }
        static bool IsItFragile(string shape)
        {
            bool fragile = false;
            try
            {
                Console.WriteLine($"Is the {shape} fragile?");
                Console.WriteLine("[1] Yes \n[2] No\nEnter: ");
                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        return true;
                    case 2:
                        return false;
                    default:
                        Console.Clear();
                        Console.WriteLine("Wrong input! \nThe answer has to be entered with an integer between 1-2");
                        Console.WriteLine("Press any key to try again");
                        Console.ReadLine();
                        IsItFragile(shape);
                        break;
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Wrong input! \nThe answer has to be entered with an integer between 1-2");
                Console.WriteLine("Press any key to try again");
                Console.ReadLine();
                IsItFragile(shape);
            }
            return fragile;
        }
        static bool Manual()
        {
            Console.Clear();
            Console.WriteLine("Would you like to use a manual or generated location?");
            Console.WriteLine("[1] Manual \n[2] Generated");
            int input = int.Parse(Console.ReadLine());
            bool manual = false;
            try
            {
                switch (input)
                {
                    case 1:
                        return true;
                    case 2:
                        return false;
                    default:
                        Console.Clear();
                        Console.WriteLine("Wrong input! \nThe answer has to be entered with an integer between 1-2.");
                        Console.ReadLine();
                        Manual();
                        break;
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Wrong input! \nThe answer has to be entered with an integer between 1-2.");
                Console.ReadLine();
                Manual();
            }
            return manual;
        }
    }
}
