using System;
using System.Linq;

namespace PragueParking_1
{
    class Program
    {
        public static string[] parkArray = new string[101];

        static void Main(string[] args)
        {
            while (true)
            {
                Menu.mainMenu();
            }
        }


        /*
         * Fixa alla felmeddelanden - Byt ut "Something went wrong" med ett mer konkret felmeddelande
         * Fixa Locate-koden - ta bort individuella MCs/Bilar
         * Kontrollera regnummer för MCs som lagras - 2 MCs skall inte kunna använda samma registreringsnummer
         * Börja med Collect-koden - Skriv ut ett kvitto till användaren när bilen har hämtats
         * Fixa input 1 --> 'Back to main menu?', no fungerar inte som den ska
         * Fixa input 2 --> Fixa for-loop, Flytta mc till plats 20, men MC placeras på plats 19
         */


        /*-----------------------------------------------------------------------------------------------

                                                  DESIGN ÄNDRINGAR

        1. Input 1 --> Car --> Fixa Console.Clear
        2. Input 1 --> 1 MC --> Fixa Console.Clear
        3. Input 2 --> Move behöver Console.Clear
        4. Input 2 --> Move 2 MCs --> Fixa färg + Console.Clear
        5. 

        -----------------------------------------------------------------------------------------------*/

        public class Menu
        {
            public static void mainMenu()
            {
                try
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("1:Park vehicle\n2:Move vehicle\n3:Check Array\n4:Locate vehicle\n5:Collect vehicle\n0:Quit");
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
                    Console.Write("Input: ");
                    Console.ResetColor();
                    string search = Console.ReadLine();
                    string parkVehicles;
                    int input = int.Parse(search);

                    switch (input)
                    {
                        case 1:
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("1 - Park a car\n" +
                                "2 - Park one or two MCs");
                            Console.ResetColor();
                            parkVehicles = Console.ReadLine();
                            if (parkVehicles == "1")
                            {
                                int userInput = 1;
                                Parking.ParkCar(userInput);
                            }

                            else if (parkVehicles == "2")
                            {
                                int userInput = 2;
                                Parking.ParkMC(userInput);
                            }

                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Incorrect input. Returning back to main menu...");
                                Console.ResetColor();
                                Menu.mainMenu();
                            }
                            break;

                        case 2:
                            Console.Clear();
                            Parking.MoveVehicle();
                            break;

                        case 3:
                            Console.Clear();
                            Parking.PrintArray();
                            break;

                        case 4:
                            Console.Clear();
                            SearchVehicle();
                            break;

                        case 5:
                            Console.Clear();
                            CollectVehicle();
                            break;

                        case 0:
                            Console.WriteLine("Quitting the program...");
                            Environment.Exit(0);
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine();
                            Console.WriteLine("Invalid menu input. Please try again!");
                            Console.WriteLine();
                            Console.ResetColor();
                            break;
                    }
                }

                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine("Incorrect format detected in menu. Please enter a number between 0 - 5");
                    Console.WriteLine();
                    Console.ResetColor();
                }
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                      PARK A CAR CODE
        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public class Parking
        {
            public static string TypeOfVehicle(string vehiclePlate, int userInput)
            {
                if (userInput == 1)
                {
                    vehiclePlate = "Car#" + vehiclePlate;
                    return vehiclePlate;
                }

                else if (userInput == 2)
                {
                    vehiclePlate = "MC#" + vehiclePlate;
                    return vehiclePlate;
                }

                return vehiclePlate;
            }

            public static string ParkCar(int userInput) 
            {
                string regNmr, backToMenu;

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Enter the registration number: ");
                Console.ResetColor();
                string newPlate = Console.ReadLine();

                regNmr = TypeOfVehicle(newPlate, userInput);

                if (string.IsNullOrEmpty(regNmr))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine("You need to enter your registration plate!");
                    Console.WriteLine();
                    Console.ResetColor();
                    Menu.mainMenu();
                }

                if (regNmr.Length < 4)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine("Registration number too short. Enter between 4-10 characters. Press any key to try again...");
                    Console.WriteLine();
                    Console.ResetColor();
                    Menu.mainMenu();
                }

