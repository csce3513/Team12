namespace ProjectCowTest
{
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

    [TestClass]
    public class SpaceshipTest
    {
        [TestMethod]
        public void MoveLeftTest()
        {
            Spaceship spaceship = new Spaceship();
            spaceship.X = 300;

            spaceship.MoveLeft();

            Assert.AreEqual(300 - spaceship.Speed, spaceship.X);
        }

        [TestMethod]
        public void MoveRightTest()
        {
            Spaceship spaceship = new Spaceship();
            spaceship.X = 0;

            spaceship.MoveRight();

            Assert.AreEqual(0 + spaceship.Speed, spaceship.X);
        }

        [TestMethod]
        public void MoveUpTest()
        {
            Spaceship spaceship = new Spaceship();
            spaceship.Y = 300;

            spaceship.MoveUp();

            Assert.AreEqual(300 - spaceship.Speed, spaceship.Y);
        }

        [TestMethod]
        public void MoveDownTest()
        {
            Spaceship spaceship = new Spaceship();
            spaceship.Y = 0;

            spaceship.MoveDown();

            Assert.AreEqual(0 + spaceship.Speed, spaceship.Y);
        }

        [TestMethod]
        public void LeftBoundaryTest()
        {
            Spaceship spaceship = new Spaceship();
            spaceship.LeftBoundary = 0;
            spaceship.MoveLeft();

            Assert.AreEqual(0, spaceship.X);
        }

        [TestMethod]
        public void RightBoundaryTest()
        {
            Spaceship spaceship = new Spaceship();
            spaceship.X = 300 - spaceship.Width;
            spaceship.RightBoundary = 300;
            spaceship.MoveRight();

            Assert.AreEqual(300 - spaceship.Width, spaceship.X);
        }

        [TestMethod]
        public void TopBoundaryTest()
        {
            Spaceship spaceship = new Spaceship();
            spaceship.Y = 0;
            spaceship.TopBoundary = 0;
            spaceship.MoveUp();

            Assert.AreEqual(0, spaceship.Y);
        }

        [TestMethod]
        public void BottomBoundaryTest()
        {
            Spaceship spaceship = new Spaceship();
            spaceship.Y = 300 - spaceship.Height;
            spaceship.BottomBoundary = 300;
            spaceship.MoveDown();

            Assert.AreEqual(300 - spaceship.Height, spaceship.Y);
        }

        [TestMethod]
        public void GetBeamNameTest()
        {
            Spaceship spaceship = new Spaceship();

            Assert.AreEqual("Tractor Beam", spaceship.CurrentBeam.Name);
        }
    }
}