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

        [TestMethod]
        public void OperateTractorBeamTest()
        {
            PrivateObject screen = new PrivateObject(gameScreen);
            Spaceship spaceship = new Spaceship();
            spaceship.X = 49;
            spaceship.Width = 200;
            Cow cow = new Cow(new Vector2(50, 500), 0, null);

            screen.SetField("spaceship", spaceship);
            screen.SetField("cow", cow);
            screen.Invoke("OperateTractorBeam");
            cow = (Cow)screen.GetField("cow");
            cow.Update(new GameTime());

            Assert.IsTrue(cow.Position.Y < 500);
        }
    }
}
