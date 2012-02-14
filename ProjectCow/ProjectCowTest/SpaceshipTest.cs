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
            previousX = spaceship.X;

            spaceship.MoveLeft();

            Assert.AreEqual(previousX - spaceship.Speed, spaceship.X);
        }

        [TestMethod]
        public void MoveRightTest()
        {
            float previousX;

            Spaceship spaceship = new Spaceship();
            previousX = spaceship.X;

            spaceship.MoveRight();

            Assert.AreEqual(previousX + spaceship.Speed, spaceship.X);
        }

        [TestMethod]
        public void MoveUpTest()
        {
            float previousY;

            Spaceship spaceship = new Spaceship();
            previousY = spaceship.Y;

            spaceship.MoveUp();

            Assert.AreEqual(previousY - spaceship.Speed, spaceship.Y);
        }

        [TestMethod]
        public void MoveDownTest()
        {
            float previousY;

            Spaceship spaceship = new Spaceship();
            previousY = spaceship.Y;

            spaceship.MoveDown();

            Assert.AreEqual(previousY + spaceship.Speed, spaceship.Y);
        }
    }
}
