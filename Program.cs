using System.Buffers.Text;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Drawing;
using System.Resources;
using System.Security.AccessControl;
using System.Xml.Serialization;
using static System.Formats.Asn1.AsnWriter;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ZombieGame
{
    internal class Program
    {
        //Inventory string by Samuel B 26/5/2024//
        private static string[] Inventory = new string[0];
        private static string instructions = string.Empty;
        private static string choice = string.Empty;
        
        static void Main()
        {
            // Mustafa - Starting the game
            int ans;
            Console.WriteLine("\n\n\t\t\t\t\t\tWhispers of the Dead");
            Console.WriteLine("\n\t\t\t\t\t\t   1) Start");
            Console.WriteLine("\t\t\t\t\t\t   2) Quit");
            ans = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            // Mustafa -Intro to the Game
            if (ans == 1)
            {

                string combinedText = "In a world overrun by the Undead, survival is paramount.\n" +
                                          "Welcome to [Whispers of the Dead].\n" +
                                          "Where every shadow could conceal a lurking horror and every step could lead to your doom.\n" +
                                          "As civilization crumbles, you must navigate through the chaos, scavenging for resources, fortifying shelters.\n" +
                                          "But beware, for the infected hordes are relentless, driven by an insatiable hunger for flesh.\n" +
                                          "Will you muster the courage to fight back, or will you join the ranks of the walking dead?\n" +
                                          "The choices are yours in this heart-pounding journey through the apocalypse.";

                PrintOneByOne(combinedText);
            }
            else
            {
                // Mustafa - Quiting the game
                Console.WriteLine("Quiting the game.");
            }

            Console.Clear();
            Console.WriteLine("'You hear a voice screaming and you wake up'");




            Console.ReadLine();
            Console.Clear();
            //Input call by Samuel B 25/5/2024//
            Input();
        }
        //Input method by Samuel B 25/5/2024//
        static void Input()
        {
            
            //Do while loop by Samuel B 25/5/2024 | If the user types an answer other than yes or no when asked for instructions//
            do
            {
                //Ask for user choice Samuel B 25/5/2024
                Console.Write("Would you like instructions?: ");
                choice = Console.ReadLine();

                if (choice.ToLower() == "yes")
                {

                    instructions = Instructions(instructions);
                    Console.WriteLine(instructions);
                    
                }
                else if (choice.ToLower() == "no")
                {
                    Console.WriteLine("Press enter to continue...");
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please type 'yes' or 'no'");
                }
            } while (choice != "yes" && choice != "no");

            Console.ReadLine();
            //Call to Shed method by Samuel B 25/5/2024//
            House();

        }
        //Shed method by Samuel B 26/05/2024//
        static void House()
        {
            //Items array by Samuel B 28/05/2024//
            string[] houseItems = new string[] { "Shotgun", "Bulletproof Vest" };

            Console.WriteLine("\nYou are located within a small 2 story house located in the northern outskirts of Dubravica");
            do
            {

                Console.Write("\nWhat's next? > ");
                choice = Console.ReadLine().ToLower();
                //Changed if statement to switch statement by Samuel B 29/05/2024
                switch (choice)
                {
                    case "map":
                        map();
                        break;
                    case "inventory":
                        ShowInventory();
                        break;
                    case "south":

                        break;
                    case "add shotgun":
                        //Adding item to inv
                        AddItemInv("Shotgun");
                        //remove item from item list
                        houseItems = RemoveItem(houseItems, "Shotgun");
                        break;
                    case "drop shotgun":
                        //Dropping item from inv
                        DropItemInv("Shotgun");
                        //Adding item back to house items
                        houseItems = AddItem(houseItems, "Shotgun");
                        break;
                    case "add vest":
                        //Adding item to inv
                        AddItemInv("Bulletproof Vest");
                        //remove item from item list
                        houseItems = RemoveItem(houseItems, "Bulletproof Vest");
                        break;
                    case "drop vest":
                        // Dropping item from inventory
                        DropItemInv("Bulletproof Vest");
                        // Adding item back to house items
                        houseItems = AddItem(houseItems, "Bulletproof Vest");
                        break;
                    case "?":
                    case "help":
                        Console.WriteLine("You can type commands such as: ");
                        Console.WriteLine("'map' - for viewing the map ");
                        Console.WriteLine("'inventory' - for viewing items your inventory ");
                        Console.WriteLine("'south' - to navigate and proceed south ");
                        Console.WriteLine("'add (item)' - to add an item to your inventory ");
                        Console.WriteLine("'items' - view items located within your current location ");
                        break;
                    case "items":
                        if (houseItems.Length <= 0)
                        {
                            Console.WriteLine("No items left in this location.");

                        }
                        else
                        {
                            Console.Write("\nCurrent items within the house:");
                            foreach (string item in houseItems)
                            {
                                Console.WriteLine("\n- " + item);

                            }
                        }
                        break;
                    default:
                        Console.WriteLine("\nThe path you seek does not exist in this forsaken place. \nChoose wisely, for each misstep might be your last. Dare to try again, and may the shadows guide you.");
                        break;

                }
                Thread.Sleep(1500);
            } while (choice != "south");
            //Call to South method by Samuel B 26/05/2024//
            
                South();
                    
             
        }
        //Methods regarding Item Arrays by Samuel B 27/05/2024----------------------
        static string[] RemoveItem(string[] array, string itemToRemove)
        {
            int index = Array.IndexOf(array, itemToRemove);
            //If the item is not found
            if (index < 0)
                return array;
            //New array length is lesser than original
            string[] newArray = new string[array.Length - 1];
            //Array, Start Index, NewArray, Start Index, Num of Elements to copy to (exclusive)
            Array.Copy(array, 0, newArray, 0, index);
            Array.Copy(array, index + 1, newArray, index, newArray.Length - index);
            return newArray;
        }
        //Method for add items back to the item array by Samuel B 29/05/2024
        static string[] AddItem(string[] array, string itemToAdd)
        {
            string[] newArray = new string[array.Length + 1];
            Array.Copy(array, newArray, array.Length);
            newArray[newArray.Length - 1] = itemToAdd;
            return newArray;
        }
        //-----------------------------------------------------------

        //Methods regarding the inventory array by Samuel B 27/05/2024-----------------------
        //Method for Adding items to inv  by Samuel B 29/05/2024
        static void AddItemInv(string item)
        {
            Array.Resize(ref Inventory, Inventory.Length + 1);

            Inventory[Inventory.Length - 1] = item;
            Console.WriteLine("You grabbed the " + item + ".");

        }
        //Method for removing items to inv  by Samuel B 29/05/2024
        static void DropItemInv(string item)
        {
           int index = Array.IndexOf(Inventory, item);
            //If the item isn't found
            if (index < 0)
            {
                Console.WriteLine("You don't have a " + item + " in your inventory.");
                return;
            }
            Inventory = RemoveItem(Inventory, item);
            Console.WriteLine("You dropped the " + item + ".");

        }
        //Inventory method by Samuel B 27/05/2024//
        static void ShowInventory()
        {
            if (Inventory.Length == 0)
            {
                Console.WriteLine("Your inventory appears to be empty");

            }
            else
            {
                Console.WriteLine("Items in Inventory: ");
                foreach (string item in Inventory)
                {

                    Console.WriteLine("- " + item);
                }
            }

        }
       //--------------------------------
        
        //Thomas F 28/05/2024 working on the south method
        static void South()
        {
     
                Console.WriteLine("\nYou've gone south but not all seems right in the distance whinning echos through what could you could almost distinguish as the Whinge of childern ");
          
                do
                {
                    Console.Write("\nWhat's next? > ");
                    choice = Console.ReadLine().ToLower();
                //Changed if statement to switch statement by Samuel B 29/05/2024
                switch (choice) 
                {
                    case "map":
                        map();
                        break;
                    case "inventory":
                        ShowInventory();
                        break;
                    case "south":    
                    case "east":  
                    case "west":
                        break;


                    default:
                        Console.WriteLine("\nThe path you seek does not exist in this forsaken place. \nChoose wisely, for each misstep might be your last. Dare to try again, and may the shadows guide you.");
                        break;

                }
                
                Thread.Sleep(1500);
            } while (choice != "south" && choice != "east" && choice != "west");
                //If the choices are equal to the south, west and east
                switch (choice)
                {
                    case "south":
                        extendedSouth();
                        break;
                    case "east":
                        east();
                        break;
                    case "west":
                        west();
                        break;
                }
            
        }
        //Extended South room by Samuel B 29/05/2024
        static void extendedSouth()
        {
           
            Console.WriteLine("\nYou find yourself standing at the edge of a quiet street, shrouded in the soft glow ");
            Console.WriteLine("of the evening sun. The street is named \"Crescent Way\", but locals refer to it");
            Console.WriteLine("simply as \"The Forgotten Path.\" The air is thick with mystery, and each house along ");
            Console.WriteLine("the road seems to whisper tales of its own.");
            Console.WriteLine("\nYou have two navigation choices \"left\" or \"right\"");
            do
            {
                Console.Write("\nWhat's next? > ");
                choice = Console.ReadLine().ToLower();

                switch (choice)
                {
                    case "map":
                        map();
                        break;
                    case "inventory":
                        ShowInventory();
                        break;
                    case "left":

                        break;
                    case "right":

                        break;
                    default:
                        Console.WriteLine("\nThe path you seek does not exist in this forsaken place. \nChoose wisely, for each misstep might be your last. Dare to try again, and may the shadows guide you.");
                        break;


                }
            } while (choice != "left" && choice!= "right");
            switch (choice)
            {
                case "left":
                    Left();
                    break;
                case "right":
                    Right();
                    break;

            }



        }
        //Mustafa 
        static void Left()
        {
            Console.WriteLine("The path Lead to the beach would you like to continue?");
            string lans = Console.ReadLine().ToLower();
            if (lans == "Yes")
            {
                Console.WriteLine("Proceeding");
                Thread.Sleep(1000);
                Console.WriteLine(".");
                Thread.Sleep(1000);
                Console.WriteLine(".");
                Thread.Sleep(1000);
                Beach();
            }
            else
            {
                return;
            }
        }
        //Made the Beach Method. -Mustafa
        static void Beach()
        {
            Console.WriteLine("");
        }
        //Made the Right Method - Mustafa
        static void Right()
        {

            Console.WriteLine("The path Lead to the School would you like to continue?");
            string lans = Console.ReadLine().ToLower();
            if (lans == "Yes")
            {
                Console.WriteLine("Proceeding");
                Thread.Sleep(1000);
                Console.WriteLine(".");
                Thread.Sleep(1000);
                Console.WriteLine(".");
                Thread.Sleep(1000);
                School();
            }
            else
            {
                return;
            }
        }
        //Made The School Method
        static void School()
        {
            Console.WriteLine("You are inside the school.");

        }
        //Trevor
        static void east() 
        {
            Console.WriteLine("You have gone East.");
            Console.WriteLine("You see 4 paths ahead");
            Console.WriteLine("Park\nHouse\nChurch\nForest");
            string einput = Console.ReadLine();
            if (einput == "forest")
            {
                Forest();
            }
        }
        //Samuel
        static void west()
        {
            Console.WriteLine("You have gone West");
        }
        static void map()
        {        //Mustafa 25/05/2024 - Adding a basic map to be used for now will make a better one..
            //Samuel 28/05/2024 - Updated map
            Console.WriteLine(@"





                                                                             HOUSE
                                                   Convenience Store           |                   Park
                                         Pharmacy      |                       |                     |     House
                                                  \    |                     South                   |    /
                                                   \   |                       |                     |   /
                                                      West    <----------------|---------------->   East           North
                                                   /   |                       |                     |   \        /
                                                  /    |                       |                     |    \      /
                                        Town Hall      |                       |                     |     Forest ----- East
                                                     Street                    |                   Church        \
                                                                               |                                  \
                                                                             South                                  South
                                                                            /     \
                                                                           /       \
                                                                       Left         Right
                                                                        |             |
                                                                      School          Beach

      N
      |
   W<--->E
      |
      S
");
        }
        static void PrintOneByOne(string text)
        {
            //Mustafa - Making the characters one by one
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(05);
            }
            Console.ReadLine();
        }
        static void Forest()
        {   //Mustafa but Thomas is fixed 30/05/24
            Console.WriteLine("\nYou are Wondering through the forest");
            Console.WriteLine("You see 3 Paths Ahead");
            Console.WriteLine("Which way do you choose to go?");
            Console.WriteLine("North\nSouth\nEast\nGo Back");
            do
            {
                Console.Write("\nWhat's next? > ");
                choice = Console.ReadLine().ToLower();
                switch (choice)
                {
                    case "map":
                        map();
                        break;
                    case "inventory":
                        ShowInventory();
                        break;
                    case "north":
                    case "south":
                    case "east":
                        break;
                    default:
                        Console.WriteLine("\nThe path you seek does not exist in this forsaken place. Choose wisely, for each misstep might be your last. Dare to try again, and may the shadows guide you.");
                        break;
                }
            } while (choice !="south" && choice !="east" && choice != "north");
            switch (choice)
            {
                case "south":
                    fsouth(); 
                    break;
                case "east":
                    feast();
                    break;
                case "north":
                    fnorth();
                    break;
            }

        }
        static void fnorth()
        {
            //Trevor
            Console.WriteLine("A horde of Zombie swarms you.. Runnn!!");
            Console.WriteLine("You Died while being alive and torn into CHUNKS of Meat");
            Console.ReadLine();
            Forest();
        }
        static void fsouth()
        {
            //ZZZZzzzzzzz
            //Mustafa
            Console.WriteLine("You see a an old bridge and you start passing through it.");
            Console.WriteLine("Player: What's the sound..?");
            Console.WriteLine("creeek");
            Console.WriteLine("The bridge starts falling apart");
            Console.WriteLine("GAME OVER!!");
            Console.ReadLine();
            Forest();
        }
        static void feast()
        {
            //Samuel
            Console.WriteLine("You have found a Non-Functioning Radio");
            Console.WriteLine("What's Next? >");
            Console.WriteLine("Go Back?");
            string feastinput = Console.ReadLine();
            if (feastinput == "Go Back")
            {
                Forest();
            }
            else
            {
                Console.WriteLine("\nThe path you seek does not exist in this forsaken place. Choose wisely, for each misstep might be your last. Dare to try again, and may the shadows guide you.");
            }
        }
        //Instructions method by Sam B
        static string Instructions(string inst)
        {
            inst = @"INSTRUCTIONS:
             You will be stationed within the haunted village, known as Dubravica
             located in the European country of Croatia.When exploring through
             the haunted village, stay alert and avoid zombies by staying out of sight
             and moving quietly.Explore different areas of the village and search for
             supplies such as food, medical kits etc.Your final objective is to locate
             the final evacuation point, your final score will be based on time taken,
             resources gathered and zombies defeated.
             Good luck, and stay safe out there!. 
             (If you get stuck, type 'help' or '?' for some general hints)

             Press enter to continue....";
            return inst;
        }
        //Church -Mustafa (Made a bit story here :) ) 
        static void Church()
        {
            int key = 0;
            Console.WriteLine("You are heading inside the church..");
            Console.WriteLine("You open the door.. Creeeekk");
            Console.WriteLine("Your find 2 Survivors that were recently infected");
            string Talking = "Survivor 1 : Please Don't Kill us\n" +
                             "Survivor 2 : It's a survivor\n" +
                             "Survivor 1 : Argh!! DAMN IT!\n" +
                             "Player : What happened?\n" +
                             "Survivor 2 : You have to find the point of Evacuation and return back to save us..\n" +
                             "\t We found a hint but we couldn't solve it and before we knew it, it was too late.\n" +
                             "\t The hint is Within these walls, a haven waits, Where safety beckons, beyond the gates.\n" +
                             "\t to find ... what was after it?\n" +
                             "Survivor 1 : Within these walls, a haven waits, Where safety beckons, beyond the gates.\n" +
                             "To find salvation, heed this call, In the heart of governance, stands tall." +
                             "Survivor 2: And we found this key.." +
                             "Player : I'll try..";
            PrintOneByOne2(Talking);
            key = key + 1;
            Console.ReadLine();
        }
        static void PrintOneByOne2(string text)
        {
            //Mustafa - Making the characters one by one for the survivor and the player talking for the hint of evac
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(05);
            }
            Console.ReadLine();
        }
        //The Town hall will be completed once the reference method is done - Mustafa
        /* static void Townhall()
         {
             int key; 
            if (key == 1)
             {
                 Console.WriteLine("You have unlocked the door");
             }
             else
             {
                 Console.WriteLine("You do not meet the requirement.");
                 Console.WriteLine("You are not qualified yet");
             }
             Console.ReadLine();
         }
        */
    }
}
