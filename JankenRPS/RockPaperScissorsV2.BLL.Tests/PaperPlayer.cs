using System;

namespace RockPaperScissorsV2.BLL.Tests
{
    internal class PaperPlayer : IPlayer
    {
        public Weapon GetWeapon()
        {
            return Weapon.Paper;
        }
    }
}