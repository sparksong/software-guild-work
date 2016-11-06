using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using RockPaperScissorsV2.BLL;

namespace RockPaperScissorsV2.BLL.Tests
{
	[TestFixture]
    public class RockPaperScissorsGameTests
    {
        [Test]
        public void RockAlwaysBeatsScissors()
        {
            IPlayer player1 = new RockPlayer();
            IPlayer player2 = new ScissorsPlayer();

            RockPaperScissorsGame game = new RockPaperScissorsGame(player1, player2);

            Outcome result = game.Play();

            Assert.AreEqual(Outcome.Player1Wins, result);
        }

        [Test]
        public void PaperAlwaysBeatsRock()
        {
            IPlayer player1 = new PaperPlayer();
            IPlayer player2 = new RockPlayer();

            RockPaperScissorsGame game = new RockPaperScissorsGame(player1, player2);

            Outcome result = game.Play();

            Assert.AreEqual(Outcome.Player1Wins, result);
        }

        [Test]
        public void ScissorsAlwaysBeatsPaper()
        {
            IPlayer player1 = new ScissorsPlayer();
            IPlayer player2 = new PaperPlayer();

            RockPaperScissorsGame game = new RockPaperScissorsGame(player1, player2);

            Outcome result = game.Play();

            Assert.AreEqual(Outcome.Player1Wins, result);
        }

        [Test]
        public void RockAlwaysDrawsRock()
        {
            IPlayer player1 = new RockPlayer();
            IPlayer player2 = new RockPlayer();

            RockPaperScissorsGame game = new RockPaperScissorsGame(player1, player2);

            Outcome result = game.Play();

            Assert.AreEqual(Outcome.Draw, result);
        }

        [Test]
        public void PaperAlwaysDrawsPaper()
        {
            IPlayer player1 = new PaperPlayer();
            IPlayer player2 = new PaperPlayer();

            RockPaperScissorsGame game = new RockPaperScissorsGame(player1, player2);

            Outcome result = game.Play();

            Assert.AreEqual(Outcome.Draw, result);
        }
        
        [Test]
        public void ScissorsAlwaysDrawsScissors()
        {
            IPlayer player1 = new ScissorsPlayer();
            IPlayer player2 = new ScissorsPlayer();

            RockPaperScissorsGame game = new RockPaperScissorsGame(player1, player2);

            Outcome result = game.Play();

            Assert.AreEqual(Outcome.Draw, result);
        }
    }
}
