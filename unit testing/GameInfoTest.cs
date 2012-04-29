using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectCow;

namespace ProjectCowTest
{
    [TestClass]
    public class GameInfoTest
    {
        [TestMethod]
        public void AddHealthTest()
        {
            GameInfo info = new GameInfo();
            info.Health += 1000;

            Assert.AreEqual(100, info.Health);

            info.Health -= 50;

            Assert.AreEqual(50, info.Health);
        }
    }
}
