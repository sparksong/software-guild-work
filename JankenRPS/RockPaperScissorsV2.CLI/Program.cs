using RockPaperScissorsV2.BLL;
using System;

namespace RockPaperScissorsV2.CLI
{
    class Program
    {
        public int p1Wins = 0;
        public int p2Wins = 0;

        static void Main(string[] args)
        {
            IPlayer player1 = PlayerFactory.GetHuman();
            IPlayer player2 = PlayerFactory.GetComputer();
            bool gameOver = false;
            bool validPlayers = false;
            bool keepPlaying = true;
            int p1Wins = 0;
            int p2Wins = 0;

            Console.Title = "Rock, Paper, Scissors!";
            Console.WriteLine("Let's do janken! (Rock Paper Scissors)");
            Console.WriteLine("How many players do you want?");
            Console.WriteLine("0 = Computer vs. Computer.");
            Console.WriteLine("1 = Player vs. Computer.");
            Console.WriteLine("2 = Player vs. Player.");
            
            do
            {
                string numberOfPlayers = Console.ReadLine();

                if (numberOfPlayers == "0")
                {
                    player1 = PlayerFactory.GetComputer();
                    player2 = PlayerFactory.GetComputer();
                    validPlayers = true;
                }
                else if (numberOfPlayers == "1")
                {
                    player1 = PlayerFactory.GetHuman();
                    player2 = PlayerFactory.GetComputer();
                    validPlayers = true;
                }
                else if (numberOfPlayers == "2")
                {
                    player1 = PlayerFactory.GetHuman();
                    player2 = PlayerFactory.GetHuman();
                    validPlayers = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a valid number 0, 1, or 2!");
                    validPlayers = false;
                }
            }
            while (!validPlayers);
            Console.Clear();
            //Move within factory ^^
            RockPaperScissorsGame newGame = new RockPaperScissorsGame(player1, player2);

            do
            {
                do
                {
                    Outcome outcome = newGame.Play();

                    switch (outcome)
                    {
                        case Outcome.Spock:
                            Console.WriteLine("You knew about Spock? Sneaky sneaky. Spock always wins no matter what! (Only real wins count though!)");
                            gameOver = true;
                            break;
                        case Outcome.Player1Wins:
                            Console.WriteLine("Player 1, it looks like you got lucky and won. Congrats I guess.");
                            Console.WriteLine("Player 2 is a losey loser who loses.");
                            p1Wins++;
                            gameOver = true;
                            break;
                        case Outcome.Player2Wins:
                            Console.WriteLine("Player 2 is the winner this time. Do you feel special now?");
                            Console.WriteLine("Sucks to suck player 1, you lose.");
                            p2Wins++;
                            gameOver = true;
                            break;
                        case Outcome.Draw:
                            Console.Clear();
                            Console.WriteLine("Both players chose the same thing.. Press any key and try again.");
                            Console.ReadKey();
                            gameOver = false;
                            break;
                        default:
                            gameOver = false;
                            break;
                    }
                }
                while (!gameOver);

                //Play again loop.
                bool validResponse = false;
                do
                {
                    Console.Title = "Player One Wins: " + p1Wins + " Player Two Wins: " + p2Wins;
                    Console.WriteLine();
                    Console.WriteLine("Would you like to play again? Y / N");
                    string playAgain = Console.ReadLine();

                    if (keepPlaying = playAgain.ToUpper() == "Y")
                    {
                        Console.Clear();
                        keepPlaying = true;
                        validResponse = true;
                    }
                    else if (keepPlaying = playAgain.ToUpper() == "N")
                    {
                        Console.WriteLine("Come again anytime! Press any key to quit!");
                        Console.ReadKey();
                        keepPlaying = false;
                        validResponse = true;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Y or N only!");
                        validResponse = false;
                    }
                }
                while (!validResponse);
            }
            while (keepPlaying);
        }
    }
}