using System;
using System.Collections.Generic;

using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;


namespace Pick_My_Look
{
    class Program
    {
        private static bool MainMenu()
        {
            Console.WriteLine("Choose an option");
            Console.WriteLine("1) Pick my look");
            Console.WriteLine("2) Pick a random look");
            Console.WriteLine("3) Add new make up");
            Console.WriteLine("4) Remove makeup");
            Console.WriteLine("5) Exit");

            String uInput;
            switch (Console.ReadLine())
            {
                case "1":
                    PickLook();
                    Console.WriteLine("Would you like to return to the main menu? (y/n)");
                    uInput = Console.ReadLine().ToLower().Trim();
                    while (uInput != "y" && uInput != "n")
                    {
                        Console.WriteLine("Please enter either y or n");
                        uInput = Console.ReadLine().ToLower().Trim();
                    }
                    if (uInput == "y")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    
                case "2":
                    PickRandom();
                    Console.WriteLine("Would you like to return to the main menu? (y/n)");
                    uInput = Console.ReadLine().ToLower().Trim();
                    while (uInput != "y" && uInput != "n")
                    {
                        Console.WriteLine("Please enter either y or n");
                        uInput = Console.ReadLine().ToLower().Trim();
                    }
                    if (uInput == "y")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "3":
                    AddMakeup();
                    return true;
                case "4":
                    RemoveMakeup();
                    return true;
                case "5":
                    return false;
                default:
                    return true;
            }
        }
        private static void OpenConnection()
        {
            List<string> color = new List<string>();
            color.Add("red");
            color.Add("white");

            string provider = ConfigurationManager.AppSettings["provider"];

            string connectionString = ConfigurationManager.AppSettings
                ["connectionString"];

            DbProviderFactory factory = DbProviderFactories.GetFactory(provider);

            using (DbConnection connection = factory.CreateConnection())
            {
                if (connection == null)
                {
                    Console.WriteLine("Connection Error");
                    Console.ReadLine();
                    return;
                }

                connection.ConnectionString = connectionString;

                connection.Open();

                DbCommand command = factory.CreateCommand();

                if (command == null)
                {
                    Console.WriteLine("Command Error");
                    Console.ReadLine();
                    return;
                }

                command.Connection = connection;

                command.CommandText = ("SELECT * FROM EyeShadow WHERE Color = red ORDER BY Layer ");

                using (DbDataReader dataReader = command.ExecuteReader())
                {
                    // Advance to the next results
                    while (dataReader.Read())
                    {
                        // Output results using row names
                        Console.WriteLine($"{dataReader["PaletteName"]} " +
                            $"{dataReader["ShadeName"]} " + $"{dataReader["Color"]} " + $"{dataReader["Layer"]} ");
                    }
                }
            }       
        }

        

        private static void PickLook()
        {
            string colorChoice;
            List<string> AllColors = new List<string>() { "yellow", "green", "blue", "purple", "red", "pink", "orange", "black", "copper", "gold", "silver", "brown", "grey", "white" };
            List<string> ComplimentaryColors = new List<string>();
            

            Console.WriteLine("Enter a color (red, green, blue, etc)");

            colorChoice = Console.ReadLine();
            colorChoice = colorChoice.ToLower().Trim();

            if (colorChoice == "yellow")
            {
                ComplimentaryColors.Add("orange");
                ComplimentaryColors.Add("red");
                ComplimentaryColors.Add("yellow");
                LookSearch(ComplimentaryColors);
            }
            else if (colorChoice == "green")
            {
                Console.WriteLine(colorChoice);
            }
            else if (colorChoice == "blue")
            {
                Console.WriteLine(colorChoice);
            }
            else if (colorChoice == "purple")
            {
                Console.WriteLine(colorChoice);
            }
            else if (colorChoice == "red")
            {
                Console.WriteLine(colorChoice);
                OpenConnection();
            }
            else if (colorChoice == "pink")
            {
                Console.WriteLine(colorChoice);
            }
            else if (colorChoice == "orange")
            {
                Console.WriteLine(colorChoice);
            }
            else if (colorChoice == "black")
            {
                Console.WriteLine(colorChoice);
            }
            else if (colorChoice == "copper")
            {
                Console.WriteLine(colorChoice);
            }
            else if (colorChoice == "gold")
            {
                Console.WriteLine(colorChoice);
            }
            else if (colorChoice == "silver")
            {
                Console.WriteLine(colorChoice);
            }
            else if (colorChoice == "brown")
            {
                Console.WriteLine(colorChoice);
            }
            else if (colorChoice == "grey")
            {
                Console.WriteLine(colorChoice);
            }
            else if (colorChoice == "white")
            {
                Console.WriteLine(colorChoice);
            }
            else
            {
                Console.WriteLine("Error. No color matches found");
            }
        }

