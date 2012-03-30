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
            GameObjectsManager manager = new GameObjectsManager(new Random());
            manager.AddRandomObject();

            screen.SetField("spaceship", spaceship);
            screen.SetField("gameObjectsManager", manager);
            screen.Invoke("OperateTractorBeam");
            manager = (GameObjectsManager)screen.GetField("gameObjectsManager");
            manager.Update(new GameTime());

            PrivateObject privateManager = new PrivateObject(manager);
            List<LiftObject> liftObjects = (List<LiftObject>)privateManager.GetField("liftObjects");

            Assert.IsTrue(liftObjects[0].Position.Y < 500);
        }
    }
}
