using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectYellow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectYellow.Tests
{
    [TestClass()]
    public class GameTests
    {
        [TestMethod()]
        public void NewGameIsNotOver()
        {
            var game = new Game(randomSeed: 0);
            Assert.IsFalse(game.IsOver);
        }
    }
}