        private static void LookSearch(List<string> colors)
        {
            Random rnd = new Random();
            int max = colors.Count;
            int color = rnd.Next(0, max);
            int j = 1;

            string UInput = "y";
            string colorChoice = "nope";
            string Highlighter = "nope";
            string Blush = "nope";
            string Lipstick = "nope";
            List<string> Layer5 = new List<string>();

            Console.WriteLine();
            Console.WriteLine("Would you like a 3 layer look or a 5 layer look?(3/5)");

            string Layers = Console.ReadLine();
            int Layer = Convert.ToInt32(Layers);

            while(Layer != 3 && Layer != 5)
            {
                Console.WriteLine("Please enter either 3 or 5");
                Layers = Console.ReadLine();
                Layer = Convert.ToInt32(Layers);
            }
            for(int i = 1; i < Layer + 1; i++)
            {
                color = rnd.Next(0, max);
                colorChoice = colors[color];

                Console.Write("Layer: {0} ", j);
                Console.Write(" Palette Name: ");
                Console.Write(" Shade Name: ");
                Console.WriteLine(" Color: {0}", colorChoice);

                Layer5.Add(colorChoice);
                j++;

                if(Layer == 3)
                {
                    if(i == 2)
                    {
                        //if no color of blush using colorchoice available set pink
                        Blush = colorChoice;
                        Lipstick = colorChoice;
                    }
                    if(i == 3)
                    {
                        Highlighter = colorChoice;
                    }
                }
                if(Layer == 5)
                {
                    if (i == 3)
                    {
                        //if no color of blush using colorchoice available set pink
                        Blush = colorChoice;
                        Lipstick = colorChoice;
                    }
                    if (i == 5)
                    {
                        Highlighter = colorChoice;
                    }
                }
            }
            
            Console.WriteLine();
            BlushSearch(Blush);
            LipstickSearch(Lipstick);
            HighlighterSearch(Highlighter);

            while (UInput != "n")
            {
                Console.WriteLine("Would you like to change any?(y/n)");
                UInput = Console.ReadLine().ToLower().Trim();
                while (UInput != "y" && UInput != "n")
                {
                    Console.WriteLine("Please enter either y or n");
                    UInput = Console.ReadLine().ToLower().Trim();
                }
                if (UInput == "y")
                {
                    ChangeLook(Layer5, colors, Blush, Highlighter, Lipstick);
                    
                }
            }
            

        }
        private static void ChangeLook(List<string> Layers, List<string> colors, string Blush, string Highlighter, string Lipstick)
        {
            Random rnd = new Random();
            int max = colors.Count; 
            int color = rnd.Next(0, max);
            int j = 0;
            int MenuChoice = 0;

            string colorChoice = "nope";
            string HighlighterChoice = "nope";
            string BlushChoice = "nope";
            string LipstickChoice = "nope";

            Console.WriteLine();
            Console.WriteLine(" Which would you like to change? 1 = Eyeshadow  2 = Blush  3 = Highlighter  4 = Lipstick  5 = none");
            MenuChoice = int.Parse(Console.ReadLine());
            while (MenuChoice != 5)
            {
                switch (MenuChoice)
                {
                    case 1:
                        Console.WriteLine();
                        Console.WriteLine("Which Layer?");
                        j = int.Parse(Console.ReadLine());

                        Console.WriteLine("Would you like a new shade or a new color of Eyeshadow?");
                        string UInput = Console.ReadLine().ToLower().Trim();
                        while (UInput != "shade" && UInput != "color")
                        {
                            Console.WriteLine("Options are color or shade");
                            UInput = Console.ReadLine().ToLower().Trim();
                        }
                        if (UInput == "shade")
                        {
                            colorChoice = (Layers[j - 1] + "2");
                            Console.WriteLine();
                            Console.Write("Layer: {0} ", j);
                            Console.Write(" Palette Name: ");
                            Console.Write(" Shade Name: ");
                            Console.WriteLine(" Color: {0}", colorChoice);
                            Console.WriteLine();
                            Layers[j - 1] = colorChoice;
                        }
                        if (UInput == "color")
                        {
                            color = rnd.Next(0, max);
                            colorChoice = colors[color];

                            while (colorChoice == Layers[j - 1])
                            {
                                color = rnd.Next(0, max);
                                colorChoice = colors[color];
                            }


                            Console.Write("Layer: {0} ", j);
                            Console.Write(" Palette Name: ");
                            Console.Write(" Shade Name: ");
                            Console.WriteLine(" Color: {0}", colorChoice);
                            Console.WriteLine();
                            Layers[j - 1] = colorChoice;
                        }
                        return;
                    case 2:
                        Console.WriteLine("Would you like a new shade or a new color of Blush?");
                        UInput = Console.ReadLine().ToLower().Trim();
                        while (UInput != "shade" && UInput != "color")
                        {
                            Console.WriteLine("Options are color or shade");
                            UInput = Console.ReadLine().ToLower().Trim();
                        }
                        if (UInput == "shade") {
                            BlushChoice = (Blush + "2");
                            Console.WriteLine();
                            Console.WriteLine("Name: Blush Color: {0}", BlushChoice);
                            Console.WriteLine();
                        }
                        if (UInput == "color")
                        {
                            color = rnd.Next(0, max);
                            BlushChoice = colors[color];

                            while (BlushChoice == Blush)
                            {
                                color = rnd.Next(0, max);
                                BlushChoice = colors[color];
                            }

                            Console.WriteLine();
                            Console.WriteLine("Name: Blush Color: {0}", BlushChoice);
                            Console.WriteLine();
                        }
                        return;
                    case 3:
                        Console.WriteLine("Would you like a new shade or a new color of Highlighter?");
                        UInput = Console.ReadLine().ToLower().Trim();
                        while (UInput != "shade" && UInput != "color")
                        {
                            Console.WriteLine("Options are color or shade");
                            UInput = Console.ReadLine().ToLower().Trim();
                        }
                        if (UInput == "shade")
                        {
                            HighlighterChoice = (Highlighter + "2");
                            Console.WriteLine();
                            Console.WriteLine("Name: Highlighter Color: {0}", HighlighterChoice);
                            Console.WriteLine();
                        }
                        if (UInput == "color")
                        {
                            color = rnd.Next(0, max);
                            HighlighterChoice = colors[color];

                            while (HighlighterChoice == Highlighter)
                            {
                                color = rnd.Next(0, max);
                                HighlighterChoice = colors[color];
                            }

                            Console.WriteLine();
                            Console.WriteLine("Name: Highlighter Color: {0}", HighlighterChoice);
                            Console.WriteLine();
                        }
                        return;
                    case 4:
                        Console.WriteLine("Would you like a new shade or a new color of Lipstick?");
                        UInput = Console.ReadLine().ToLower().Trim();
                        while (UInput != "shade" && UInput != "color")
                        {
                            Console.WriteLine("Options are color or shade");
                            UInput = Console.ReadLine().ToLower().Trim();
                        }
                        if (UInput == "shade")
                        {
                            LipstickChoice = (Lipstick + "2");
                            Console.WriteLine();
                            Console.WriteLine("Name: Lipstick Color: {0}", LipstickChoice);
                            Console.WriteLine();
                        }
                        if (UInput == "color")
                        {
                            color = rnd.Next(0, max);
                            LipstickChoice = colors[color];

                            while (LipstickChoice == Lipstick)
                            {
                                color = rnd.Next(0, max);
                                LipstickChoice = colors[color];
                            }

                            Console.WriteLine();
                            Console.WriteLine("Name: Lipstick Color: {0}", LipstickChoice);
                            Console.WriteLine();
                        }
                        return;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Please enter 1-5(5 exits)");
                        return;
                }
            }
        }
        private static void PickRandom()
        {
            Random rnd = new Random();
            int color = rnd.Next(1, 15);
            string colorChoice;

            Console.WriteLine("Random!");

            switch (color)
            {
                case 1:
                    Console.WriteLine("yellow");
                    colorChoice = ("yellow");
                    return;
                case 2:
                    Console.WriteLine("green");
                    colorChoice = ("green");
                    return;
                case 3:
                    Console.WriteLine("blue");
                    colorChoice = ("blue");
                    return;
                case 4:
                    Console.WriteLine("purple");
                    colorChoice = ("purple");
                    return;
                case 5:
                    Console.WriteLine("red");
                    colorChoice = ("red");
                    return;
                case 6:
                    Console.WriteLine("pink");
                    colorChoice = ("pink");
                    return;
                case 7:
                    Console.WriteLine("orange");
                    colorChoice = ("orange");
                    return;
                case 8:
                    Console.WriteLine("black");
                    colorChoice = ("black");
                    return;
                case 9:
                    Console.WriteLine("copper");
                    colorChoice = ("copper");
                    return;
                case 10:
                    Console.WriteLine("gold");
                    colorChoice = ("gold");
                    return;
                case 11:
                    Console.WriteLine("silver");
                    colorChoice = ("silver");
                    return;
                case 12:
                    Console.WriteLine("brown");
                    colorChoice = ("brown");
                    return;
                case 13:
                    Console.WriteLine("grey");
                    colorChoice = ("grey");
                    return;
                case 14:
                    Console.WriteLine("white");
                    colorChoice = ("white");
                    return;
                default:
                    Console.WriteLine("Error");
                    return;

            }

        }

        private static void AddMakeup()
        {
            Console.WriteLine("Makeup added");
        }

        private static void RemoveMakeup()
        {
            Console.WriteLine("Makeup removed");
        }
        private static void BlushSearch(string BlushColor)
        {
            Console.WriteLine("Name: Blush Color: {0}", BlushColor);
            Console.WriteLine();
        }
        private static void LipstickSearch(string LipstickColor)
        {
            Console.WriteLine("Name: Lipstick Color: {0}", LipstickColor);
            Console.WriteLine();
        }
        private static void HighlighterSearch(string HighlighterColor)
        {
            Console.WriteLine("Name: Highlighter Color: {0}", HighlighterColor);
            Console.WriteLine();
        }

        public static void DBRegister()
        {
            DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
        }

        static void Main(string[] args)
        {

            DBRegister();
                
            

            Console.WriteLine("Welcome to Pick my look!");
            Console.WriteLine();
            bool menu = true;
            while (menu)
            {
                menu = MainMenu();
            }



        }
    }
}
