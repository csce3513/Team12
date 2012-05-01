using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectCow;
using Microsoft.Xna.Framework;

namespace ProjectCowTest
{
    [TestClass]
    public class GameScreenTest
    {
        GameScreen gameScreen = new GameScreen();

        [TestMethod]
        public void DetermineBeamTest()
        {
            // PrivateObject allows you access private stuff of class
            PrivateObject screen = new PrivateObject(gameScreen);
            object beamName = screen.Invoke("DetermineBeam");

            Assert.AreEqual("Tractor Beam", (String)beamName);
        }
    }
}
