namespace ProjectCowTest
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProjectCow;
    using Microsoft.Xna.Framework.Input;

    [TestClass]
    public class SpaceshipInputManagerTest
    {
        [TestMethod]
        public void LeftInputTest()
        {
            Spaceship spaceship = new Spaceship();
            SpaceshipInputManager input = new SpaceshipInputManager(spaceship);

            spaceship.X = 300;
            input.ProcessInputs(new KeyboardState(Keys.A));
            Assert.AreEqual(300 - spaceship.Speed, spaceship.X);
        }

        [TestMethod]
        public void RightInputTest()
        {
            Spaceship spaceship = new Spaceship();
            SpaceshipInputManager input = new SpaceshipInputManager(spaceship);

            spaceship.X = 0;
            input.ProcessInputs(new KeyboardState(Keys.D));
            Assert.AreEqual(0 + spaceship.Speed, spaceship.X);
        }

        [TestMethod]
        public void UpInputTest()
        {
            Spaceship spaceship = new Spaceship();
            SpaceshipInputManager input = new SpaceshipInputManager(spaceship);

            spaceship.Y = 300;
            input.ProcessInputs(new KeyboardState(Keys.W));
            Assert.AreEqual(300 - spaceship.Speed, spaceship.Y);
        }

        [TestMethod]
        public void DownInputTest()
        {
            Spaceship spaceship = new Spaceship();
            SpaceshipInputManager input = new SpaceshipInputManager(spaceship);

            spaceship.Y = 0;
            input.ProcessInputs(new KeyboardState(Keys.S));
            Assert.AreEqual(0 + spaceship.Speed, spaceship.Y);
        }

        [TestMethod]
        public void ProcessSpaceInputTest()
        {
            Spaceship spaceship = new Spaceship();
            SpaceshipInputManager input = new SpaceshipInputManager(spaceship);

            input.ProcessInputs(new KeyboardState(Keys.Space));
            Assert.IsTrue(spaceship.BeamOn);
        }
    }
}