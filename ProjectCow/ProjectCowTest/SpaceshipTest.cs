using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ProjectCow;

namespace ProjectCowTest
{
    [TestClass]
    public class SpaceshipTest
    {
        [TestMethod]
        public void MoveLeftTest()
        {
            float previousX;

            Spaceship spaceship = new Spaceship();
            previousX = spaceship.Position.X;

            spaceship.MoveLeft();

            Assert.AreEqual(previousX - spaceship.Speed, spaceship.Position.X);
        }

        [TestMethod]
        public void MoveRightTest()
        {
            float previousX;

            Spaceship spaceship = new Spaceship();
            previousX = spaceship.Position.X;

            spaceship.MoveRight();

            Assert.AreEqual(previousX + spaceship.Speed, spaceship.Position.X);
        }

        [TestMethod]
        public void MoveUpTest()
        {
            float previousY;

            Spaceship spaceship = new Spaceship();
            previousY = spaceship.Position.Y;

            spaceship.MoveUp();

            Assert.AreEqual(previousY - spaceship.Speed, spaceship.Position.Y);
        }

        [TestMethod]
        public void MoveDownTest()
        {
            float previousY;

            Spaceship spaceship = new Spaceship();
            previousY = spaceship.Position.Y;

            spaceship.MoveDown();

            Assert.AreEqual(previousY + spaceship.Speed, spaceship.Position.Y);
        }

        [TestMethod]
        public void ProcessLeftInputTest()
        {
            float previousX;

            Spaceship spaceship = new Spaceship();

            previousX = spaceship.Position.X;
            spaceship.ProcessInputs(new KeyboardState(Keys.Left));
            Assert.AreEqual(previousX - spaceship.Speed, spaceship.Position.X);
        }

        [TestMethod]
        public void ProcessRightInputTest()
        {
            float previousX;

            Spaceship spaceship = new Spaceship();

            previousX = spaceship.Position.X;
            spaceship.ProcessInputs(new KeyboardState(Keys.Right));
            Assert.AreEqual(previousX + spaceship.Speed, spaceship.Position.X);
        }

        [TestMethod]
        public void ProcessUpInputTest()
        {
            float previousY;

            Spaceship spaceship = new Spaceship();

            previousY = spaceship.Position.Y;
            spaceship.ProcessInputs(new KeyboardState(Keys.Up));
            Assert.AreEqual(previousY - spaceship.Speed, spaceship.Position.Y);
        }

        [TestMethod]
        public void ProcessDownInputTest()
        {
            float previousY;

            Spaceship spaceship = new Spaceship();

            previousY = spaceship.Position.Y;
            spaceship.ProcessInputs(new KeyboardState(Keys.Down));
            Assert.AreEqual(previousY + spaceship.Speed, spaceship.Position.Y);
        }

        [TestMethod]
        public void ProcessSpaceInputTest()
        {
            Spaceship spaceship = new Spaceship();

            spaceship.ProcessInputs(new KeyboardState(Keys.Space));
            Assert.IsTrue(spaceship.TractorBeam);
        }

        [TestMethod]
        public void TurnOffTractorBeamTest()
        {
            Spaceship spaceship = new Spaceship();

            spaceship.TractorBeam = true;
            spaceship.ProcessInputs(new KeyboardState());
            Assert.IsFalse(spaceship.TractorBeam);
        }
    }
}
