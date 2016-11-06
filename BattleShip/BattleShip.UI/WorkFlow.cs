using System;
using System.Linq;
using BattleShip.BLL;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;

namespace BattleShip.UI
{
    internal class WorkFlow
    {
        private Players player1 = new Players();
        private Players player2 = new Players();
        private bool player1Turn = true;
        private bool shootShips = false;
        private bool victory = false;

        public void Start()
        {
            //Changes the title in the console.
            Console.Title = "BATTLESHIP";

            //Greet
            Console.WriteLine("Welcome to Battleship!");

            //Start Game! Get names + Validate names are not empty.
            if (PlayGame() == true)
            {
                Console.Clear();
                Console.WriteLine("Well then, what is your name player one?");
                do
                {
                    player1.Name = Console.ReadLine();
                    Console.Clear();
                    if (!string.IsNullOrEmpty(player1.Name) &&
                        !string.IsNullOrWhiteSpace(player1.Name))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a name!");
                    }
                }
                while (string.IsNullOrEmpty(player1.Name) ||
                string.IsNullOrWhiteSpace(player1.Name));

                Console.Clear();

                Console.WriteLine("And what is your name, player two?");
                do
                {
                    player2.Name = Console.ReadLine();
                    Console.Clear();
                    if (!string.IsNullOrEmpty(player2.Name) &&
                        !string.IsNullOrWhiteSpace(player2.Name))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a name!");
                    }
                }
                while (string.IsNullOrEmpty(player2.Name) ||
                string.IsNullOrWhiteSpace(player2.Name));

                Console.WriteLine("Thank you Captain {0} and Comrade {1}, prepare to battle the ships.", player1.Name, player2.Name);
                Console.ReadKey();

                Console.Clear();
                Console.WriteLine("Comrade {1}, please go away while Captain {0} prepares their ships. Press any key to continue.", player1.Name, player2.Name);
                Console.ReadKey();

                Setup(player1.Surface);
                Console.WriteLine("Now, Captain {0}, go away while Comrade {1} prepares. Press any key to continue.", player1.Name, player2.Name);
                Console.ReadKey();

                Setup(player2.Surface);
                Console.WriteLine("{0}, you will begin! Press any key to continue.", player1.Name);
                Console.ReadKey();

