using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.GameLogic;

namespace BattleShip.BLL
{
    // Establish players and boards.
    public class Players
    {
        private Board _board = new Board();

        public string Name { get; set; }

        public Board Surface { get { return _board; } }
    }
}