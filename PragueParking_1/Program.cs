using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace PragueParking_1
{
    class Program
    {
        public static string[] parkArray = new string[101];

        static void Main(string[] args)
        {
            while (true)
            {
                mainMenu();
            }
        }

            public static void mainMenu()
            {
                try
                {

                // Visar menyn. Här får användarne välja vad de vill göra i menyn

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

                    switch (input) // Början av min meny 
                    {
                        case 1: // Case 1 - kontrollerar om användaren väljer att parkera en bil eller mc. Det som användaren väljer för antingen ett Car# eller MC# tag framför reg numret
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
                                Console.WriteLine("Incorrect input. Returning back to main ..");
                                Console.ResetColor();
                                mainMenu();
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

                catch (FormatException) // FormatExceptiopn - Kontrollerar användarens input. Om användaren skriver in ett nummer som inte finns i menyn, då visas felmeddelandet nedan
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine("Incorrect format detected in  Please enter a number between 0 - 5");
                    Console.WriteLine();
                    Console.ResetColor();
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
                    vehiclePlate = "Car#" + vehiclePlate; // Om användaren vill spara en bil, då sparas car# framför reg numret
                    return vehiclePlate;
                }

                else if (userInput == 2)
                {
                    vehiclePlate = "MC#" + vehiclePlate; // Om användaren vill spara en mc, då sparas mc# framför reg numret
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
                    mainMenu();
                }

                if (regNmr.Length < 4) // Kontrollerar användarens reg input. Om det är mindre än 4 tecken, då visas detta felmeddelande
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine("Registration number too short. Enter between 4-10 characters. Press any key to try again...");
                    Console.WriteLine();
                    Console.ResetColor();
                    mainMenu();
                }

                else if (regNmr.Length > 10) // Kontrollerar användarens reg input. Om det är mer än 10 tecken, då visas detta felmeddelande
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine("Registration number too long. Enter between 4-10 characters. Press any key to try again...");
                    Console.WriteLine();
                    Console.ResetColor();
                    mainMenu();
                }

                else if (parkArray.Contains(regNmr)) // Om användarens input redan finns i arrayen, då visas detta felmeddelande
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("A vehicle with the same registration plate is already here...");
                    Console.WriteLine("Try again by pressing the 'Enter' key");
                    Console.ReadKey();
                    Console.ResetColor();
                    mainMenu();
                }

                    for (int i = 1; i < parkArray.Length; i++) // Loopar genom arrayen
                    {
                        if (parkArray[i] == null)
                        {
                            parkArray[i] = regNmr;
                            DateTime Date = DateTime.Now; // Skapar en variabel som sparar tiden

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
                        mainMenu();
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

                        if (newMCPlate == "") // Om användarens input är tomt, visas följande felmeddelande
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine();
                            Console.WriteLine("You need to enter your registration plate!");
                            Console.WriteLine();
                            Console.ResetColor();
                            mainMenu();
                        }

                        else if (newMCPlate.Length < 4)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine();
                            Console.WriteLine("Registration number too short. Enter between 4-10 characters. Press any key to try again...");
                            Console.WriteLine();
                            Console.ResetColor();
                            mainMenu();
                        }

                        else if (newMCPlate.Length > 10)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine();
                            Console.WriteLine("Registration number too long. Enter between 4-10 characters. Press any key to try again...");
                            Console.WriteLine();
                            Console.ResetColor();
                            mainMenu();
                        }

                        else if (parkArray.Contains(newMCPlate))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("A vehicle with the same registration plate is already here...");
                            Console.WriteLine("Try again by pressing the 'Enter' key");
                            Console.ResetColor();
                            mainMenu();
                        }

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
                                mainMenu();
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
                    //---------------------------------------------------------------------------------------------------------------------
                    else if (mcInput == "2") // All kod nedan körs om användaren väljer att spara 2 mcs
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

                        mcReg1 = TypeOfVehicle(newPlate1, userInput); // Kontrollerar typen av forden. I detta fall är det mc (MC#)
                        mcReg2 = TypeOfVehicle(newPlate2, userInput); // Kontrollerar typen av forden. I detta fall är det mc (MC#)

                        string mcRegPlates = mcReg1 + " | " + mcReg2; // Skapar formatet till 2 sparade MCs

                        if (mcReg1.Length <= 4)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("The registration number is too short!");
                            Console.ResetColor();
                        }

                        else if (mcReg1.Length > 10)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("The registration number is too Long!");
                            Console.ResetColor();
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
                mainMenu();
                return ParkMC(userInput);
            }

            //-----------------------------------------------------------------------------------------------------------------------------------------------------
            //                                                      MOVE VEHICLE CODE
            //-----------------------------------------------------------------------------------------------------------------------------------------------------

            public static string MoveVehicle() 
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Which type of vehicle do you want to move, car or mc?");
                    Console.ResetColor();
                    string vehiclePlate = Console.ReadLine().ToLower();

                    if (vehiclePlate == "car") // All kod körs nedan om användaren vill flytta på en bil
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Enter your registration plate number: ");
                        Console.ResetColor();
                        vehiclePlate = Console.ReadLine();

                        for (int i = 0; i < parkArray.Length; i++)
                        {
                            if (parkArray[i] == null) // Kontrollerar ifall arrayen är tom
                            {
                                continue;
                            }

                            if (parkArray[i].Contains(vehiclePlate)) // Kollar vart reg numret är sparad i arrayen
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

                                if (moveRegPlate.Contains("Car#")) // Kontrollerar om reg numret är en bil eller mc
                                {
                                    if (parkArray[moveVehicle - 1] == null) // Flyttar bilen och ger förra indexen värdet null
                                    {
                                        parkArray[moveVehicle] = parkArray[i]; // Sparar den nya positionen i arrayen som bilen har flyttats till
                                        parkArray[i] = null;
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("Vehicle with registration plate: {0} has been moved to spot {1} ", vehiclePlate, moveVehicle);
                                        Console.ResetColor();
                                        mainMenu();
                                        break;
                                    }

                                    else if (parkArray[moveVehicle - 1] != parkArray[i]) // Kontrollerar platsen dit du vill flytta bilen. Om platsen är upptagen, visa följande felmeddelande
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

                    if (vehiclePlate == "mc") // All kod nedan körs om användaren vill spara en eller två MCs
                    {
                        int mcAmount;
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("How many MCs do you want to move?");
                        Console.ResetColor();
                        mcAmount = int.Parse(Console.ReadLine());

                        if (mcAmount == 1)
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

                                else if (parkArray[i].Contains(vehiclePlate))
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
                                    int mySpot = VehicleLocation(vehiclePlate);

                                    //kontrollera om det är en mc


                                    if (moveRegPlate.Contains("|") && parkArray[moveVehicle] == null) // Kontrollerar tecknet som skiljer 2 MCs med varandra " | "
                                    {
                                        string[] vehicle = moveRegPlate.Split(" | "); // Splittar på de två MCs parkerade på samma plats och sparar de två Mcs i en sträng array

                                        if (vehicle[0] == "MC#" + vehiclePlate) // Kollar om det användaren skrev in är en MC
                                        {
                                            parkArray[mySpot] = vehicle[1]; // Flyttar på den MC som användaren skrev in, men håller kvar den andra MCn
                                            parkArray[moveVehicle] = vehicle[0];
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("Vehicle {0} is moved to spot {1}", vehiclePlate, i);
                                            Console.ResetColor();
                                        }
                                        
                                        else if (vehicle[1] == "MC#" + vehiclePlate) // Kollar om reg numret har MC# tag framför
                                        {
                                            parkArray[mySpot] = vehicle[0];
                                            parkArray[moveVehicle] = vehicle[1];
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("Vehicle {0} is moved to spot {1}", vehiclePlate, i);
                                            Console.ResetColor();
                                        }

                                        mainMenu();
                                    }

                                    else
                                    {
                                        string[] vehicle = moveRegPlate.Split(" | "); // Splittar MCn igen
                                        if (vehicle[0] == "MC#" + vehiclePlate)
                                        {
                                            parkArray[mySpot] = vehicle[1];
                                            parkArray[moveVehicle] = vehicle[0];
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("Vehicle {0} is moved to spot {1}", vehiclePlate, i);
                                            Console.ResetColor();
                                        }
                                        else if (vehicle[1] == "MC#" + vehiclePlate)
                                        {
                                            parkArray[mySpot] = vehicle[0];
                                            parkArray[moveVehicle] = vehicle[1];
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("Vehicle {0} is moved to spot {1}", vehiclePlate, i);
                                            Console.ResetColor();
                                        }
                                    }
                                }
                            }
                        }
                    

                        else if (mcAmount == 2) // Kod körs om användaren vill flytta på 2 MCs
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
                                        if (parkArray[moveMC1 - 1] == null) // Nollställer platsen
                                        {
                                            parkArray[moveMC1] = parkArray[i];
                                            parkArray[i] = null;
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("Your MC, {0} and {1} are moved to spot {2}", vehiclePlate, vehiclePlate2, moveMC1);
                                            Console.ResetColor();
                                            mainMenu();
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
                                            mainMenu();
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

            public static string FindVehicleParkedOnSpot(string vehiclePlate)       
            {
                /*
                 * Metod som kontrollerar vart fordon är parkerade
                 */

                for (int i = 0; i < parkArray.Length; i++) // Loopar genom arrayen
                {
                    if (parkArray[i] == null) // Kollar om arrayen är tom
                    {
                        continue;
                    }

                    if (parkArray[i].Contains(vehiclePlate)) // Kollar om reg numret redan finns
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

            public static void PrintArray() // Design 
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
                mainMenu();
            }

        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                      COLLECT VEHICLE CODE
        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public static string CollectVehicle() // Kod för att hämta ut ett fordon
        {
            int userInput = 0;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Enter your registration number: ");
            Console.ResetColor();

            try
            {
                string vehiclePlate = Console.ReadLine().ToLower();

                if (SearchVehicle(vehiclePlate)) // Anropar metoden SearchVehicles för att se om fordonet redan finns
                {
                    Console.Clear();

                    int index = VehicleLocation(vehiclePlate); // Anropar metoden VehicleLocation för att se om reg numret har taggen car# eller mc#

                    if (parkArray[index].Contains("|")) // Kollar om dubbel MC har en separator 
                    {
                        string type = "MC";

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
                        Console.WriteLine("Type: {0}", type);
                        Console.WriteLine();
                        Console.WriteLine("Date: {0}", checkOut);
                        Console.WriteLine();
                        Console.ResetColor();

                        parkArray[index] = parkArray[index].Replace("MC#" + vehiclePlate, "");
                        parkArray[index] = parkArray[index].Replace("|", "");
                        mainMenu();
                    }

                    else
                    {
                        DateTime checkOut = DateTime.Now;
                        string vehType = Parking.TypeOfVehicle(vehiclePlate, userInput);
                        string type = "Car";

                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Vehicle checked out");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Vehicle Details:");
                        Console.WriteLine();
                        Console.WriteLine("Registration number: {0}", vehiclePlate);
                        Console.WriteLine();
                        Console.WriteLine("Type: {0}", type);
                        Console.WriteLine();
                        Console.WriteLine("Date: {0}", checkOut);
                        Console.WriteLine();
                        Console.ResetColor();

                        parkArray[index] = null;
                        mainMenu();
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

        public static string SearchVehicle() // Metod SearchVehicle
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Enter your registration number: ");
                Console.ResetColor();
                string vehiclePlate = Console.ReadLine().ToLower();

                int index = VehicleLocation(vehiclePlate); // Visar användaren vilken position fordonet ligger på

                if (SearchVehicle(vehiclePlate))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Your vehicle {0}, is parked on spot {1}", vehiclePlate, index);
                    Console.WriteLine();
                    Console.ResetColor();
                    mainMenu();
                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Incorrect user input in SearchVehicle.");
                    Console.ResetColor();
                }
            }

            catch (NullReferenceException e)
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

        static bool SearchVehicle(string vehiclePlate) // Denna motod anropas när användaren söker efter sitt fordon
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

        public static int VehicleLocation(string vehiclePlate) // Denna metod anropas när användaren vill veta vilken parkeringsplats fordonet är parkerat på
        {
            vehiclePlate.ToLower();

            for (int i = 1; i < parkArray.Length; i++)
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