                GameStart();
                PlayAgain();
            }
        }

        //Finally playing the game!
        public void GameStart()
        {
            do
            {
                if (player1Turn == true)
                {
                    do
                    {
                        Console.Clear();
                        Console.Title = "Captain " + player1.Name + "'s turn!";
                        ShowShotBoard(player2.Surface);
                        Console.WriteLine("{0}, declare a coordinate for your attack!", player1.Name);
                        Coordinate shotCoor = GetCoordinates();
                        FireShot(player2.Surface, shotCoor);
                        player1Turn = false;
                    }
                    while (!shootShips);
                }
                else
                {
                    do
                    {
                        Console.Clear();
                        Console.Title = "Comrade " + player2.Name + "'s turn!";
                        ShowShotBoard(player1.Surface);
                        Console.WriteLine("{0}, declare a coordinate for your attack!", player2.Name);
                        Coordinate shotCoor = GetCoordinates();
                        FireShot(player1.Surface, shotCoor);
                        player1Turn = true;
                    }
                    while (!shootShips);
                }
            }
            while (victory == false);

            if (player1Turn == true)
            {
                Console.WriteLine("Congratulations {0}, you are the champion!", player1.Name);
            }
            else
            {
                Console.WriteLine("Congratulations {0}, you are the champion!", player2.Name);
            }
        }

        // Direction Validation
        public ShipDirection GetDirection()
        {
            string userInput = string.Empty;
            bool isValid = false;
            ShipDirection direction = new ShipDirection();

            do
            {
                isValid = true;
                userInput = Console.ReadLine();
                switch (userInput.ToUpper())
                {
                    case "UP":
                        direction = ShipDirection.Up;
                        break;
                    case "DOWN":
                        direction = ShipDirection.Down;
                        break;
                    case "RIGHT":
                        direction = ShipDirection.Right;
                        break;
                    case "LEFT":
                        direction = ShipDirection.Left;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("That isn't a valid direction! (Up, Down, Left, Right)");
                        isValid = false;
                        continue;
                }
            }
            while (!isValid);
            return direction;
        }

        //XY Coordinates Validation!
        public Coordinate GetCoordinates()
        {
            string userInput = string.Empty;
            int xCoordinate = 0;
            int yCoordinate = 0;
            bool isValid = false;
            Translator translate = new Translator();

            do
            {
                isValid = true;
                userInput = Console.ReadLine();

                if (!(userInput.Length == 2) && !(userInput.Length == 3))
                {
                    Console.Clear();
                    Console.WriteLine("Please enter VALID coordinates! Try again!");
                    isValid = false;
                    continue;
                }
                if (!(char.IsLetter(userInput[0])))
                {
                    Console.Clear();
                    Console.WriteLine("Your coordinates must start with a LETTER! Try again!");
                    isValid = false;
                    continue;
                }
                xCoordinate = translate.ToNumbersXY(userInput.Substring(0, 1));
                if (xCoordinate == -1)
                {
                    Console.Clear();
                    Console.WriteLine("Letter must be A - J! Try again!");
                    isValid = false;
                    continue;
                }
                if (!int.TryParse(userInput.Substring(1), out yCoordinate))
                {
                    Console.Clear();
                    Console.WriteLine("Coordinate must end in a number!");
                    isValid = false;
                    continue;
                }
                if (!(yCoordinate >= 1 && yCoordinate <= 10))
                {
                    Console.Clear();
                    Console.WriteLine("Number must be between 1 and 10!");
                    isValid = false;
                    continue;
                }
            }
            while (!isValid);
            return new Coordinate(yCoordinate, xCoordinate);
        }

        //Check shots if they hit a ship.
        public ShotStatus FireShot(Board b, Coordinate shotCoor)
        {
            FireShotResponse response = b.FireShot(shotCoor);

            if (response.ShotStatus == ShotStatus.Invalid)
            {
                Console.Clear();
                shootShips = false;
                ShowShotBoard(b);
                Console.WriteLine("Invalid location! Try again!");
                Console.ReadKey();
            }
            if (response.ShotStatus == ShotStatus.Duplicate)
            {
                Console.Clear();
                shootShips = false;
                ShowShotBoard(b);
                Console.WriteLine("That location has already been shot at! Try again!");
                Console.ReadKey();
            }
            if (response.ShotStatus == ShotStatus.Hit)
            {
                Console.Clear();
                shootShips = true;
                ShowShotBoard(b);
                Console.WriteLine("You hit something!");
                Console.ReadKey();
            }
            if (response.ShotStatus == ShotStatus.HitAndSunk)
            {
                Console.Clear();
                shootShips = true;
                ShowShotBoard(b);
                Console.WriteLine("You sank your opponent's {0}", response.ShipImpacted);
                Console.ReadKey();
            }
            if (response.ShotStatus == ShotStatus.Miss)
            {
                Console.Clear();
                shootShips = true;
                ShowShotBoard(b);
                Console.WriteLine("Your projectile splashes into the ocean, you missed!");
                Console.ReadKey();
            }
            if (response.ShotStatus == ShotStatus.Victory)
            {
                Console.Clear();
                shootShips = true;
                ShowShotBoard(b);
                Console.WriteLine("You have sunk all your opponent's ships, you win!");
                Console.ReadKey();
                victory = true;
            }
            return response.ShotStatus;
        }

        // Board layout
        private void ShowBoard(Board board)
        {
            Console.WriteLine();
            Console.Write("    A  B  C  D  E  F  G  H  I  J ");
            for (int y = 1; y <= 10; y++)
            {
                if (y < 10)
                {
                    Console.Write($"\n{y}  ");
                }
                else
                {
                    Console.Write($"\n{y} ");
                }
                for (int x = 0; x <= 9; x++)
                {
                    Coordinate c = new Coordinate(y, x + 1);
                    if (CheckShipsExistance(c, board) == true)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write("(");
                        Console.Write("X");
                        Console.Write(")");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write("(");
                        Console.Write("_");
                        Console.Write(")");
                    }
                }
            }
            Console.WriteLine();
        }

        private bool CheckShipsExistance(Coordinate coordinate, Board b)
        {
            foreach (Ship ship in b.Ships)
            {
                if (ship != null)
                {
                    if (ship.BoardPositions.Contains(coordinate))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // Board layouts
        private void ShowShotBoard(Board board)
        {
            Console.WriteLine();
            Console.Write("    A  B  C  D  E  F  G  H  I  J ");
            for (int y = 1; y <= 10; y++)
            {
                if (y < 10)
                {
                    Console.Write($"\n{y}  ");
                }
                else
                {
                    Console.Write($"\n{y} ");
                }
                for (int x = 1; x <= 10; x++)
                {
                    Coordinate shotCoor = new Coordinate(y, x);
                    ShotHistory shotExist = ShotHistory.Unknown;
                    if (board.ShotHistory.ContainsKey(shotCoor))
                    {
                        shotExist = board.ShotHistory[shotCoor];
                    }

                    if (shotExist == ShotHistory.Hit)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("(");
                        Console.Write("H");
                        Console.Write(")");
                        Console.ResetColor();
                    }
                    else if (shotExist == ShotHistory.Miss)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("(");
                        Console.Write("M");
                        Console.Write(")");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write("(");
                        Console.Write("_");
                        Console.Write(")");
                    }
                }
            }
            Console.WriteLine();
        }

        //Setup ships Loop - ok, notenoughspace, overlap
        private void SetupShips(ShipType s, Board b)
        {
            ShipPlacement placed;
            do
            {
                ShowBoard(b);
                Console.WriteLine("\nPlease enter a coordinate to place your {0} (ex. A1, C6, J10)", s);
                PlaceShipRequest request = new PlaceShipRequest();
                request.Coordinate = GetCoordinates();
                Console.WriteLine("Please enter a direction! (Up, down, left, right).");
                request.Direction = GetDirection();
                request.ShipType = s;

                placed = b.PlaceShip(request);

                switch (placed)
                {
                    case (ShipPlacement.NotEnoughSpace):
                        Console.WriteLine("There is not enough space to place the ship! Press any key to try again");
                        Console.ReadKey();
                        ShowBoard(b);
                        Console.Clear();
                        break;
                    case (ShipPlacement.Overlap):
                        Console.WriteLine("Your ship overlaps another! Press any key to try again(are you even paying attention?)");
                        Console.ReadKey();
                        ShowBoard(b);
                        Console.Clear();
                        break;
                    default:
                        break;
                }
            }
            while (placed != ShipPlacement.Ok);
            Console.Clear();
        }

        //Go through the ship types
        private void Setup(Board b)
        {
            SetupShips(ShipType.Carrier, b);
            SetupShips(ShipType.Battleship, b);
            SetupShips(ShipType.Cruiser, b);
            SetupShips(ShipType.Submarine, b);
            SetupShips(ShipType.Destroyer, b);
        }

        //Play? Exit?
        private bool PlayGame()
        {
            Console.WriteLine("Do you want to play the game? Y / N");
            while (true)
            {
                string userInput = Console.ReadLine();

                if (userInput.ToUpper() == "Y")
                {
                    return true;
                }
                else if (userInput.ToUpper() == "N")
                {
                    Console.Clear();
                    Console.WriteLine("Then don't waste my time! Scram!");
                    Console.ReadKey();
                    return false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Please enter Y or N only!");
                }
            }
        }

        //Play Again? Exit?
        private bool PlayAgain()
        {
            Console.WriteLine("Would you like to play again? Y / N");
            while (true)
            {
                string userInput = Console.ReadLine();

                if (userInput.ToUpper() == "Y")
                {
                    Console.Clear();
                }
                else if (userInput.ToUpper() == "N")
                {
                    Console.Clear();
                    Console.WriteLine("Until next time!");
                    Console.ReadKey();
                    return false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Please enter Y or N only!");
                }
            }
        }
    }
}