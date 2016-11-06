using RockPaperScissorsV2.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissorsV2.CLI
{
    public class ComputerPlayer : IPlayer
    {
        static Random compInput = new Random();
        private Weapon player2Weapon;

        public Weapon GetWeapon()
        {
            int player2Input = compInput.Next(0, 4);

            switch (player2Input)
            {
                case 1:
                    player2Weapon = Weapon.Rock;
                    break;
                case 2:
                    player2Weapon = Weapon.Paper;
                    break;
                case 3:
                    player2Weapon = Weapon.Scissors;
                    break;
                default:
                    player2Weapon = Weapon.Rock;
                    break;
            }
            return player2Weapon;
        }
    }
}
