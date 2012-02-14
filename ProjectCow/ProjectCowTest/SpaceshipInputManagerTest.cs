using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectCow;
using Microsoft.Xna.Framework.Input;

namespace ProjectCowTest
{
    [TestClass]
    public class SpaceshipInputManagerTest
    {
        [TestMethod]
        public void LeftInputTest()
        {
            float previousX;

            Spaceship spaceship = new Spaceship();
            SpaceshipInputManager input = new SpaceshipInputManager(spaceship);

            previousX = spaceship.X;
            input.ProcessInputs(new KeyboardState(Keys.Left));
            Assert.AreEqual(previousX - spaceship.Speed, spaceship.X);
        }

        [TestMethod]
        public void RightInputTest()
        {
            float previousX;

            Spaceship spaceship = new Spaceship();
            SpaceshipInputManager input = new SpaceshipInputManager(spaceship);

            previousX = spaceship.X;
            input.ProcessInputs(new KeyboardState(Keys.Right));
            Assert.AreEqual(previousX + spaceship.Speed, spaceship.X);
        }

        [TestMethod]
        public void UpInputTest()
        {
            float previousY;

            Spaceship spaceship = new Spaceship();
            SpaceshipInputManager input = new SpaceshipInputManager(spaceship);

            previousY = spaceship.Y;
            input.ProcessInputs(new KeyboardState(Keys.Up));
            Assert.AreEqual(previousY - spaceship.Speed, spaceship.Y);
        }

        [TestMethod]
        public void DownInputTest()
        {
            float previousY;

            Spaceship spaceship = new Spaceship();
            SpaceshipInputManager input = new SpaceshipInputManager(spaceship);

            previousY = spaceship.Y;
            input.ProcessInputs(new KeyboardState(Keys.Down));
            Assert.AreEqual(previousY + spaceship.Speed, spaceship.Y);
        }

        [TestMethod]
        public void ProcessSpaceInputTest()
        {
            Spaceship spaceship = new Spaceship();
            SpaceshipInputManager input = new SpaceshipInputManager(spaceship);

            input.ProcessInputs(new KeyboardState(Keys.Space));
            Assert.IsTrue(spaceship.TractorBeam);
        }
    }
}
