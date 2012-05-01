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
    public class SpaceshipControllerTest
    {
        [TestMethod]
        public void LeftInputTest()
        {
            Spaceship spaceship = new Spaceship();
            InputManager input = new InputManager();
            PrivateObject privateInput = new PrivateObject(input);
            privateInput.SetField("currentState", new KeyboardState(Keys.A));

            SpaceshipController controller = new SpaceshipController(spaceship, input);

            spaceship.X = 300;
            controller.ProcessInput();
            Assert.AreEqual(300 - spaceship.Speed, spaceship.X);
        }

        [TestMethod]
        public void RightInputTest()
        {
            Spaceship spaceship = new Spaceship();
            InputManager input = new InputManager();
            PrivateObject privateInput = new PrivateObject(input);
            privateInput.SetField("currentState", new KeyboardState(Keys.D));

            SpaceshipController controller = new SpaceshipController(spaceship, input);

            spaceship.X = 0;
            controller.ProcessInput();
            Assert.AreEqual(0 + spaceship.Speed, spaceship.X);
        }

        [TestMethod]
        public void UpInputTest()
        {
            Spaceship spaceship = new Spaceship();
            InputManager input = new InputManager();
            PrivateObject privateInput = new PrivateObject(input);
            privateInput.SetField("currentState", new KeyboardState(Keys.W));

            SpaceshipController controller = new SpaceshipController(spaceship, input);

            spaceship.Y = 300;
            controller.ProcessInput();
            Assert.AreEqual(300 - spaceship.Speed, spaceship.Y);
        }

        [TestMethod]
        public void DownInputTest()
        {
            Spaceship spaceship = new Spaceship();
            InputManager input = new InputManager();
            PrivateObject privateInput = new PrivateObject(input);
            privateInput.SetField("currentState", new KeyboardState(Keys.S));

            SpaceshipController controller = new SpaceshipController(spaceship, input);

            spaceship.Y = 0;
            controller.ProcessInput();
            Assert.AreEqual(0 + spaceship.Speed, spaceship.Y);
        }

        [TestMethod]
        public void ProcessSpaceInputTest()
        {
            Spaceship spaceship = new Spaceship();
            InputManager input = new InputManager();
            PrivateObject privateInput = new PrivateObject(input);
            privateInput.SetField("currentState", new KeyboardState(Keys.Space));

            SpaceshipController controller = new SpaceshipController(spaceship, input);

            controller.ProcessInput();
            Assert.IsTrue(spaceship.BeamOn);
        }
    }
}