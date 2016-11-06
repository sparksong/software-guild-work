using RockPaperScissorsV2.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissorsV2.CLI
{
    public static class PlayerFactory
    {
        public static IPlayer GetHuman()
        {
            return new HumanPlayer();
        }

        public static IPlayer GetComputer()
        {
            return new ComputerPlayer();
        }
    }
}