                else if (regNmr.Length > 10)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine("Registration number too long. Enter between 4-10 characters. Press any key to try again...");
                    Console.WriteLine();
                    Console.ResetColor();
                    Menu.mainMenu();
                }

                else if (parkArray.Contains(regNmr))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("A vehicle with the same registration plate is already here...");
                    Console.WriteLine("Try again by pressing the 'Enter' key");
                    Console.ReadKey();
                    Console.ResetColor();
                    Menu.mainMenu();
                }

                else
                {
                    for (int i = 1; i < parkArray.Length; i++)
                    {
                        if (parkArray[i] == null)
                        {
                            parkArray[i] = regNmr;
                            DateTime Date = DateTime.Now;

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine();
                            Console.WriteLine("Your car: {0}\nparked on parking spot: P{1}\nCar parked at: {2}", regNmr, " " + i, " " + Date);
                            Console.WriteLine();
                            Console.ResetColor();
                            break;
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Back to the menu? [y] or [n]?");
                    Console.ResetColor();
                    Console.WriteLine();

                    backToMenu = Console.ReadLine().ToLower();

                    if (backToMenu == "y")
                    {
                        Menu.mainMenu();
                        return ParkCar(userInput);
                    }

                    else if (backToMenu == "n")
                    {
                        Console.WriteLine("Closing down the program");
                        Environment.Exit(1);
                    }

                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid user input...");
                        Console.WriteLine("Returning to 'Add vehicle' option in the menu");
                        Console.ResetColor();
                    }
                }

                return ParkCar(userInput);
            }

            //-----------------------------------------------------------------------------------------------------------------------------------------------------
            //                                                      PARK AN MC CODE
            //-----------------------------------------------------------------------------------------------------------------------------------------------------

            public static string ParkMC(int userInput)
            {
                
                try
                {
                    string mcInput, backToMenu;

                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("How many MCs do you want to park, 1 or 2?");
                    Console.ResetColor();
                    mcInput = Console.ReadLine();

                    string mcReg1, mcReg2;

                    if (mcInput == "1")
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Enter the registration number: ");
                        Console.ResetColor();
                        mcReg1 = Console.ReadLine();

                        string newMCPlate = TypeOfVehicle(mcReg1, userInput);

                        if (newMCPlate == "")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine();
                            Console.WriteLine("You need to enter your registration plate!");
                            Console.WriteLine();
                            Console.ResetColor();
                            Menu.mainMenu();
                        }

                        else if (newMCPlate.Length < 4)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine();
                            Console.WriteLine("Registration number too short. Enter between 4-10 characters. Press any key to try again...");
                            Console.WriteLine();
                            Console.ResetColor();
                            Menu.mainMenu();
                        }

                        else if (newMCPlate.Length > 10)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine();
                            Console.WriteLine("Registration number too long. Enter between 4-10 characters. Press any key to try again...");
                            Console.WriteLine();
                            Console.ResetColor();
                            Menu.mainMenu();
                        }

                        else if (parkArray.Contains(newMCPlate))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("A vehicle with the same registration plate is already here...");
                            Console.WriteLine("Try again by pressing the 'Enter' key");
                            Console.ReadKey();
                            Console.ResetColor();
                            Menu.mainMenu();
                        }

                        else
                        {
                            for (int i = 1; i < parkArray.Length; i++)
                            {
                                if (parkArray[i] == null)
                                {
                                    parkArray[i] = newMCPlate;
                                    DateTime Date = DateTime.Now;

                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine();
                                    Console.WriteLine("Your car: {0}\nparked on parking spot: P{1}\nCar parked at: {2}", newMCPlate, " " + i, " " + Date);
                                    Console.WriteLine();
                                    Console.ResetColor();
                                    break;
                                }
                            }

                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Back to the menu? [y] or [n]?");
                            Console.ResetColor();
                            Console.WriteLine();

                            backToMenu = Console.ReadLine().ToLower();

                            if (backToMenu == "y")
                            {
                                Menu.mainMenu();
                                return ParkCar(userInput);
                            }

                            else if (backToMenu == "n")
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Closing down the program");
                                Console.ResetColor();
                                Environment.Exit(1);
                            }

                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid user input...");
                                Console.WriteLine("Returning to 'Add vehicle' option in the menu");
                                Console.ResetColor();
                            }
                        }


                    }
                    //---------------------------------------------------------------------------------------------------------------------
                    else if (mcInput == "2")
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Enter the registration number for your first MC: ");
                        Console.ResetColor();
                        string newPlate1 = Console.ReadLine();

                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Enter the registration number for your second MC: ");
                        Console.ResetColor();
                        string newPlate2 = Console.ReadLine();

                        mcReg1 = TypeOfVehicle(newPlate1, userInput);
                        mcReg2 = TypeOfVehicle(newPlate2, userInput);

                        string mcRegPlates = mcReg1 + " | " + mcReg2;

                        if (mcReg1.Length <= 4)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("The registration number is too short!");
                            Console.ResetColor();
                            Menu.mainMenu();
                        }

                        else if (mcReg1.Length > 10)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("The registration number is too Long!");
                            Console.ResetColor();
                            Menu.mainMenu();
                        }


                        for (int i = 1; i < parkArray.Length; i++)
                        {
                            if (parkArray[i] == null)
                            {
                                parkArray[i] = mcRegPlates;
                                DateTime Date = DateTime.Now;

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine();
                                Console.WriteLine("Your MC(s): {0}\nparked on parking spot: P{1}\nMC(s) parked at: {2}", mcRegPlates, " " + i, " " + Date);
                                Console.WriteLine();
                                Console.ResetColor();
                                Menu.mainMenu();
                                break;
                            }
                        }
                    }

                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input. Program only accepts user to park 1 or 2 MCs... Try again!");
                        Console.ResetColor();
                        ParkMC(userInput);
                    }
                }

                catch (FormatException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine("Something went wrong with adding MCs... Please try again!", e);
                    Console.WriteLine();
                    Console.ResetColor();
                }

                return ParkMC(userInput);
            }

            //-----------------------------------------------------------------------------------------------------------------------------------------------------
            //                                                      MOVE VEHICLE CODE
            //-----------------------------------------------------------------------------------------------------------------------------------------------------

            public static string MoveVehicle() //TODO: Skapa en submeny
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Which type of vehicle do you want to move, car or mc?");
                    Console.ResetColor();
                    string vehiclePlate = Console.ReadLine().ToLower();

                    if(vehiclePlate == "car")
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Enter your registration plate number: ");
                        Console.ResetColor();
                        vehiclePlate = Console.ReadLine();

                        for(int i = 0; i < parkArray.Length; i++)
                        {
                            if(parkArray[i] == null)
                            {
                                continue;
                            }

                            if(parkArray[i].Contains(vehiclePlate))
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine();
                                Console.WriteLine("Vehicle is currently parked on parking space: {0} ", i);
                                Console.WriteLine();
                                Console.ResetColor();

                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("Enter a new parking space number: ");
                                Console.ResetColor();
                                Console.WriteLine();

                                int moveVehicle = int.Parse(Console.ReadLine());
                                string moveRegPlate = FindVehicleParkedOnSpot(vehiclePlate);

                                if(moveRegPlate.Contains("Car#"))
                                {
                                    if(parkArray[moveVehicle - 1] == null)
                                    {
                                        parkArray[moveVehicle] = parkArray[i];
                                        parkArray[i] = null;
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("Vehicle with registration plate: {0} has been moved to spot {1} ", vehiclePlate, moveVehicle);
                                        Console.ResetColor();
                                        Menu.mainMenu();
                                        Console.ReadKey();
                                        break;
                                    }

                                    else if(parkArray[moveVehicle - 1] != parkArray[i])
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine();
                                        Console.WriteLine("A vehicle with registration plate: {0} is already parked on this spot...", parkArray[moveVehicle - 1]);
                                        Console.WriteLine();
                                        Console.ResetColor();
                                        Console.ReadLine();
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    if(vehiclePlate == "mc")
                    {
                        int mcAmount;
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("How many MCs do you want to move?");
                        Console.ResetColor();
                        mcAmount = int.Parse(Console.ReadLine());

                        if(mcAmount == 1)
                        {
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Enter MC registration number: ");
                            Console.ResetColor();
                            vehiclePlate = Console.ReadLine();

                            for (int i = 0; i < parkArray.Length; i++)
                            {
                                if (parkArray[i] == null)
                                {
                                    continue;
                                }

                                if (parkArray[i].Contains(vehiclePlate))
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine();
                                    Console.WriteLine("Vehicle is currently parked on parking space: {0} ", i);
                                    Console.WriteLine();
                                    Console.ResetColor();

                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.WriteLine("Enter a new parking space number: ");
                                    Console.ResetColor();
                                    Console.WriteLine();

                                    int moveVehicle = int.Parse(Console.ReadLine());
                                    string moveRegPlate = FindVehicleParkedOnSpot(vehiclePlate);

                                    if (moveRegPlate.Contains("MC#"))
                                    {
                                    
                                    if (parkArray[moveVehicle - 1] == null)
                                    {
                                        parkArray[moveVehicle] = parkArray[i];
                                        parkArray[i] = null;
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine();
                                        Console.WriteLine("Vehicle with registration plate: {0} has been moved to spot {1} ", vehiclePlate, moveVehicle);
                                        Menu.mainMenu();
                                        Console.ReadKey();
                                        break;
                                    }

                                    else if (parkArray[moveVehicle - 1] != parkArray[i])
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine();
                                        Console.WriteLine("A vehicle with registration plate: {0} is already parked on this spot...", parkArray[moveVehicle - 1]);
                                        Console.WriteLine();
                                        Console.ResetColor();
                                        Console.ReadLine();
                                        break;
                                    }
                                }
                            }
                        }
                    }

                        else if (mcAmount == 2) // ----------------------------------------------------------------------------------------------------  MOVE 2
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Enter your first MCs registration number: ");
                            Console.ResetColor();
                            vehiclePlate = Console.ReadLine();

                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Enter your second MCs registration number: ");
                            Console.ResetColor();
                            string vehiclePlate2 = Console.ReadLine();

                            for (int i = 0; i < parkArray.Length; i++)
                            {
                                if (parkArray[i] == null)
                                {
                                    continue;
                                }

                                if (parkArray[i].Contains(vehiclePlate))
                                {
                                    Console.Clear();
                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Your MCs: {0} and {1} are currently parked on spot {2}", vehiclePlate, vehiclePlate2, i);
                                    Console.ResetColor();
                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.WriteLine("Enter a new parking space: ");
                                    Console.ResetColor();
                                    int moveMC1 = int.Parse(Console.ReadLine());
                                    string moveRegPlate = FindVehicleParkedOnSpot(vehiclePlate);

                                    if (moveRegPlate.Contains("MC#"))
                                    {
                                        if (parkArray[moveMC1 - 1] == null)
                                        {
                                            parkArray[moveMC1] = parkArray[i];
                                            parkArray[i] = null;
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("Your MC, {0} and {1} are moved to spot {2}", vehiclePlate, vehiclePlate2, moveMC1);
                                            Console.ResetColor();
                                            Menu.mainMenu();
                                            Console.ReadKey();
                                            break;
                                        }

                                        else if (parkArray[moveMC1 - 1] != parkArray[i])
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine();
                                            Console.WriteLine("A vehicle with registration plate: {0} is already parked on this spot...", parkArray[moveMC1 - 1]);
                                            Console.WriteLine();
                                            Console.ResetColor();
                                            break;
                                        }
                                    }

                                    else if (moveRegPlate.Contains("MC#"))
                                    {
                                        if (parkArray[moveMC1 - 1] == null)
                                        {
                                            parkArray[moveMC1 - 1] = parkArray[i];
                                            parkArray[i] = null;
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("MCs: {0} and {1} have been moved to spot {2}", vehiclePlate, vehiclePlate2, moveMC1);
                                            Console.ResetColor();
                                            Menu.mainMenu();
                                            Console.ReadLine();
                                            break;
                                        }

                                        else if (parkArray[moveMC1 - 1].Contains(" | ") == true)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine();
                                            Console.WriteLine("This parking spot is currently taken... Please choose another spot.");
                                            Console.WriteLine();
                                            Console.ResetColor();
                                        }

                                        else if (parkArray[moveMC1 - 1].Contains("MC#") == true)
                                        {
                                            vehiclePlate = Parking.TypeOfVehicle(vehiclePlate, 2);
                                            string parkedMC = parkArray[moveMC1 - 1];
                                            string mcRegPlate1 = parkedMC + " | " + vehiclePlate;

                                            parkArray[moveMC1 - 1] = mcRegPlate1;
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("MC is now parked on parking spot {0}", moveMC1);
                                            Console.ResetColor();
                                            Console.ReadLine();
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    

                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Something went wrong when trying to register 2 MCs");
                            Console.ResetColor();
                        }
                    }
                }

                catch (FormatException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Something went wrong with moving your vehicle...", e);
                    Console.ResetColor();
                }

                return MoveVehicle();
            }

            public static string FindVehicleParkedOnSpot(string vehiclePlate)        //Fungerar
            {
                /*
                * Metod för att for-loopa igenom parkeringslistan - returnerar regnr på parkerad bil på specifik plats.
                * if (parkingList.Contains("CAR@" + vehiclePlate) || parkingList.Contains("MC@" + vehiclePlate))
                */

                for (int i = 0; i < parkArray.Length; i++)
                {
                    if (parkArray[i] == null)
                    {
                        continue;
                    }
                    
                    if (parkArray[i].Contains(vehiclePlate))
                    {
                        string returnedPlate = parkArray[i];
                        return returnedPlate;
                    }
                }
                return null;
            }

            //-----------------------------------------------------------------------------------------------------------------------------------------------------
            //                                                      PRINT OUT ARRAY CODE
            //-----------------------------------------------------------------------------------------------------------------------------------------------------

            public static void PrintArray()
            {
                int cols = 6;
                int rows = 1;

                for (int i = 1; i < parkArray.Length; i++)
                {
                    if (rows >= cols && rows % cols == 0)
                    {
                        Console.WriteLine();
                        rows = 1;
                    }

                    if (parkArray[i] == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(i + ": Empty \t");
                        Console.ResetColor();
                        rows++;
                    }

                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(i + ": " + parkArray[i] + "\t");
                        Console.ResetColor();
                        rows++;
                    }
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.WriteLine("---------------------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("\t\tPress any key to return to the menu");
                Console.WriteLine();
                Console.WriteLine("---------------------------------------------------------------------------");
                Console.ResetColor();
                Console.ReadKey();
                Menu.mainMenu();
            }

        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                      COLLECT VEHICLE CODE
        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        // FIXA TYPE OF VEHICLE --> Förtillfället visas det som 0

        public static string CollectVehicle() 
        {
            int userInput = 0;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Enter your registration number: ");
            Console.ResetColor();

            try
            {
                string vehiclePlate = Console.ReadLine().ToLower();

                if(SearchVehicle(vehiclePlate))
                {
                    Console.Clear();

                    int index = VehicleLocation(vehiclePlate);

                    if(parkArray[index].Contains("|"))
                    {
                        DateTime checkOut = DateTime.Now;
                        string vehType = Parking.TypeOfVehicle(vehiclePlate, userInput);

                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Vehicle checked out");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Vehicle Details:");
                        Console.WriteLine();
                        Console.WriteLine("Registration number: {0}", vehiclePlate);
                        Console.WriteLine();
                        Console.WriteLine("Type: {0}", userInput);
                        Console.WriteLine();
                        Console.WriteLine("Date: {0}", checkOut);
                        Console.WriteLine();
                        Console.ResetColor();

                        parkArray[index] = parkArray[index].Replace("MC#" + vehiclePlate, "");
                        parkArray[index] = parkArray[index].Replace("|", "");
                        Menu.mainMenu();
                    }

                    else
                    {
                        DateTime checkOut = DateTime.Now;
                        string vehType = Parking.TypeOfVehicle(vehiclePlate, userInput);

                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Vehicle checked out");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Vehicle Details:");
                        Console.WriteLine();
                        Console.WriteLine("Registration number: {0}", vehiclePlate);
                        Console.WriteLine();
                        Console.WriteLine("Type: {0}", userInput);
                        Console.WriteLine();
                        Console.WriteLine("Date: {0}", checkOut);
                        Console.WriteLine();
                        Console.ResetColor();

                        parkArray[index] = null;
                        Menu.mainMenu();
                    }
                }

                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Given registration number: {0} does not exist in our system... Please try again!");
                    Console.WriteLine();
                    Console.ResetColor();
                }
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }

            return CollectVehicle();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                      LOCATE VEHICLE CODE
        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public static string SearchVehicle() 
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Enter your registration number: ");
                Console.ResetColor();
                string vehiclePlate = Console.ReadLine().ToLower();

                int index = VehicleLocation(vehiclePlate);

                if(SearchVehicle(vehiclePlate))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Your vehicle {0}, is parked on spot {1}", vehiclePlate, index);
                    Console.WriteLine();
                    Console.ResetColor();
                    Menu.mainMenu();
                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Incorrect user input in SearchVehicle.");
                    Console.ResetColor();
                }
            }
            
            catch(NullReferenceException e)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
            }

            catch (FormatException e)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
            }

            return SearchVehicle();
        }

        static bool SearchVehicle(string vehiclePlate)
        {
            vehiclePlate.ToUpper();

            for (int i = 1; i < parkArray.Length; i++)
            {
                if (parkArray[i] != null && parkArray[i].Contains(vehiclePlate))
                {
                    return true;
                }
            }
            return false;
        }

        public static int VehicleLocation(string vehiclePlate)
        {
            vehiclePlate.ToLower();

            for(int i = 1; i < parkArray.Length; i++)
            {
                if (parkArray[i] != null && parkArray[i].Contains(vehiclePlate))
                {
                    int index = i;
                    return index;
                }
            }
            return 0;
        }
    }
}

