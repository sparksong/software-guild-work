using BattleShip.BLL;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;
using NUnit.Framework;

namespace Battleship.Tests
{
    [TestFixture]
    class CoordinateTranslatorTest
    {
        Translator translate = new Translator();

        [TestCase("a", 1)]
        [TestCase("", -1)]
        [TestCase("z", -1)]
        [TestCase("0", -1)]
        [TestCase("j", 10)]
        [TestCase("k", -1)]
        [TestCase("zasdfasdgafgfdfadfgfgsgf", -1)]
        [TestCase(" ", -1)]
        [TestCase("c", 3)]
        [TestCase("-4.5", -1)]
        public void TranslateInputTest(string xCoordinate, int expected)
        {
            int actual = translate.ToNumbersXY(xCoordinate);
            Assert.AreEqual(expected, actual);
        }
    }
}
