using System;

namespace RockPaperScissorsV2.BLL
{
    public class RockPaperScissorsGame
    {
        private IPlayer _player1;
        private IPlayer _player2;
        //Keep track of wins here.
        public int Player1Wins { get; private set; }
        public int Player2Wins { get; private set; }

        public RockPaperScissorsGame(IPlayer player1, IPlayer player2)
        {
            _player1 = player1;
            _player2 = player2;
        }

        public Outcome Play()
        {
            Weapon player1Weapon = _player1.GetWeapon();
            Weapon player2Weapon = _player2.GetWeapon();

            //Find out the outcome of the game.
            Outcome outcome;

            if (player1Weapon == player2Weapon)
            {
                outcome = Outcome.Draw;
            }
            else if (player1Weapon == Weapon.Spock)
            {
                outcome = Outcome.Spock;
            }
            else if (player2Weapon == Weapon.Spock)
            {
                outcome = Outcome.Spock;

            }
            else if (player1Weapon == Weapon.Rock && player2Weapon == Weapon.Scissors
                || player1Weapon == Weapon.Scissors && player2Weapon == Weapon.Paper
                || player1Weapon == Weapon.Paper && player2Weapon == Weapon.Rock)
            {
                outcome = Outcome.Player1Wins;
            }
            else
            {
                outcome = Outcome.Player2Wins;
            }
            return outcome;
        }
    }
}
