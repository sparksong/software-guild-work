using System;

namespace RockPaperScissorsV2.BLL.Tests
{
    internal class RockPlayer : IPlayer
    {
        public Weapon GetWeapon()
        {
            return Weapon.Rock;
        }
    }
}