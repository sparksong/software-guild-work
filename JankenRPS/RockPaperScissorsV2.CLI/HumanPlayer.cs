using RockPaperScissorsV2.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissorsV2.CLI
{
    public class HumanPlayer : IPlayer
    {
        private Weapon player1Weapon;

        public Weapon GetWeapon()
        {
            bool valid = false;

            do
            {
                Console.Clear();
                Console.WriteLine("Enter a number, the first letter, or the word to shoot!");
                Console.WriteLine("1, R, Rock");
                Console.WriteLine("2, P, Paper");
                Console.WriteLine("3, S, Scissors");
            
                string playerInput = Console.ReadLine();

                switch (playerInput.ToUpper())
                {
                    case "ROCK":
                    case "R":
                    case "1":
                        player1Weapon = Weapon.Rock;
                        valid = true;
                        break;
                    case "PAPER":
                    case "P":
                    case "2":
                        player1Weapon = Weapon.Paper;
                        valid = true;
                        break;
                    case "SCISSORS":
                    case "S":
                    case "3":
                        player1Weapon = Weapon.Scissors;
                        valid = true;
                        break;
                    case "SPOCK":
                        player1Weapon = Weapon.Spock;
                        valid = true;
                        break;
                    default:
                        player1Weapon = Weapon.Invalid;
                        Console.WriteLine("You must choose one the the options! Rock, Paper, or Scissors! Dummy.");
                        Console.ReadKey();
                        valid = false;
                        break;
                }
            } while (!valid);
            return player1Weapon;
        }
    }
}
