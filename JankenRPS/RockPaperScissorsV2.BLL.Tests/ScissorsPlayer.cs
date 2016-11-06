using System;

namespace RockPaperScissorsV2.BLL.Tests
{
    internal class ScissorsPlayer : IPlayer
    {
        public Weapon GetWeapon()
        {
            return Weapon.Scissors;
        }
    }
}